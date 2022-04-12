using System;
using UnityEngine;

namespace Weapon
{
    public abstract class Container<T, T1> : MonoBehaviour
    {
        public abstract event Action OnPopped;
        public abstract event Action OnEntered;

        public abstract void Init();
        public abstract bool TryPop(out T t);
        public abstract bool TryPush(T t);

        public abstract T GetStored();
        public abstract bool CheckStored();
        public abstract T1 GetStoredType();
    }
}