using UnityEngine;

namespace Round
{
    internal abstract class MagazineView : MonoBehaviour
    {
        protected MagazineContainer magazineContainer;
        public abstract void Init(MagazineContainer container);
        public abstract void OnQuantityUpdate(int count);
    }
}