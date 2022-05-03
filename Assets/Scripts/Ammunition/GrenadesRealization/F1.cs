﻿using Ammunition.GrenadeStructure;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadesRealization
{
    [RequireComponent(typeof(GrenadeContainer))]
    [RequireComponent(typeof(GrenadeExplosion))]
    [RequireComponent(typeof(GrenadeExtractor))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Throwable))]
    [RequireComponent(typeof(Popper))]
    public class F1 : Grenade
    {
        [SerializeField] private Collider _collider;

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            Attachment = GetComponent<Attachment>();
            Popper = GetComponent<Popper>();

            Attachment.Init();
            Popper.Init(Attachment);

            Container = GetComponent<Container<GrenadeFuse, GrenadeFuseType>>();
            Explosion = GetComponent<GrenadeExplosion>();
            Extractor = GetComponent<GrenadeExtractor>();

            Container.Init();
            Loader.Init(Container, Attachment, _collider);
            Extractor.Init(Container, Attachment, Popper, _collider);
            Explosion.Init(Container);
            ExplosionView.Init(Explosion);
            ExplosionParticle.Init(Explosion);
        }
    }
}