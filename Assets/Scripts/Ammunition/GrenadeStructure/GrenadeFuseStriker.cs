using System;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseStriker : MonoBehaviour
    {
        public event Action<float> OnStrike;

        [SerializeField] private float _delay;

        private GrenadeFuseLever _fuseLever;
        private GrenadeFuseRing _fuseRing;

        private bool _isLeverLocked;
        private bool _isRingDragged;

        public void Init(GrenadeFuseLever lever, GrenadeFuseRing ring)
        {
            _fuseLever = lever;
            _fuseRing = ring;

            _fuseRing.OnDrag += OnDrag;

            _fuseLever.OnRelease += OnRelease;
            _fuseLever.OnLock += OnLock;

            _isLeverLocked = false;
            _isRingDragged = false;
        }

        private void OnLock()
        {
            _isLeverLocked = true;
            // Switch state, on action from lever,
            // we can freely lock then unlock lever
        }

        private void OnRelease()
        {
            _isLeverLocked = false;
            if (!_isRingDragged) return;
            // If we release lever, then we check is ring already dragged?
            // Then detonate
            OnStrike?.Invoke(_delay);
            _fuseLever.OnLock -= OnLock;
            _fuseLever.OnRelease -= OnRelease;
        }

        private void OnDrag()
        {
            _isRingDragged = true;
            _fuseRing.OnDrag -= OnDrag;

            if (_isLeverLocked) return;
            // Explode anyway, if ring dragged, and lever is not locked

            OnStrike?.Invoke(_delay);
            _fuseLever.OnLock -= OnLock;
            _fuseLever.OnRelease -= OnRelease;
        }
    }
}