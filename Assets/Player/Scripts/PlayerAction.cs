using System;
using Base;
using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Attributes.Validation;
using CorePlugin.Core;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace Player.Scripts
{
    [CoreManagerElement]
    public class PlayerAction : MonoBehaviour, IEventSubscriber, IDamageable, IEventHandler
    {
        [SerializeField] [NotNull] private MeshRenderer meshRenderer;
        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private Color color;
        [SerializeField] private float speed;
        [SerializeField] private float maxMinPos;
        private bool markedForDie;
        private bool paused = false;
        private int currentHealth;
        private float objectWidth;

        private event PlayerEvents.GetScore GetScore;
        private event PlayerEvents.OnPlayerHealthReceived OnPlayerHealthReceived;
        private event GeneralEvents.MarkedForDie MarkedForDie;
        private event ScreenStateDelegates.Play Play;
        private event ScreenStateDelegates.Die Die;

        private void Start()
        {
            markedForDie = false;
            transform.position = spawnPosition;
        }


        private void Run(Vector3 dir)
        {
            if (paused) return;
            transform.Translate(dir * speed * Time.deltaTime);

            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, -maxMinPos, maxMinPos);
            transform.position = viewPos;
        }


        private void OnDestroy()
        {
            GetScore!.Invoke(currentHealth);
            EventInitializer.Unsubscribe(this);
        }

        private void DieAction()
        {
            MarkedForDie?.Invoke(markedForDie);
            Die?.Invoke();
            Destroy(gameObject);
        }

        public void ReceiveDamage(Color colorCheck, int receivedDamage)
        {
            if (markedForDie) return;
            if (colorCheck == color) currentHealth += receivedDamage;
            else currentHealth -= receivedDamage;
            OnPlayerHealthReceived!.Invoke(currentHealth);
            if (currentHealth < 0)
            {
                markedForDie = true;
                DieAction();
            }
        }

        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (PlayerEvents.Move) Run,
                (GeneralEvents.OnPauseStateChanged) UpdateMSG
            };
        }
        public void InvokeEvents()
        {
            meshRenderer.material.color = color;
            currentHealth = 0;
            OnPlayerHealthReceived!.Invoke(currentHealth);
        }

        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref OnPlayerHealthReceived, subscribers);
            EventExtensions.Subscribe(ref GetScore, subscribers);
            EventExtensions.Subscribe(ref Play, subscribers);
            EventExtensions.Subscribe(ref Die, subscribers);
            EventExtensions.Subscribe(ref MarkedForDie, subscribers);
        }

        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref OnPlayerHealthReceived, unsubscribers);
            EventExtensions.Unsubscribe(ref GetScore, unsubscribers);
            EventExtensions.Unsubscribe(ref Play, unsubscribers);
            EventExtensions.Unsubscribe(ref Die, unsubscribers);
            EventExtensions.Unsubscribe(ref MarkedForDie, unsubscribers);
        }
        public void UpdateMSG(bool pause)
        {
            paused = pause;
        }
    }
}
