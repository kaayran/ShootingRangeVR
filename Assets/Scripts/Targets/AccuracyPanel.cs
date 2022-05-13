using System;
using TMPro;
using UnityEngine;

namespace Targets
{
    public class AccuracyPanel : MonoBehaviour
    {
        private TextMeshProUGUI _label;
        private HumanTarget _target;
        private int _accuracy = 0;
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

            _accuracy = _count == 1 ? accuracy : (_accuracy + accuracy) / 2;

            _label.text = $"[{_accuracy}%]\n[{_count}]";
        }
    }
}