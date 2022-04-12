using Ammunition.GrenadeStructure;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadesRealization
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(GrenadeView))]
    [RequireComponent(typeof(GrenadeStriker))]
    [RequireComponent(typeof(GrenadeContainer))]
    public class F1Grenade : Grenade
    {
        public override void Init()
        {
            GrenadeContainer = GetComponent<GrenadeContainer>();
            GrenadeStriker = GetComponent<GrenadeStriker>();
            GrenadeView = GetComponent<GrenadeView>();
            Attachment = GetComponent<Attachment>();

            Attachment.Init();
            GrenadeView.Init();
            GrenadeStriker.Init();
            GrenadeContainer.Init();
            GrenadeInserter.Init(GrenadeContainer, Attachment);
        }
    }
}