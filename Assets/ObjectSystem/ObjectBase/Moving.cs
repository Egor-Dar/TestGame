using System;
using Base;
using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Attributes.Headers;
using CorePlugin.Attributes.Validation;
using CorePlugin.Cross.Events.Interface;
using CorePlugin.Extensions;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectSystem.ObjectBase
{
    [CoreManagerElement, RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
    public abstract class Moving : MonoBehaviour, IPoolObject, IEventHandler
    {
        [SettingsHeader] [SerializeField] private protected int damage;

        [SerializeField] private protected Vector3 size;
        private event GeneralEvents.GetRandomColor GetColor;
        private protected MaterialPropertyBlock materialPropertyBlock;

        private protected bool markedForDie;

        private protected Action<IPoolObject> onReleaseNeeded;

        public bool IsReleased { get; private set; }

        public abstract void OnTriggerEnter(Collider other);

        public virtual void Destroy()
        {
            Destroy(this);
        }

        public virtual void Initialize(Action<IPoolObject> onRelease)
        {
            onReleaseNeeded = onRelease;
           materialPropertyBlock.SetColor(0,GetColor!.Invoke());
            transform.localScale = size;
        }

        public virtual IPoolObject Instantiate()
        {
            return Instantiate(this);
        }

        public virtual void SetPosition(Vector3 newPos)
        {
            Debug.Log(newPos);
            transform.position = newPos;
        }

        public void OnGet()
        {
            IsReleased = false;
        }

        public void OnRelease()
        {
            IsReleased = true;
        }

        private protected virtual void Die()
        {
            Unsubscribe();
            onReleaseNeeded.Invoke(this);
        }

        public virtual void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public virtual void ResetState()
        {
            markedForDie = false;
        }
        public void InvokeEvents()
        {

        }
        public void Subscribe(params Delegate[] subscribers)
        {
            EventExtensions.Subscribe(ref GetColor, subscribers);
        }
        public void Unsubscribe(params Delegate[] unsubscribers)
        {
            EventExtensions.Unsubscribe(ref GetColor, unsubscribers);
        }
    }
}
