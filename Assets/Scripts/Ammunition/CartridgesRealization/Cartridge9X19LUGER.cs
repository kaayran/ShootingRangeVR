using Ammunition.CartridgeStructure;
using StructureComponents;
using UnityEngine;

namespace Ammunition.CartridgesRealization
{
    public class Cartridge9X19LUGER : Cartridge
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            CartridgeAttachment = GetComponent<Attachment>();
            RigidBody = GetComponent<Rigidbody>();

            CartridgeAttachment.Init();

            CartridgeBullet.Init();
            CartridgeCase.Init();
        }
    }
}