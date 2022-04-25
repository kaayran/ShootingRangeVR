using UnityEngine;

namespace Utilities
{
    public class FramesPerSecondCounter : MonoBehaviour
    {
        public int averageFramesPerSecond { get; private set; }
        public int minFramesPerSecond { get; private set; }
        public int maxFramesPerSecond { get; private set; }

        private int _frameRange = 60;
        private int[] _framesBuffer;
        private int _frameIndex;

        private void Update()
        {
            if (_framesBuffer == null || _frameRange != _framesBuffer.Length)
                InitializeBuffer();

            UpdateFrameBuffer();
            CalculateFramesPerSecond();
        }

        private void InitializeBuffer()
        {
            if (_frameRange <= 0) _frameRange = 1;

            _framesBuffer = new int[_frameRange];
            _frameIndex = 0;
        }

        private void UpdateFrameBuffer()
        {
            _framesBuffer[_frameIndex++] = (int) (1f / Time.unscaledDeltaTime);

            if (_frameIndex >= _frameRange)
            {
                _frameIndex = 0;
            }
        }

        private void CalculateFramesPerSecond()
        {
            var sum = 0;
            var min = int.MaxValue;
            var max = 0;

            for (var i = 0; i < _frameRange; i++)
            {
                var value = _framesBuffer[i];

                if (value > max) max = value;
                if (value < min) min = value;

                sum += value;
            }

            averageFramesPerSecond = sum / _frameRange;
            minFramesPerSecond = min;
            maxFramesPerSecond = max;
        }
    }
}