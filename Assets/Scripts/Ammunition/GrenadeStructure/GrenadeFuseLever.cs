using System;
using StructureComponents;
using UnityEngine;
using Valve.VR;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseLever : MonoBehaviour
    {
        public event Action OnRelease;
        public event Action OnLock;

        [SerializeField] private SteamVR_Action_Single _trigger;

        private Attachment _attachment;

        private bool _isInit;
        private LeverState _state;
        private SteamVR_Input_Sources _type;

        public void Init(Attachment attachment)
        {
            _attachment = attachment;
            _isInit = true;

            _attachment.OnDrop += Drop;
        }

        public void SetAttachment(Attachment attachment)
        {
            // Unsubscribe from event of old attachment
            _attachment.OnDrop -= Drop;

            _attachment = attachment;
            _attachment.OnDrop += Drop;
            // Subscribe to new attachment event
            _state = LeverState.Unlocking;
        }

        private void Drop()
        {
            OnRelease?.Invoke();
            // If we throw fuse away, when need to be sure, 
            // than we release lever
        }

        private void Update()
        {
            if (!_isInit) return;
            if (!_attachment.TryGetHand(out var hand))
            {
                _state = LeverState.Attached;
                return;
            }

            _type = hand.handType;

            switch (_state)
            {
                case LeverState.Attached:
                    if (!(_trigger[_type].axis < 0.1f)) return;
                    _state = LeverState.Locking;
                    break;
                case LeverState.Locking:
                    if (!(_trigger[_type].axis > 0.75f)) return;
                    OnLock?.Invoke();
                    _state = LeverState.Unlocking;
                    break;
                case LeverState.Unlocking:
                    if (!(_trigger[_type].axis < 0.25f)) return;
                    _state = LeverState.Pull;
                    break;
                case LeverState.Pull:
                    OnRelease?.Invoke();
                    _state = LeverState.Attached;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum LeverState
        {
            Attached,
            Locking,
            Unlocking,
            Pull
        }
    }
}