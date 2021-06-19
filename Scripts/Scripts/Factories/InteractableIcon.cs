using UnityEngine;

namespace Assets.Scripts.Factories
{
    public class InteractableIcon : MonoBehaviour
    {
        private Transform parentTransform;


        public void SetParent(Transform parent)
        {
            parentTransform = parent;
            gameObject.SetActive(parent ? true : false);
        }

        private void Update()
        {
            if (parentTransform)
            {
                transform.position = parentTransform.position + new Vector3(0, 0.75f, 0);
                transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
            }
        }
    }
}
