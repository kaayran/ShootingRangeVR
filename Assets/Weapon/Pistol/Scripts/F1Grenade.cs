using Nade;
using UnityEngine;

namespace Weapon.Pistol.Scripts
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(GrenadeView))]
    [RequireComponent(typeof(GrenadeExploder))]
    [RequireComponent(typeof(GrenadeContainer))]
    public class F1Grenade : Grenade
    {
        public override void Init()
        {
            _grenadeContainer = GetComponent<GrenadeContainer>();
            _grenadeExploder = GetComponent<GrenadeExploder>();
            _grenadeView = GetComponent<GrenadeView>();
            _attachment = GetComponent<Attachment>();

            _attachment.Init();
            _grenadeView.Init();
            _grenadeExploder.Init();
            _grenadeContainer.Init();
            _grenadeInserter.Init(_grenadeContainer, _attachment);
        }
    }
}