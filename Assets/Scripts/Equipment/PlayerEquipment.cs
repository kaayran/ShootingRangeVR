using UnityEngine;

namespace Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private HeadVisualizer _headVisualizer;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _headVisualizer.Init();
        }
    }
}