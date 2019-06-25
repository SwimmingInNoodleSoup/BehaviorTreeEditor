﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTreeData
{
    public partial class RepeatLongField : BaseFiled
    {
        public List<long> Value;

        public override void Read(ref Reader reader)
        {
            reader.Read(ref FieldName).Read(ref Value);
        }

        public override void Write(ref Writer writer)
        {
            writer.Write(FieldName).Write(Value);
        }
    }
}
