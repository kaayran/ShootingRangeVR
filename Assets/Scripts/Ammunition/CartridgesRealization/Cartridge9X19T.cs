using Ammunition.CartridgeStructure;
using StructureComponents;
using UnityEngine;

namespace Ammunition.CartridgesRealization
{
    public class Cartridge9X19T : Cartridge
    {
        public override void Init()
        {
            CartridgeAttachment = GetComponent<Attachment>();
            RigidBody = GetComponent<Rigidbody>();

            CartridgeAttachment.Init();

            CartridgeBullet.Init();
            CartridgeCase.Init();
        }

        private void Start()
        {
            Init();
        }
    }
}