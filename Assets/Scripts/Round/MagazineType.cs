using UnityEngine;

namespace Round
{
    [CreateAssetMenu(fileName = "MAGAZINE_TYPE", menuName = "Magazine", order = 0)]
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