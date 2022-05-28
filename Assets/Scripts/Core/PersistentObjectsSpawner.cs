using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObject;
        static bool hasSpawned = false;
        private void Awake() {
            if(hasSpawned) return;

            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject obj = Instantiate(persistentObject);
            DontDestroyOnLoad(obj);
        }
    } 
}
