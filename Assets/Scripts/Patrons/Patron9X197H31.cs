using Ammo;
using Weapon;

namespace Patrons
{
    public class Patron9X197H31 : Patron
    {
        public override void Init()
        {
            patronAttachment = GetComponent<Attachment>();

            patronAttachment.Init();

            _bullet.Init();
            _cartridge.Init();
        }

        private void Start()
        {
            Init();
        }
    }
}
