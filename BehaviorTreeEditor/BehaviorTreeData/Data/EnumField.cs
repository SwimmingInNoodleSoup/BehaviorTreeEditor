﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTreeData
{
    public partial class EnumField : BaseFiled
    {
        public string EnumType;
        public float Value;

        public override void Read(ref Reader reader)
        {
            reader.Read(ref FieldName).Read(ref EnumType).Read(ref Value);
        }

        public override void Write(ref Writer writer)
        {
            writer.Write(FieldName).Write(EnumType).Write(Value);
        }
    }
}
