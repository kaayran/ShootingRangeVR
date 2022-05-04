using System.Collections.Generic;
using UnityEngine;

namespace StructureComponents
{
    public class CollisionIgnoring : MonoBehaviour
    {
        private List<Collider> _colliders;

        public void Init()
        {
            _colliders = new List<Collider>(GetComponentsInChildren<Collider>());

            for (var i = 0; i < _colliders.Count - 1; i++)
                _colliders.GetRange(i + 1, _colliders.Count - i - 1).ForEach(col =>
                    Physics.IgnoreCollision(_colliders[i], col));
        }

        public List<Collider> GetColliders()
        {
            return _colliders;
        }
    }
}