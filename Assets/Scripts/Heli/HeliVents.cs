using UnityEngine;

namespace Heli
{
    public class HeliVents : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private void Update()
        {
            transform.Rotate(Vector3.up * _speed * Time.deltaTime);
        }
    }
}
