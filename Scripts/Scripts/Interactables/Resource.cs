using Assets.Scripts;
using Assets.Scripts.Enums;
using Units;

namespace Interactables
{
    public class Resource : Movable
    {
        public ResourceType ResourceType;
        public float Quantity;

        protected override void Drop()
        {
            rb.isKinematic = false;
            transform.parent = null;
            Owner = null;
            coll.enabled = true;
        }

        protected override void PickUp(UnitBase unit)
        {
            rb.isKinematic = true;
            transform.parent = unit.transform;
            Owner = unit;
            coll.enabled = false;
        }
    }
}
