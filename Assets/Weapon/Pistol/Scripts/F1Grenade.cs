using Ammunition.Grenade;
using UnityEngine;

namespace Weapon.Pistol.Scripts
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(GrenadeView))]
    [RequireComponent(typeof(Striker))]
    [RequireComponent(typeof(GrenadeContainer))]
    public class F1Grenade : Grenade
    {
        public override void Init()
        {
            grenadeContainer = GetComponent<GrenadeContainer>();
            Striker = GetComponent<Striker>();
            grenadeView = GetComponent<GrenadeView>();
            attachment = GetComponent<Attachment>();

            attachment.Init();
            grenadeView.Init();
            Striker.Init();
            grenadeContainer.Init();
            _grenadeInserter.Init(grenadeContainer, attachment);
        }
    }
}