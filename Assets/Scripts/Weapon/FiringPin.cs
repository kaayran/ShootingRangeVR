using Round;
using UnityEngine;

namespace Weapon
{
    public class FiringPin : MonoBehaviour
    {
        private Container<Magazine, MagazineType> _container;
        private ChamberEjector _ejector;
        private Chamber _chamber;
        private Trigger _trigger;
        private Barrel _barrel;
        private Slide _slide;

        public void Init(Container<Magazine, MagazineType> container, ChamberEjector ejector, Chamber chamber,
            Trigger trigger, Barrel barrel,
            Slide slide)
        {
            _container = container;
            _ejector = ejector;
            _chamber = chamber;
            _trigger = trigger;
            _barrel = barrel;
            _slide = slide;

            _trigger.OnTriggerPressed += OnTriggerPressed;

            _slide.OnForward += OnForward;
            _slide.OnBackward += OnBackward;
        }

        private void OnTriggerPressed()
        {
            if (!_slide.IsInPosition()) return;

            if (!_chamber.TryPopPatron(out var patron)) return;

            var cartridge = patron.GetCartridge();
            _ejector.SetCartridge(cartridge);
            
            _barrel.Fire(patron);
        }

        private void OnBackward()
        {
            var magazine = _container.GetStored();
            if (magazine == null) return;
            if (!magazine.CheckPatron()) return;

            _slide.ForwardSlide();
        }

        private void OnForward()
        {
            // Check is patron in chamber -> then return
            if (_chamber.CheckPatron()) return;

            var magazine = _container.GetStored();

            if (magazine == null) return;

            // Try get patron from magazine
            if (magazine.TryPopPatron(out var patron))
            {
                _chamber.TrySetPatron(patron);
            }
        }
    }
}