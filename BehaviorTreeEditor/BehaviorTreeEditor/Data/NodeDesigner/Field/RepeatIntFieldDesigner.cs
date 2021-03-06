﻿using System.Collections.Generic;
using System.ComponentModel;

namespace BehaviorTreeEditor
{
    public class RepeatIntFieldDesigner : BaseFieldDesigner
    {
        private List<int> m_Value = new List<int>();

        [Category("常规"), DisplayName("Int数组"),Description("Int数组")]
        public List<int> Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public override string ToString()
        {
            string content = string.Empty;
            content += "[";
            for (int i = 0; i < m_Value.Count; i++)
            {
                content += m_Value[i] + (i < m_Value.Count - 1 ? "," : string.Empty);
            }
            content += "]";
            return content;
        }
    }
}
