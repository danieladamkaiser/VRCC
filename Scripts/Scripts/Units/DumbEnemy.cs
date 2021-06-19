using Assets.Scripts;
using Assets.Scripts.Common;
using System;
using Units;
using UnityEngine;

public class DumbEnemy : UnitBase, IAttackable
{
    public float AttackDistance;
    public float Health;
    public float Damage;
    public float AttackCooldown;
    private float currentAttackCooldown;
    public float Hitpoints { get => Health; set => Health = value; }

    private PCPlayerController target;

    public void Attacked(float damage)
    {
        Hitpoints -= damage;
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        FindTarget();
        Move();
        Attack(target);
    }

    private void Attack(PCPlayerController target)
    {
        if (target)
        {
            if (currentAttackCooldown < AttackCooldown)
            {
                currentAttackCooldown += Time.deltaTime;
            }
            else
            {
                if (target && Vector3.Distance(target.transform.position, rb.position) < AttackDistance)
                {
                    target.Attacked(Damage);
                    currentAttackCooldown = 0;
                }
            }
        }
    }

    private void Move()
    {
        if (target)
        {
            var direction = new Vector2(target.transform.position.x - rb.position.x, target.transform.position.z - rb.position.z);
            Rotate(direction);
            if (target && Vector3.Distance(target.transform.position, rb.position) >= AttackDistance)
            {
                Move(direction);
            }
        }
    }

    private void FindTarget()
    {
        if (!target || !target.isAlive)
        {
            target = GameController.Instance.GetClosestPlayer(transform.position);
        }
    }
}
