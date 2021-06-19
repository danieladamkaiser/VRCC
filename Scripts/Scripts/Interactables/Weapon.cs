using System;
using Units;
using UnityEngine;

namespace Interactables
{
    public abstract class Weapon : Movable
    {
        protected bool isReady;
        protected bool isAtacking;
        protected float attackProgress;
        protected float currentCooldown;

        public float Cooldown;
        public Vector3 StartAngle;
        public float AttackSpeed;
        public DamageType DamageType;
        public float Damage;
        public AttackPoint[] AttackPoints;

        public abstract void Attack();
        protected override void Drop()
        {
            rb.isKinematic = false;
            coll.enabled = true;
            transform.parent = null;
        }

        protected override void PickUp(UnitBase unit)
        {
            rb.isKinematic = true;
            coll.enabled = false;
            transform.parent = unit.gameObject.transform;
            transform.localPosition = new UnityEngine.Vector3(0, 0, 0.5f);
            transform.localRotation = Quaternion.Euler(StartAngle);
        }


    }

    [Serializable]
    public struct AttackPoint
    {
        public Transform Transform;
        public float Time;
        public float Radius;
    }
}
