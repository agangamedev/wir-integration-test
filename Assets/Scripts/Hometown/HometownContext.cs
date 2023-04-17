using Project.Components;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Hometown
{
    [RequireComponent(typeof(InputManager) , typeof(Spawner))]
    public class HometownContext : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private Spawner _spawner;
        [SerializeField]
        private HouseView _houseView;
        [SerializeField]
        private TextMeshProUGUI _textLevel;

        public HouseController HouseController { get; private set; }
        
        private void Awake()
        {
            if(_inputManager == null)
            {
                _inputManager = GetComponent<InputManager>();
            }

            if (_spawner == null)
            {
                _spawner =  GetComponent<Spawner>();
            }

            //add implementation

            var upgradableRepository = new UpgradeableRepository(this);
            UpgradeableData upgradeableData = null;
            upgradableRepository.GetUpgradeableData(data =>
            {
                upgradeableData = data;

                HouseController = new HouseController(this, "UpgradeableItemName", _inputManager, upgradeableData);

                _houseView = FindObjectOfType<HouseView>().Setup(HouseController);
                _houseView.EnableScript();

                _spawner.Setup(HouseController);
                _spawner.EnableScript();

                UpdateTextLevel(data);

            });

        }

        private void OnDisable()
        {
            //add implementation
            if (HouseController != null)
            {
                HouseController.OnContextDispose();
            }
        }

        private void OnEnable()
        {
            _inputManager.OnInputTouch += UpgradeHouse; 
        }

        private void UpgradeHouse()
        {
            HouseController.Upgrade();

            UpdateTextLevel(HouseController._upgradeableData);
        }

        public void UpdateTextLevel(UpgradeableData _data)
        {
            _textLevel.text = ($"Level {_data.Level} / {_data.MaxLevel}");
        }
    }
}