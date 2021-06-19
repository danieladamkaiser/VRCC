using System;
using Units;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Interactable : MonoBehaviour
    {
        protected SphereCollider trigger;
        public UnitBase Owner { get; set; }
        public abstract void Interact(UnitBase unit);

        private void Awake()
        {
            trigger = GetComponent<SphereCollider>();
            trigger.radius = 2.5f;
            trigger.isTrigger = true;
            Initialize();
        }

        protected virtual void Initialize() { }
    }
}
