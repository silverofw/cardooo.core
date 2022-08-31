using UnityEngine;
namespace cardooo.core
{
    public class DLog
    {
        private static bool isOn = true;

        public static void SetOnOff(bool value)
        {
            isOn = value;
        }
        public static void Log(string msg)
        {
            if (!isOn)
                return;
            Debug.Log($"[{Time.frameCount}] {msg}");
        }
        public static void Log(string msg, params object[] args)
        {
            if (!isOn)
                return;
            Debug.LogFormat($"[{Time.frameCount}] {msg}", args);
        }

        public static void LogError(string msg)
        {
            if (!isOn)
                return;
            Debug.LogError($"[{Time.frameCount}] {msg}");
        }
        public static void LogErrorFormat(string msg, params object[] args)
        {
            if (!isOn)
                return;
            Debug.LogErrorFormat($"[{Time.frameCount}] {msg}", args);
        }
    }
}
