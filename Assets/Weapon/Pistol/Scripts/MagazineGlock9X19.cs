using Round;

namespace Weapon.Pistol.Scripts
{
    public class MagazineGlock9X19 : Magazine
    {
        public override void Init()
        {
            magazineContainer = GetComponent<MagazineContainer>();
            magazineExtractor = GetComponent<MagazineExtractor>();
            magazinePopper = GetComponent<Popper>();
            magazineAttachment = GetComponent<Attachment>();

            magazineContainer.Init();
            magazineAttachment.Init();
            magazinePopper.Init(magazineAttachment);
            magazineExtractor.Init(magazineContainer, magazinePopper, magazineAttachment);
            _magazineInserter.Init(magazineContainer, magazineAttachment);
            _magazineView.Init(magazineContainer);
        }

        private void Start()
        {
            Init();
        }
    }
}