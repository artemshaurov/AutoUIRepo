using System;
using System.Collections.Generic;

namespace Common.AutoUI.Runtime
{
    public interface IRootAutoNode : IAutoNode
    {
        IList<IAutoNode> GetChildren();
        Type FieldType { get; }
    }
}