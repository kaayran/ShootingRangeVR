using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public abstract class Grenade : MonoBehaviour
    {
        [SerializeField] protected GrenadeLoader grenadeLoader;
        [SerializeField] protected GrenadeFuseType GrenadeFuseType;

        protected GrenadeFuseExtractor GrenadeFuseExtractor;
        protected GrenadeContainer GrenadeContainer;
        protected GrenadeExplosionView GrenadeExplosionView;
        protected GrenadeExplosion GrenadeExplosion;
        protected GrenadeView GrenadeView;
        protected Attachment Attachment;
        protected Popper Popper;

        public abstract void Init();
    }
}