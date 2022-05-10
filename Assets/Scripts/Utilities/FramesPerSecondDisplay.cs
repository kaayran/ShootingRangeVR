using System;
using TMPro;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(FramesPerSecondCounter))]
    public class FramesPerSecondDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _labelAverage;
        [SerializeField] private TextMeshProUGUI _labelMin;
        [SerializeField] private TextMeshProUGUI _labelMax;
        [SerializeField] private FramesPerSecondColor[] _colors;

        private FramesPerSecondCounter _counter;

        private readonly string[] _values =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
            "90"
        };

        private void Awake()
        {
            _counter = GetComponent<FramesPerSecondCounter>();
        }

        private void Update()
        {
            var averageValue = Mathf.Clamp(_counter.averageFramesPerSecond, 0, 90);
            var minValue = Mathf.Clamp(_counter.minFramesPerSecond, 0, 90);
            var maxValue = Mathf.Clamp(_counter.maxFramesPerSecond, 0, 90);

            DisplayColor(_labelAverage, averageValue);
            DisplayColor(_labelMin, minValue);
            DisplayColor(_labelMax, maxValue);
        }

        private void DisplayColor(TMP_Text label, int framesPerSecond)
        {
            label.text = _values[framesPerSecond];

            for (var i = 0; i < _colors.Length; i++)
            {
                if (framesPerSecond <= _colors[i].Minimum) continue;

                label.color = _colors[i].Color;
                break;
            }
        }

        [Serializable]
        private struct FramesPerSecondColor
        {
            public Color Color;
            public int Minimum;
        }
    }
}