using System;
using UnityEngine;

namespace StructureComponents
{
    public abstract class Container<T, T1> : MonoBehaviour
    {
        public abstract event Action OnPopped;
        public abstract event Action OnEntered;

        public abstract void Init();
        public abstract bool TryPop(out T fuse);
        public abstract bool TryPush(T t);
        public abstract T GetStored();
        public abstract T1 GetStoredType();
    }
}