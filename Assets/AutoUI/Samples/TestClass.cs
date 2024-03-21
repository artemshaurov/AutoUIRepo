using System;
using System.Collections.Generic;

namespace Common.AutoUI.Samples
{
    [Serializable]
    public class TestClass
    {
        public bool Boolean1;
        public bool Boolean2;
        public int Integer1;
        public int Integer2;
        public string Text1;
        public string Text2;
        public float Float1;
        public float Float2;
        public TestEnum10 TestEnum10;
        public TestEnum15 TestEnum15;
        public TestEnum20 TestEnum20;
        public InnerTestClass0 InnerTestClass0;
        public InnerTestClass0 InnerTestClass1;
        public int[] NumbersArray;
        public List<bool> BooleansList;
        public List<string> Textes;
        public InnerTestClass0[] InnerClassesArray;
    }

    public enum TestEnum10
    {
        E0,
        E1,
        E2,
        E3,
        E4,
        
        E5,
        E6,
        E7,
        E8,
        E9,
    }
    
    public enum TestEnum15
    {
        E0,
        E1,
        E2,
        E3,
        E4,
        
        E5,
        E6,
        E7,
        E8,
        E9,
        
        E10,
        E11,
        E12,
        E13,
        E14,
    }
    
    public enum TestEnum20
    {
        E0,
        E1,
        E2,
        E3,
        E4,
        
        E5,
        E6,
        E7,
        E8,
        E9,
        
        E10,
        E11,
        E12,
        E13,
        E14,
        
        E15,
        E16,
        E17,
        E18,
        E19,
    }
}