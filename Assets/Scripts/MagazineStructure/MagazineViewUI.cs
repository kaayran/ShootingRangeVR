using TMPro;
using UnityEngine;

namespace MagazineStructure
{
    internal class MagazineViewUI : MagazineView
    {
        [SerializeField] private TextMeshProUGUI _leftText;

        public override void Init(MagazineCartridgeContainer cartridgeContainer)
        {
            MagazineCartridgeContainer = cartridgeContainer;
            _leftText.text = "LEFT: " + cartridgeContainer.GetQuantity();

            MagazineCartridgeContainer.OnQuantityUpdate += OnQuantityUpdate;
        }

        public override void OnQuantityUpdate(int count)
        {
            _leftText.text = "LEFT: " + count;
        }
    }
}