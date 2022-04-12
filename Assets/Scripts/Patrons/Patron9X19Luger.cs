using Ammo;
using Weapon;

namespace Patrons
{
    public class Patron9X19Luger : Patron
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
