using Units;
using UnityEngine;

namespace Interactables
{
    public class ResourceGenerator : Stationary
    {
        public GameObject resource;
        public float cooldown;
        public Vector3 spawnPoint;
        private float currentTimer;
        public override void Interact(UnitBase unit)
        {
            if (currentTimer >= cooldown)
            {
                trigger.enabled = false;
                currentTimer = 0;
                Instantiate(resource, transform.position + spawnPoint, Quaternion.identity);
            }
        }

        private void Update()
        {
            if (currentTimer < cooldown)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer >= cooldown)
                {
                    trigger.enabled = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{other.name} entered trigger");
        }
    }
}
