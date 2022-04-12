using UnityEngine;
using Weapon;

namespace Ammunition.Grenade
{
    public abstract class Grenade : MonoBehaviour
    {
        [SerializeField] protected GrenadeInserter _grenadeInserter;

        protected GrenadeContainer grenadeContainer;
        protected Striker Striker;
        protected GrenadeView grenadeView;
        protected Attachment attachment;

        public abstract void Init();
    }
}