using System;
using Ammunition.GrenadeStructure;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadesRealization
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(GrenadeView))]
    [RequireComponent(typeof(GrenadeContainer))]
    public class F1Grenade : Grenade
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            GrenadeContainer = GetComponent<GrenadeContainer>();
            GrenadeView = GetComponent<GrenadeView>();
            Attachment = GetComponent<Attachment>();

            Attachment.Init();
            GrenadeView.Init();
            GrenadeContainer.Init();
            GrenadeInserter.Init(GrenadeContainer, Attachment);
        }
    }
}