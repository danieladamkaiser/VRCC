using Assets.Scripts.Enums;
using Interactables;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class StorageController : MonoBehaviour
    {
        public ResourcePoint[] ResourcePoints;
        private void Awake()
        {
            GameController.Instance.RegisterStorage(this);
        }

        public void ReceiveResouce(Resource resource)
        {
            var resourcePoint = ResourcePoints.First(r => r.ResourceType == resource.ResourceType);
            resource.transform.rotation = resourcePoint.Transform.rotation;
            resource.transform.position = resourcePoint.Transform.position;
        }
    }

    [Serializable]
    public class ResourcePoint
    {
        public ResourceType ResourceType;
        public Transform Transform;
    }
}
