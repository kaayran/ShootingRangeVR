using UnityEngine;
using Weapon;

namespace Nade
{
    public abstract class Grenade : MonoBehaviour
    {
        [SerializeField] protected GrenadeInserter _grenadeInserter;
        
        protected GrenadeContainer _grenadeContainer;
        protected GrenadeExploder _grenadeExploder;
        protected GrenadeView _grenadeView;
        protected Attachment _attachment;

        public abstract void Init();
    }
}