using UnityEngine;

namespace Ammunition.Grenade
{
    [CreateAssetMenu(fileName = "FUSE_TYPE", menuName = "Fuse", order = 0)]
    public class FuseType : ScriptableObject
    {
        public string FuseName;

        public FuseType(string fuseName)
        {
            FuseName = fuseName;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}