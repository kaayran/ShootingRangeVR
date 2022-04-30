using System;
using UnityEngine;

namespace Utilities.Logger
{
    public static class InGameLogger
    {
        public static Action<string, bool> DataSend;
        public static Action Clear;

        public static void Log(string message, bool append)
        {
            if (append)
            {
                DataSend?.Invoke(message, true);

                Debug.Log(message);
            }
            else
            {
                DataSend?.Invoke(message, false);

                Debug.Log(message);
                Debug.ClearDeveloperConsole();
            }
        }

        public static void Log(string message)
        {
            DataSend?.Invoke(message, false);

            Debug.Log(message);
            Debug.ClearDeveloperConsole();
        }

        public static void ClearLog()
        {
            Clear?.Invoke();
        }
    }
}