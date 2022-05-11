using UnityEngine;

namespace Targets
{
    public class HumanTarget : MonoBehaviour
    {
        [SerializeField] private Transform _forwardPos;
        [SerializeField] private Transform _backwardPos;

        private float _speed;

        public void Init(float speed)
        {
            _speed = speed;
        }

        public void MoveForward()
        {
            if (transform.position.z >= _forwardPos.position.z)
            {
                transform.position = _forwardPos.position;
                return;
            }

            transform.Translate(transform.forward * (_speed * Time.deltaTime));
        }

        public void MoveBackward()
        {
            if (transform.position.z <= _backwardPos.position.z)
            {
                transform.position = _backwardPos.position;
                return;
            }

            transform.Translate(transform.forward * (-_speed * Time.deltaTime));
        }
    }
}