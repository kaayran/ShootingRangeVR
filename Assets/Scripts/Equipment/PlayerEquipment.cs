using UnityEngine;
using UnityEngine.Audio;

namespace Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private HeadVisualizer _headVisualizer;
        [SerializeField] private AudioMixer _mixer;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _headVisualizer.Init(_mixer);
        }
    }
}