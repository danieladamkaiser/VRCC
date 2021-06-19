using UnityEngine;

namespace Units
{
    public class UnitBase : MonoBehaviour
    {
        public float speed;
        public Rigidbody rb;

        protected void Move(Vector2 direction)
        {
            rb.velocity = Vector3.ClampMagnitude(new Vector3(direction.x, 0, direction.y), 1) * speed;
        }

        protected void Rotate(Vector2 direction)
        {
            if (direction.magnitude > 0.1f)
            {
                var curRotation = Vector3.left * direction.x + Vector3.forward * direction.y;
                var playerRotation = Quaternion.Inverse(Quaternion.LookRotation(curRotation, Vector3.up));
                rb.MoveRotation(playerRotation);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GetComponent<BoxCollider>().isTrigger = false;            
        }
    }
}
