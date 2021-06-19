using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class SwingWeapon : Weapon
    {
        public Vector3 endAngle;

        private List<IAttackable> alreadyAttacked = new List<IAttackable>();
        private int currentAttackPointIndex = 0;

        public override void Attack()
        {
            if (isReady)
            {
                currentCooldown = 0;
                isReady = false;
                isAtacking = true;
                Swing();
            }
        }

        private void Update()
        {
            if (!isAtacking && !isReady)
            {
                currentCooldown += Time.deltaTime;
            }

            if (currentCooldown >= Cooldown)
            {
                isReady = true;
            }

            if (isAtacking)
            {
                Swing();
            }
        }

        private void Swing()
        {
            attackProgress += Time.deltaTime * AttackSpeed;
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(StartAngle, endAngle, attackProgress));

            if (currentAttackPointIndex < AttackPoints.Length)
            {
                var currentAttackPoint = AttackPoints[currentAttackPointIndex];
                if (attackProgress > currentAttackPoint.Time)
                {
                    var colliders = Physics.OverlapSphere(currentAttackPoint.Transform.position, currentAttackPoint.Radius);
                    foreach (var collider in colliders)
                    {
                        var attackable = collider.gameObject.GetComponent<IAttackable>();
                        if (attackable != null && attackable!=(IAttackable)Owner && !alreadyAttacked.Contains(attackable))
                        {
                            attackable.Attacked(Damage);
                            alreadyAttacked.Add(attackable);
                        }
                    }

                    currentAttackPointIndex++;
                }
            }

            if (attackProgress >= 1)
            {
                isAtacking = false;
                transform.localRotation = Quaternion.Euler(StartAngle);
                attackProgress = 0;
                currentAttackPointIndex = 0;
                alreadyAttacked.Clear();
            }
        }
    }
}
