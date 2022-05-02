using System;
using System.Collections;
using Interfaces;
using StructureComponents;
using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(GrenadeStriker))]
    [RequireComponent(typeof(CollisionIgnoring))]
    [RequireComponent(typeof(Attachment))]
    public class GrenadeFuse : MonoBehaviour, IActivatable
    {
        public Action Detonate;

        [SerializeField] private GrenadeFuseStrikerLever _grenadeFuseStrikerLever;
        [SerializeField] private GrenadeSafetyRing _grenadeSafetyRing;
        [SerializeField] private GrenadeFuseType _grenadeFuseType;

        private Attachment _attachment;
        private CollisionIgnoring _collisionIgnoring;
        private bool _isRingDragged;
        private GrenadeStriker _grenadeStriker;

        public void Init()
        {
            _collisionIgnoring = GetComponent<CollisionIgnoring>();
            _attachment = GetComponent<Attachment>();

            _attachment.Init();
            _collisionIgnoring.Init();

            // Something wrong here!
            _grenadeStriker = GetComponent<GrenadeStriker>();
            _grenadeStriker.Init();

            _grenadeSafetyRing.Init();
            _grenadeFuseStrikerLever.Init(_attachment);
            _isRingDragged = false;

            _grenadeSafetyRing.OnDrag += OnDrag;
            _grenadeFuseStrikerLever.OnRelease += OnRelease;
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
            return (GrenadeFuseType) _grenadeFuseType.Clone();
        }

        private void OnRelease()
        {
            if (!_isRingDragged) return;

            StartCoroutine(FuseDelay());
        }

        private IEnumerator FuseDelay()
        {
            _grenadeFuseStrikerLever.OnRelease -= OnRelease;
            yield return new WaitForSeconds(5f);

            Detonate?.Invoke();
        }

        private void OnDrag()
        {
            InGameLogger.Log("Ring Dragged", true);
            _isRingDragged = true;
            _grenadeSafetyRing.OnDrag -= OnDrag;
        }

        private void Awake()
        {
            Init();
        }
    }
}