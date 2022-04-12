using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using Weapon;

namespace Nade
{
    [RequireComponent(typeof(Attachment))]
    public class Fuse : MonoBehaviour, IActivatable
    {
        public event Action OnDetonate;

        [SerializeField] private FuseTrigger _fuseTrigger;
        [SerializeField] private FuseRing _fuseRing;
        [SerializeField] private FuseType _fuseType;

        private Attachment _attachment;
        private bool _isRingDragged;

        public void Init()
        {
            _attachment = GetComponent<Attachment>();

            _fuseRing.Init();
            _fuseTrigger.Init();
            _isRingDragged = false;

            _fuseRing.OnDrag += OnDrag;
            _fuseTrigger.OnRelease += OnRelease;
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
            _fuseTrigger.OnRelease -= OnRelease;
        }

        private void OnDrag()
        {
            _isRingDragged = true;
            _fuseRing.OnDrag -= OnDrag;
        }

        private void Awake()
        {
            Init();
        }
    }
}