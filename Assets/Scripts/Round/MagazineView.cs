using UnityEngine;

namespace Round
{
    internal abstract class MagazineView : MonoBehaviour
    {
        protected MagazineCartridgeContainer MagazineCartridgeContainer;
        public abstract void Init(MagazineCartridgeContainer cartridgeContainer);
        public abstract void OnQuantityUpdate(int count);
    }
}