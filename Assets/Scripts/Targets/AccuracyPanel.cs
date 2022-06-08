using TMPro;
using UnityEngine;

namespace Targets
{
    public class AccuracyPanel : MonoBehaviour
    {
        private TextMeshProUGUI _label;
        private HumanTarget _target;
        private int _sumAccuracy = 0;
        private int _count = 0;

        public void Init(HumanTarget target)
        {
            _label = GetComponent<TextMeshProUGUI>();
            _label.text = "[0%]\n[0]";

            _target = target;
            _target.OnHit += HitUpdateInfo;
        }

        private void HitUpdateInfo(int accuracy, int count)
        {
            _count = count;

            _sumAccuracy += accuracy;

            var resultAccuracy = _sumAccuracy / _count;

            _label.text = $"[{resultAccuracy}%]\n[{_count}]";
        }
    }
}