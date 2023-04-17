using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Components
{

    public class Spawner : MonoBehaviour
    {
        [Header("Pool Variable")]
        [SerializeField]
        private int _poolSize = 10;
        [SerializeField]
        private MoveableComponent _moveableComponent;
        private List<GameObject> _pooledObjects;

        [Header("Spawner Location")]
        [SerializeField]
        private Transform spawnPosition;
        [SerializeField]
        private Transform despawnPosition;

        private ICanTriggerSpawn _spawnTrigger;

        private void OnDisable()
        {
            //add implementation
            _spawnTrigger.TriggerSpawn -= HandleOnSpawnTriggered;

            //DisablePooledObject();
        }

        private void OnEnable()
        {
            //add implementation

            _spawnTrigger.TriggerSpawn += HandleOnSpawnTriggered;

            InitializeObjectPool();
        }

        private void InitializeObjectPool()
        {
            _pooledObjects = new List<GameObject>();
            for(int i=0; i < _poolSize; i++)
            {
                GameObject obj = Instantiate(_moveableComponent.gameObject);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
        private void DisablePooledObject()
        {
            foreach (GameObject obj in _pooledObjects)
            {
                obj.SetActive(false);
            }
        }

        public void Setup(ICanTriggerSpawn spawnTrigger)
        {
            //add implementation
            _spawnTrigger = spawnTrigger;
        }

        public void EnableScript()
        {
            //remember to enable script from context if needed
            enabled = true;
        }

        public void HandleOnSpawnTriggered()
        {
            //add implementation
            SpawnMoveableObject();
        }

        private void SpawnMoveableObject()
        {
            //add implementation

            GameObject obj = GetPooledObject();
            if (obj != null)
            {
                obj.transform.position = spawnPosition.position;
                obj.transform.rotation = spawnPosition.rotation;
                obj.SetActive(true);
                obj.GetComponent<MoveableComponent>().SetDestination(despawnPosition.position);
            }
        }
        private GameObject GetPooledObject()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }

            Debug.LogWarning("No available objects in pool.");
            return null;
        }
    }
}