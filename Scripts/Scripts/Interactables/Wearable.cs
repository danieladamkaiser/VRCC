using Units;
using UnityEngine;

namespace Interactables
{
    public class Wearable : Movable
    {
        protected override void Drop()
        {
            rb.isKinematic = false;
            coll.enabled = true;
            Owner = null;
            transform.parent = null;
        }

        protected override void PickUp(UnitBase unit)
        {
            rb.isKinematic = true;
            coll.enabled = false;
            Owner = unit;
            transform.parent = unit.transform;
            transform.localPosition = new Vector3(0,0.8f,-0.4f);
            transform.localRotation = Quaternion.identity;
        }
    }
}
