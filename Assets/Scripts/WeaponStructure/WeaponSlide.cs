using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace WeaponStructure
{
    public class WeaponSlide : MonoBehaviour
    {
        public event Action OnForward;
        public event Action OnBackward;

        [SerializeField] private float _distance;
        [SerializeField] private float _time = 0.25f;

        private Transform _startPos;
        private Transform _endPos;

        private Hand _attachedHand;
        private WeaponBarrel _weaponBarrel;

        private Coroutine _moveSlide;
        private Coroutine _moveBackwardSlide;
        private Coroutine _moveForwardSlide;

        private float _difference;
        private bool _isInPosition;
        private WeaponAudio _weaponAudio;

        public void Init(WeaponBarrel weaponBarrel, WeaponAudio weaponAudio)
        {
            _weaponAudio = weaponAudio;
            _weaponBarrel = weaponBarrel;

            CreateSlidePositions();

            _weaponBarrel.OnBulletFired += BackwardSlideDeploy;
        }

        public bool IsInPosition()
        {
            return _isInPosition;
        }

        public void ForwardSlide()
        {
            ForwardSlideDeploy();
        }

        // Move slide to back position
        private void BackwardSlideDeploy()
        {
            if (_attachedHand == null) _moveBackwardSlide = StartCoroutine(MoveSlideBackward());
        }

        private void ForwardSlideDeploy()
        {
            if (_attachedHand == null) _moveForwardSlide = StartCoroutine(MoveSlideForward());
        }

        private void CreateSlidePositions()
        {
            var obj = new GameObject("startPos");
            var gateTransform = transform;
            var position = gateTransform.position;
            var rotation = gateTransform.rotation;
            var parentTransform = gateTransform.parent;

            _startPos = Instantiate(obj, position, rotation, parentTransform).transform;
            obj.name = "endPos";
            _endPos = Instantiate(obj, position - transform.forward * _distance, rotation,
                parentTransform).transform;

            Destroy(obj);
        }

        private IEnumerator MoveSlide()
        {
            _isInPosition = false;

            while (true)
            {
                var gateTransform = transform;
                var forward = gateTransform.forward;
                var toHand = _attachedHand.transform.position - gateTransform.position;
                var localPosition = gateTransform.localPosition;
                var z = Vector3.Dot(forward, toHand) + localPosition.z - _difference;

                var pos = new Vector3(localPosition.x, localPosition.y, z);

                if (z <= _startPos.localPosition.z && z >= _endPos.localPosition.z)
                    transform.localPosition = pos;

                // FOR TESTS!
                if (Vector3.Distance(transform.position, _endPos.position) < 0.01f)
                {
                    OnBackward?.Invoke();
                }

                yield return null;
            }
        }

        private IEnumerator MoveSlideForward()
        {
            var t = 0f;
            var dummy = 0f;
            var startPosLocalPosition = _startPos.localPosition;
            var localPosition = transform.localPosition;
            var distance = Math.Abs(startPosLocalPosition.z - localPosition.z);
            var maxDistance = Math.Abs(startPosLocalPosition.z - _endPos.localPosition.z);
            var part = distance / maxDistance;
            var timeToMove = part * _time;

            while (t <= timeToMove)
            {
                transform.localPosition = Vector3.Lerp(localPosition, startPosLocalPosition, dummy);
                t += Time.deltaTime;
                dummy = t / timeToMove;

                yield return null;
            }

            transform.position = _startPos.position;
            _isInPosition = true;
            OnForward?.Invoke();
            _weaponAudio.PlayForwardSlideSound();
        }

        private IEnumerator MoveSlideBackward()
        {
            _isInPosition = false;

            var t = 0f;
            var dummy = 0f;
            var endPosLocalPosition = _endPos.localPosition;
            var localPosition = transform.localPosition;
            var distance = Math.Abs(endPosLocalPosition.z - localPosition.z);
            var maxDistance = Math.Abs(endPosLocalPosition.z - _startPos.localPosition.z);
            var part = distance / maxDistance;
            var timeToMove = part * _time;

            while (t <= timeToMove)
            {
                transform.localPosition = Vector3.Lerp(localPosition, endPosLocalPosition, dummy);
                t += Time.deltaTime;
                dummy = t / timeToMove;

                yield return null;
            }

            transform.position = _endPos.position;
            OnBackward?.Invoke();
            _weaponAudio.PlayBackwardSlideSound();
        }

        [UsedImplicitly]
        private void OnAttachedToHand(Hand hand)
        {
            _attachedHand = hand;

            var gateMoverTransform = transform;
            var forward = gateMoverTransform.forward;
            var toHand = _attachedHand.transform.position - gateMoverTransform.position;
            _difference = Vector3.Dot(forward, toHand);

            if (_moveForwardSlide != null) StopCoroutine(_moveForwardSlide);
            if (_moveBackwardSlide != null) StopCoroutine(_moveBackwardSlide);

            _moveSlide = StartCoroutine(MoveSlide());
        }

        [UsedImplicitly]
        private void OnDetachedFromHand(Hand hand)
        {
            if (_moveSlide != null) StopCoroutine(_moveSlide);
            _attachedHand = null;

            if (_moveForwardSlide != null) StopCoroutine(_moveForwardSlide);
            if (_moveBackwardSlide != null) StopCoroutine(_moveBackwardSlide);

            _moveForwardSlide = StartCoroutine(MoveSlideForward());
        }
    }
}