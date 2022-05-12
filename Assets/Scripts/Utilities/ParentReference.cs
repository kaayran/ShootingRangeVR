using UnityEngine;

namespace Utilities
{
    public abstract class ParentReference<T> : MonoBehaviour
    {
        private T _reference;

        public void Init(T reference)
        {
            _reference = reference;
        }

        public T GetReference() => _reference;
    }
}