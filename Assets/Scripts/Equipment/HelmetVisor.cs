using System;
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

        private Hand _attachedHand;

        public void Init()
        {
        }

        private IEnumerator UpVisor()
        {
            var angle = transform.eulerAngles.z;
            var road = angle / _maxAngle;
            var temp = 0f;

            while (temp <= _time)
            {
                angle = Mathf.LerpAngle(angle, _maxAngle, temp);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
                temp += road * _time / Time.deltaTime;

                yield return null;
            }

            Debug.Log("Get to Max Angle");
        }

        private void OnAttachedToHand(Hand hand)
        {
            _attachedHand = hand;

            var up = transform.up;
            var toHand = _attachedHand.transform.position - transform.position;
            var dot = Vector3.Dot(up, toHand);

            StartCoroutine(UpVisor());
        }

        private void OnDetachedFromHand(Hand hand)
        {
            _attachedHand = null;
        }
    }
}