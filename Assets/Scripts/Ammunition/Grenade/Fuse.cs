using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using Weapon;

namespace Ammunition.Grenade
{
    [RequireComponent(typeof(Attachment))]
    public class Fuse : MonoBehaviour, IActivatable
    {
        public event Action OnDetonate;

        [SerializeField] private StrikerLever _strikerLever;
        [SerializeField] private SafetyRing _safetyRing;
        [SerializeField] private FuseType _fuseType;

        private Attachment _attachment;
        private bool _isRingDragged;

        public void Init()
        {
            _attachment = GetComponent<Attachment>();

            _safetyRing.Init();
            _strikerLever.Init();
            _isRingDragged = false;

            _safetyRing.OnDrag += OnDrag;
            _strikerLever.OnRelease += OnRelease;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public Attachment GetAttachment()
        {
            return _attachment;
        }

        public FuseType GetFuseType()
        {
            return (FuseType) _fuseType.Clone();
        }

        private void OnRelease()
        {
            if (!_isRingDragged) return;

            StartCoroutine(FuseDelay());
        }

        private IEnumerator FuseDelay()
        {
            yield return new WaitForSeconds(5f);

            OnDetonate?.Invoke();
            _strikerLever.OnRelease -= OnRelease;
        }

        private void OnDrag()
        {
            _isRingDragged = true;
            _safetyRing.OnDrag -= OnDrag;
        }

        private void Awake()
        {
            Init();
        }
    }
}