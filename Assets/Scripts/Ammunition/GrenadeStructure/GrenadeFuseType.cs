using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    [CreateAssetMenu(fileName = "FUSE_TYPE", menuName = "Fuse Type", order = 0)]
    public class GrenadeFuseType : ScriptableObject
    {
        public string FuseName;

        public GrenadeFuseType(string fuseName)
        {
            FuseName = fuseName;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}