using Project.Components;
using System;
using UnityEngine;

namespace Project.Hometown
{
    public class HouseController : IController, IUpgradeable, ICanTriggerSpawn
    {
        public event Action<LevelupEventData> OnLevelUp;
        public event Action TriggerSpawn;

        private HometownContext _hometownContext;
        private string _itemName;
        public UpgradeableData _upgradeableData;
        public UpgradeableRepository upgradeableRepository;

        public void OnContextDispose()
        {
            //add implementation

            _upgradeableData = null;
            OnLevelUp = null;
            TriggerSpawn = null;
        }

        public HouseController(HometownContext hometownContext , string upgradeableItemName , InputManager inputManager, UpgradeableData upgradeableData)
        {
            _hometownContext = hometownContext;
            _itemName = upgradeableItemName;

            //add implementation
            _upgradeableData = upgradeableData;

            Debug.Log(_upgradeableData.Level + " max : " + _upgradeableData.MaxLevel);
        }

        public void Upgrade()
        {
            //Debug.Log($"Handle Upgrade {_itemName}");
            //add implementation

            _upgradeableData.LevelUp();
            if (OnLevelUp != null)
            {
                // Membuat instance event data dan memanggil event OnLevelUp
                LevelupEventData eventData = new LevelupEventData(_upgradeableData.Level, _upgradeableData.MaxLevel);
                OnLevelUp(eventData);
            }

            if(_upgradeableData.IsMaxLevel())
            {
                Debug.Log("MaxLevel Controll > Spawn Tank if aavailable");
                TriggerSpawnAction();
            }
        }

        public void TriggerSpawnAction()
        {
            if (TriggerSpawn != null)
            {
                TriggerSpawn();
            }
        }
    }
}