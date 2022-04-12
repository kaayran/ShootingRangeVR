using UnityEngine;

namespace MagazineStructure
{
    [CreateAssetMenu(fileName = "MAGAZINE_NAME", menuName = "Magazine Type", order = 0)]
    public class MagazineType : ScriptableObject
    {
        public string MagazineName;

        public MagazineType(string magazineName)
        {
            MagazineName = magazineName;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}