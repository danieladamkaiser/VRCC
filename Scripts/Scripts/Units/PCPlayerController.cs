using Assets.Scripts;
using Assets.Scripts.Common;
using Assets.Scripts.Units;
using Interactables;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units
{
    public class PCPlayerController : UnitInteractive, IAttackable
    {
        public float Health;
        public bool isAlive = true;
        public float Hitpoints { get => Health; set => Health = value; }

        private Interactable currentItem;
        private Weapon currentWeapon;
        private Wearable currentWearable;

        public void OnMove(InputAction.CallbackContext ctx) => Move(ctx.ReadValue<Vector2>());
        public void OnAim(InputAction.CallbackContext ctx) => Rotate(ctx.ReadValue<Vector2>());
        public void OnInteract(InputAction.CallbackContext ctx) => Interact();
        public void OnDropWeapon(InputAction.CallbackContext ctx) => DropWeapon();
        public void OnDropWearable(InputAction.CallbackContext ctx) => DropWearable();
        public void OnAttack(InputAction.CallbackContext ctx) => Attack();
        void Awake()
        {
            GameController.Instance.RegisterPCPlayer(this);
            //controls = new PlayerControls();
            //controls.Enable();
            //controls.Gameplay.Attack.performed += (x) => Attack();
            //controls.Gameplay.Interact.performed += (x) => Interact();
            //controls.Gameplay.DropWeapon.performed += (x) => DropWeapon();
            //controls.Gameplay.DropWearable.performed += (x) => DropWearable();
            //controls.Gameplay.Move.performed += (value) => Move(value.ReadValue<Vector2>());
            //controls.Gameplay.Move.canceled += (value) => Move(Vector2.zero);
            //controls.Gameplay.Aim.performed += (value) => Rotate(value.ReadValue<Vector2>());
        }

        private void DropWearable()
        {
            Debug.Log("WTF");
            if (currentWearable)
            {
                currentWearable.Interact(this);
                currentWearable = null;
            }
        }

        private void Attack()
        {
            if (currentWeapon)
            {
                currentWeapon.Attack();
            }
        }

        private void DropWeapon()
        {
            if (currentWeapon)
            {
                currentWeapon.Interact(this);
                currentWeapon = null;
            }
        }

        protected override void Interact()
        {
            if (currentItem)
            {
                currentItem.Interact(this);
                currentItem = null;
            }
            else
            {
                var interactable = GetCurrentInteraction();

                if (interactable)
                {
                    if (interactable is Weapon weapon)
                    {
                        if (!currentWeapon)
                        {
                            weapon.Interact(this);
                            currentWeapon = weapon;
                        }
                    }
                    else if (interactable is Wearable wearable)
                    {
                        if (!currentWearable)
                        {
                            wearable.Interact(this);
                            currentWearable = wearable;
                        }
                    }
                    else
                    {
                        interactable.Interact(this);
                        currentItem = interactable;
                    }

                }
            }
        }

        protected override Interactable GetCurrentInteraction()
        {
            if (!currentItem)
            {
                var closestInteractable = interactables
                    .Where(i => i.Owner != this)
                    .OrderBy(i => Vector2.Distance(i.transform.position, rb.position))
                    .FirstOrDefault();

                if (closestInteractable != null)
                {
                    if (closestInteractable is Weapon weapon)
                    {
                        if (!currentWeapon)
                        {
                            return weapon;
                        }
                    }
                    else if (closestInteractable is Wearable wearable)
                    {
                        if (!currentWearable)
                        {
                            return wearable;
                        }
                    }
                    else
                    {
                        return closestInteractable;
                    }
                }
            }

            return null;
        }

        protected override void EnterStorage(StorageController storage)
        {
            if (currentItem is Resource resource)
            {
                currentItem.Interact(this);
                currentItem = null;
                interactables.Remove(resource);
                storage.ReceiveResouce(resource);
            }
        }

        protected override void ExitStorage()
        {
            //
        }

        public void Attacked(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                isAlive = false;
            }
        }
    }
}
