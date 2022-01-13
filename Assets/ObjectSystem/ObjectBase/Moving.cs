using System;
using CorePlugin.Attributes.EditorAddons;
using CorePlugin.Attributes.Headers;
using CorePlugin.Attributes.Validation;
using ObjectSystem.ObjectBase.Interfaces;
using UnityEngine;

namespace ObjectSystem.ObjectBase
{
    [CoreManagerElement, RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(MeshRenderer))]
    public abstract class Moving : MonoBehaviour, IPoolObject
    {
        [NotNull] [SerializeField] private protected Rigidbody rigidbody;
        [NotNull] [SerializeField] private protected MeshRenderer meshRenderer;
        [SettingsHeader] [SerializeField] private protected int damage;

        [SerializeField] private protected Color color;
        [SerializeField] private protected Vector3 size;
        [SerializeField] private protected float speed;

        private protected bool paused;
        private protected bool markedForDie;

        private protected Action<IPoolObject> onReleaseNeeded;

        public bool IsReleased { get; private set; }


        private protected Vector3 oldVelocity;

        public virtual void FixedUpdate()
        {
            if (paused) return;
            var position = transform.position + GetFlyDirection() * speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(position);
        }
        public abstract void OnTriggerEnter(Collider other);

        private protected abstract Vector3 GetFlyDirection();


        public virtual void Destroy()
        {
            Destroy(this);
        }

        public virtual void Initialize(Action<IPoolObject> onRelease)
        {
            onReleaseNeeded = onRelease;
            meshRenderer.materials[0].color = color;
            transform.localScale = size;
        }

        public virtual IPoolObject Instantiate()
        {
            return Instantiate(this);
        }

        public virtual void SetPosition(Vector3 newPos)
        {
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
            onReleaseNeeded.Invoke(this);
        }

        public virtual void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public virtual void OnPause(bool pause)
        {
            paused = pause;
            if (paused)
            {
                oldVelocity = rigidbody.velocity;
                rigidbody.velocity = Vector3.zero;
            }
            else
            {
                rigidbody.velocity = oldVelocity;
                oldVelocity = Vector3.zero;
            }
        }

        public virtual void ResetState()
        {
            markedForDie = false;
        }
    }
}
