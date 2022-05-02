using TMPro;
using UnityEngine;

namespace Utilities.Logger
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InGameLoggerView : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            InGameLogger.Clear += OnClearLog;
            InGameLogger.DataSend += OnDataSend;
        }

        private void OnDataSend(string message, bool append)
        {
            if (append)
            {
                _text.text += "\n" + message;
            }
            else
            {
                _text.text = message;
            }
        }

        private void OnClearLog()
        {
            _text.text = string.Empty;
        }
    }
}