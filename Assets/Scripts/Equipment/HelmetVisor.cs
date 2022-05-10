﻿using System;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Equipment
{
    public class HelmetVisor : MonoBehaviour
    {
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _time;

        private bool _inMaxRotation;
        private bool _inMinRotation;

        public void Init()
        {
            _inMaxRotation = false;
            _inMinRotation = true;
        }

        private IEnumerator RotateVisorUp()
        {
            var topRotation = Quaternion.Euler(0f, 0f, _maxAngle);
            var t = 0f;
            var dummy = 0f;

            while (t <= _time)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, topRotation, dummy);
                t += Time.deltaTime;
                dummy = t / _time;

                yield return null;
            }

            transform.localRotation = topRotation;

            _inMaxRotation = true;
            _inMinRotation = false;
        }

        private IEnumerator RotateVisorDown()
        {
            var rotation = Quaternion.Euler(0f, 0f, _minAngle);
            var t = 0f;
            var dummy = 0f;

            while (t <= _time)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, dummy);
                t += Time.deltaTime;
                dummy = t / _time;

                yield return null;
            }

            transform.localRotation = rotation;

            _inMinRotation = true;
            _inMaxRotation = false;
        }

        private void OnAttachedToHand(Hand hand)
        {
            if (!_inMaxRotation)
            {
                StartCoroutine(RotateVisorUp());
            }
            else if (!_inMinRotation)
            {
                StartCoroutine(RotateVisorDown());
            }
        }
    }
}