using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public abstract class Grenade : MonoBehaviour
    {
        [SerializeField] protected GrenadeExplosionView ExplosionView;
        [SerializeField] protected GrenadeLoader Loader;

        protected Container<GrenadeFuse, GrenadeFuseType> Container;
        protected GrenadeExplosion Explosion;
        protected GrenadeExtractor Extractor;
        protected GrenadeAudio GrenadeAudio; 

        protected Attachment Attachment;
        protected Popper Popper;
    }
}