using Round;
using UnityEngine;

namespace Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Loader _loader;
        [SerializeField] protected Slide _slide;

        protected Container<Magazine, MagazineType> container;
        protected ChamberEjector chamberEjector;
        protected Attachment attachment;
        protected FiringPin firingPin;
        protected Extractor extractor;
        protected Trigger trigger;
        protected Chamber chamber;
        protected Barrel barrel;
        protected Popper popper;
        public abstract void Init();
    }
}