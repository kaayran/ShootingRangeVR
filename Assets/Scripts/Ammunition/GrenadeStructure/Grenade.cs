using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public abstract class Grenade : MonoBehaviour
    {
        [SerializeField] protected GrenadeInserter GrenadeInserter;

        protected GrenadeContainer GrenadeContainer;
        protected GrenadeStriker GrenadeStriker;
        protected GrenadeView GrenadeView;
        protected Attachment Attachment;

        public abstract void Init();
    }
}