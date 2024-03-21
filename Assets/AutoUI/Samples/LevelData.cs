using System;

namespace Common.AutoUI.Samples
{
    [Serializable]
    public class LevelData
    {
        public int currentLevelId = 1;
        public LevelDifficulty currentLevelDifficulty;
        public int currentLevelVersion;
        public int currentLevelRetryCount;
    
        public int levelsCount = 100; // доступное кол-во уровней
    }
}