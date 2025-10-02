
    using System;

    public class SettingDTO
    {
        public int bgmVolume = 3;
        public int effectVolume = 3;
        public float thresholdPrecent = 1.0f;
        public const float nod_default = 0.02f;
        public const float shake_default = 0.022f;
        
        public string GetThresholdCmd(float percent)
        {
            return $"UpdateThreshold {Math.Round(nod_default * (1/percent), 4)} {Math.Round(shake_default * (1/percent), 4)}";
        }
    }
