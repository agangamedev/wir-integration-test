using UnityEngine;

namespace Project.Hometown
{
    public class HouseView : MonoBehaviour
    {
        [SerializeField]
        private Transform _houseTransform;
        [SerializeField]
        private float _scaleFactor = 1f;


        private HouseController _houseController;


        private void Awake()
        {
        }

        private void OnDisable()
        {
            //add implementation

            _houseController.OnLevelUp -= HandleOnHouseLevelUp;
        }

        private void OnEnable()
        {

            //add implementation

            _houseController.OnLevelUp += HandleOnHouseLevelUp; 
        }

        public HouseView Setup(HouseController houseController)
        {
            _houseController= houseController;

            HouseScaling(_houseController._upgradeableData.Level);

            return this;
        }

        public void EnableScript()
        {
            //remember to enable script from context if needed
            enabled = true;
        }

        public void HandleOnHouseLevelUp(LevelupEventData data)
        {
            //add implementation

            if(data.Level < data.MaxLevel)
            {
                HouseScaling(data.Level);
            }
            else
            {
                Debug.Log("Max level View");
            }
        }

        private void HouseScaling(int level)
        {
            _houseTransform.localScale = Vector3.one * (_scaleFactor * level);
        }
    }
}