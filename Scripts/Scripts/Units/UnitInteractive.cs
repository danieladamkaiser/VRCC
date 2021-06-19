using Assets.Scripts.Enums;
using Assets.Scripts.Factories;
using Interactables;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public abstract class UnitInteractive : UnitBase
    {
        public GameObject IconPrefab;
        private InteractableIcon interactableIcon;

        protected HashSet<Interactable> interactables = new HashSet<Interactable>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Interactable.ToString()))
            {
                if (!interactableIcon)
                {
                    interactableIcon = Instantiate(IconPrefab).GetComponent<InteractableIcon>();
                }

                var interactable = other.GetComponent<Interactable>();
                if (interactable)
                {
                    interactables.Add(interactable);
                }
            }
            else if (other.CompareTag(Tags.Storage.ToString()))
            {
                EnterStorage(other.GetComponent<StorageController>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Interactable.ToString()))
            {
                var interactable = other.GetComponent<Interactable>();
                if (interactable && interactables.Contains(interactable))
                {
                    interactables.Remove(interactable);
                }

                if (!GetCurrentInteraction())
                {
                    interactableIcon.SetParent(null);
                }
            }
            else if (other.CompareTag(Tags.Storage.ToString()))
            {
                ExitStorage();
            }
        }

        private void Update()
        {
            if (interactables.Any())
            {
                var currentInteraction = GetCurrentInteraction();
                if (currentInteraction)
                {
                    interactableIcon.SetParent(currentInteraction.transform);
                }
            }
        }
        protected virtual void Interact()
        {
            var interactable = GetCurrentInteraction();
            if (interactable)
            {
                interactable.Interact(this);
            }
        }

        protected abstract void EnterStorage(StorageController storageController);
        protected abstract void ExitStorage();
        protected abstract Interactable GetCurrentInteraction();
    }
}
