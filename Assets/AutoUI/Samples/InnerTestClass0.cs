using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Common.AutoUI.Samples
{
    [Serializable, JsonObject(MemberSerialization.Fields)]
    public class InnerTestClass0
    {
        [SerializeField] private bool m_Boolean1;
        [SerializeField] private int m_Integer1;
        [SerializeField] private string m_Text1;
        [SerializeField] private float m_Float1;
        
    }
}