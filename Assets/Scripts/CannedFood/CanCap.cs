using StructureComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannedFood
{
    [RequireComponent(typeof(Rigidbody))]
    public class CanCap : MonoBehaviour
    {
        [SerializeField] private int _takesToOpen;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioOneShot _audioOneShot;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<KnifeRazor>(out _)) return;

            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_open, 0.3f, Random.Range(0.9f, 1.1f));
            audioOneShot.Play();
            
            if (_takesToOpen-- == 1) return;
            
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.up, ForceMode.Impulse);
            _rigidbody.AddTorque(ForceTorque(), ForceMode.Impulse);
            
            Destroy(this, 5f);
        }
        
        private Vector3 ForceTorque()
        {
            var xTorque = Random.Range(0f, 30f);
            var yTorque = Random.Range(0f, 30f);
            var zTorque = Random.Range(0f, 30f);
            var torque = new Vector3(xTorque, yTorque, zTorque);
            return torque;
        }
    }
}