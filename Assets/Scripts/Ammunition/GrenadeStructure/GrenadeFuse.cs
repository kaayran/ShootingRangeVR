using System;
using System.Collections;
using Interfaces;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(Attachment))]
    public class GrenadeFuse : MonoBehaviour, IActivatable
    {
        public event Action OnDetonate;

        [SerializeField] private GrenadeStrikerLever grenadeStrikerLever;
        [SerializeField] private GrenadeSafetyRing grenadeSafetyRing;
        [SerializeField] private GrenadeFuseType grenadeFuseType;

        private Attachment _attachment;
        private bool _isRingDragged;

        public void Init()
        {
            _attachment = GetComponent<Attachment>();

            grenadeSafetyRing.Init();
            grenadeStrikerLever.Init(_attachment);
            _isRingDragged = false;

            grenadeSafetyRing.OnDrag += OnDrag;
            grenadeStrikerLever.OnRelease += OnRelease;
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

        public GrenadeFuseType GetFuseType()
        {
            return (GrenadeFuseType) grenadeFuseType.Clone();
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
            grenadeStrikerLever.OnRelease -= OnRelease;
        }

        private void OnDrag()
        {
            _isRingDragged = true;
            grenadeSafetyRing.OnDrag -= OnDrag;
        }

        private void Awake()
        {
            Init();
        }
    }
}