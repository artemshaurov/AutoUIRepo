using System;
using UnityEngine;

namespace Common.AutoUI.Samples
{
    [Serializable]
    public class SafeData
    {
        [SerializeField] private int m_LastLevelShowedView = 0;
        public bool SafeWasShowed;
        public bool HalfSafe;
        public bool FullSafe;

        public int LastLevelShowedView => m_LastLevelShowedView;

        public void SetLevelShowView(int level)
        {
            m_LastLevelShowedView = level;
        }
    }
}