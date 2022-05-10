using Equipment.Body;
using Equipment.Head;
using UnityEngine;

namespace Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private HeadVisualizer _headVisualizer;
        [SerializeField] private BodyVisualizer _bodyVisualizer;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _headVisualizer.Init();
            _bodyVisualizer.Init();
        }
    }
}