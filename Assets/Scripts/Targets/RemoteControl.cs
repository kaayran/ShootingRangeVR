using UnityEngine;

namespace Targets
{
    public class RemoteControl : MonoBehaviour
    {
        [SerializeField] private AccuracyPanel _panel;
        [SerializeField] private HumanTarget _target;
        [SerializeField] private float _speed;
        
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _target.Init(_speed);
            _panel.Init(_target);
        }

        public void OnTopButtonPressed()
        {
            _target.MoveForward();
        }

        public void OnBotButtonPressed()
        {
            _target.MoveBackward();
        }
    }
}