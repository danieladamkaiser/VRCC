using Assets.Scripts.Common;
using Units;

namespace Interactables
{
    public class Barrier : Movable, IAttackable
    {
        public float Durability;

        public float Hitpoints { get => Durability; set => Durability = value; }

        public virtual void Attacked(float damage)
        {
            Durability -= damage;
            if (Durability<=0)
            {
                Destroy(gameObject);
            }
        }

        protected override void Drop()
        {
            transform.parent = null;
            Owner = null;
            coll.enabled = true;
        }

        protected override void PickUp(UnitBase unit)
        {
            transform.parent = unit.transform;
            Owner = unit;
            coll.enabled = false;
        }
    }
}
