using Round;

namespace Weapon.Pistol.Scripts
{
    public class MagazineGlock9X19 : Magazine
    {
        public override void Init()
        {
            magazineCartridgeContainer = GetComponent<MagazineCartridgeContainer>();
            magazineCartridgeExtractor = GetComponent<MagazineCartridgeExtractor>();
            magazinePopper = GetComponent<Popper>();
            magazineAttachment = GetComponent<Attachment>();

            magazineCartridgeContainer.Init();
            magazineAttachment.Init();
            magazinePopper.Init(magazineAttachment);
            magazineCartridgeExtractor.Init(magazineCartridgeContainer, magazinePopper, magazineAttachment);
            magazineCartridgeInserter.Init(magazineCartridgeContainer, magazineAttachment);
            magazineView.Init(magazineCartridgeContainer);
        }

        private void Start()
        {
            Init();
        }
    }
}