using Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        private StorageController storage;
        private List<PCPlayerController> pCPlayers = new List<PCPlayerController>();

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("GameController already exists!");
            }   
            Instance = this;
        }

        public PCPlayerController GetClosestPlayer(Vector3 position)
        {
            var lowestDistance = float.MaxValue;
            PCPlayerController closestPlayer = null;
            foreach (var player in pCPlayers.Where(p => p.isAlive))
            {
                var distance = Vector3.Distance(player.transform.position, position);
                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    closestPlayer = player;
                }
            }

            return closestPlayer;
        }

        public void RegisterPCPlayer(PCPlayerController pCPlayer)
        {
            pCPlayers.Add(pCPlayer);
        }

        public void RegisterStorage(StorageController storage)
        {
            this.storage = storage;
        }
    }
}
