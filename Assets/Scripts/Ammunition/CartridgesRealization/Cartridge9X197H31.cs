using Weapon;

namespace Ammunition.CartridgesRealization
{
    public class Cartridge9X197H31 : Cartridge.Cartridge
    {
        public override void Init()
        {
            CartridgeAttachment = GetComponent<Attachment>();

            CartridgeAttachment.Init();

            _bullet.Init();
            _cartridgeCase.Init();
        }

        private void Start()
        {
            Init();
        }
    }
}