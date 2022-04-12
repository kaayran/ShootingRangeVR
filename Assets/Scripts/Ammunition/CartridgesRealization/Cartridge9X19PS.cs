using Ammunition.CartridgeStructure;
using StructureComponents;

namespace Ammunition.CartridgesRealization
{
    public class Cartridge9X19PS : Cartridge
    {
        public override void Init()
        {
            CartridgeAttachment = GetComponent<Attachment>();

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
