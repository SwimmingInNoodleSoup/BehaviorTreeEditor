﻿using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace BehaviorTreeEditor
{
    public class EnumFieldDesigner : BaseFieldDesigner
    {
        public EnumFieldDesigner()
        {
            if (MainForm.Instance.NodeTemplate == null)
                return;

            if (MainForm.Instance.NodeTemplate.Enums.Count > 0)
            {
                CustomEnum customEnum = MainForm.Instance.NodeTemplate.Enums[0];
                EnumType = customEnum.EnumType;
            }
        }

        private string m_EnumType;
        private string m_Value;

        [Browsable(false)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [Category("常规"), DisplayName("枚举类型"), Description("枚举类型")]
        public string EnumType
        {
            get { return m_EnumType; }
            set
            {
                m_EnumType = value;
                m_Value = null;

                if (MainForm.Instance.NodeTemplate == null)
                    return;

                CustomEnum customEnum = MainForm.Instance.NodeTemplate.FindEnum(m_EnumType);
                if (customEnum != null)
                {
                    EnumItem defaultEnumItem = customEnum.GetDefaultEnumItem();
                    if (defaultEnumItem != null)
                        m_Value = defaultEnumItem.EnumStr;
                }
            }
        }

        [TypeConverter(typeof(EnumItemTypeConverter))]
        [Category("常规"), DisplayName("枚举值"), Description("当前选中的枚举值")]
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(m_Value) ? string.Empty : m_Value;
        }

        public EnumItem GetEnumItemByIndex(int index)
        {
            CustomEnum customEnum = MainForm.Instance.NodeTemplate.FindEnum(m_EnumType);

            if (index >= 0 && index < customEnum.Enums.Count)
                return customEnum.Enums[index];

            return null;
        }

        [Browsable(false)]
        public int ValueInt
        {
            get
            {
                CustomEnum customEnum = MainForm.Instance.NodeTemplate.FindEnum(m_EnumType);
                if (customEnum != null)
                {
                    for (int i = 0; i < customEnum.Enums.Count; i++)
                    {
                        EnumItem enumItem = customEnum.Enums[i];
                        if (enumItem.EnumStr == m_Value)
                            return enumItem.EnumValue;
                    }
                }
                return 0;
            }
        }

        [Browsable(false)]
        public int ValueIndex
        {
            get
            {
                CustomEnum customEnum = MainForm.Instance.NodeTemplate.FindEnum(m_EnumType);
                if (customEnum != null)
                {
                    for (int i = 0; i < customEnum.Enums.Count; i++)
                    {
                        EnumItem enumItem = customEnum.Enums[i];
                        if (enumItem.EnumStr == m_Value)
                            return i;
                    }
                }
                return 0;
            }
        }

    }
}
