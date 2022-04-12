using TMPro;
using UnityEngine;

namespace Round
{
    internal class MagazineViewUI : MagazineView
    {
        [SerializeField] private TextMeshProUGUI _leftText;

        public override void Init(MagazineContainer container)
        {
            magazineContainer = container;
            _leftText.text = "LEFT: " + container;
            magazineContainer.OnQuantityUpdate += OnQuantityUpdate;
        }

        public override void OnQuantityUpdate(int count)
        {
            _leftText.text = "LEFT: " + count;
        }
    }
}