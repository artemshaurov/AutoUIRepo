namespace Common.AutoUI.Samples
{
    [System.Serializable]
    public class SettingsData
    {
        public bool IsSoundOn = false;
        public bool IsMusicOn = false;
        public bool IsVibroOn = true;
        public int IsNotificationPopupShovedLevel = 0;
        public bool IsNotificationAllowed = false;
        public string Name = "Player";
        public bool Rated;
        public int IsRateUsPopupShovedLevel = 0;
    }
}