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
        public event Action OnDetonate;

        [SerializeField] private GrenadeFuseStrikerLever _grenadeFuseStrikerLever;
        [SerializeField] private GrenadeSafetyRing _grenadeSafetyRing;
        [SerializeField] private GrenadeFuseType _grenadeFuseType;

        private Attachment _attachment;
        private CollisionIgnoring _collisionIgnoring;
        private GrenadeStriker _grenadeStriker;

        private bool _isLeverLocked;
        private bool _isRingDragged;

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

            _grenadeSafetyRing.OnDrag += OnDrag;
            _grenadeFuseStrikerLever.OnLock += OnLock;
            _grenadeFuseStrikerLever.OnRelease += OnRelease;
        }

        private void OnLock()
        {
            InGameLogger.Log("Lever Locked", true);
            _isLeverLocked = true;
            // Switch state, on action from lever,
            // we can freely lock then unlock lever
        }

        private void OnRelease()
        {
            InGameLogger.Log("Lever Released", true);
            _isLeverLocked = false;
            if (!_isRingDragged) return;
            // If we release lever, then we check is ring already dragged?
            // Then detonate
            StartCoroutine(FuseDelay());
        }

        private void OnDrag()
        {
            InGameLogger.Log("Ring Dragged", true);
            _isRingDragged = true;
            _grenadeSafetyRing.OnDrag -= OnDrag;

            if (_isLeverLocked) return;
            // Explode anyway, if ring dragged, and lever is not locked

            StartCoroutine(FuseDelay());
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

        private IEnumerator FuseDelay()
        {
            InGameLogger.Log("Start Fuse Delay", true);

            _grenadeFuseStrikerLever.OnRelease -= OnRelease;
            _grenadeFuseStrikerLever.OnLock -= OnLock;

            yield return new WaitForSeconds(5f);

            OnDetonate?.Invoke();
        }

        private void Awake()
        {
            Init();
        }
    }
}