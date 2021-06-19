using Units;
using UnityEngine;

namespace Interactables
{
    public abstract class Movable : Interactable
    {
        protected Rigidbody rb;
        protected Collider coll;

        protected override void Initialize()
        {
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<BoxCollider>();
        }

        public bool IsCarried { get; set; }

        public override void Interact(UnitBase unit)
        {
            if (IsCarried)
            {
                IsCarried = false;
                Owner = null;
                Drop();
            }
            else
            {
                IsCarried = true;
                Owner = unit;
                PickUp(unit);
            }
        }
        protected abstract void PickUp(UnitBase unit);
        protected abstract void Drop();
    }
}
