using StructureComponents;
using UnityEngine;
using Valve.VR;

namespace Targets
{
    [RequireComponent(typeof(Attachment))]
    public class RemoteControl : MonoBehaviour
    {
        [SerializeField] private AccuracyPanel _panel;
        [SerializeField] private HumanTarget _target;
        [SerializeField] private float _speed;

        [SerializeField] private SteamVR_Action_Boolean _forwardButton;
        [SerializeField] private SteamVR_Action_Boolean _backwardButton;

        private Attachment _attachment;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _target.Init(_speed);
            _attachment = GetComponent<Attachment>();

            _panel.Init(_target);
        }

        private void Update()
        {
            if (!_attachment.TryGetHand(out var hand)) return;
            var source = hand.handType;

            var isForward = _forwardButton.GetState(source);
            var isBackward = _backwardButton.GetState(source);

            if (isForward)
            {
                _target.MoveForward();
            }
            else if (isBackward)
            {
                _target.MoveBackward();
            }
        }
    }
}