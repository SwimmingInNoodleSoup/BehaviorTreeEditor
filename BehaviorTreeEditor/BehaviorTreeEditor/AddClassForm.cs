﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BehaviorTreeEditor
{
    public partial class AddClassForm : Form
    {
        private NodeClass m_NodeClass = new NodeClass();
        private ClassForm m_ClassForm = null;

        public AddClassForm(ClassForm classForm)
        {
            m_ClassForm = classForm;
            InitializeComponent();
        }

        private void AddClassForm_Load(object sender, EventArgs e)
        {
            nodeTypeCBB.Items.Clear();
            string[] enumNames = Enum.GetNames(typeof(NodeType));
            for (int i = 2; i < enumNames.Length; i++)
            {
                nodeTypeCBB.Items.Add(enumNames[i]);
            }
            nodeTypeCBB.SelectedIndex = 0;
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enterBTN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(classTypeTB.Text.Trim()))
            {
                MainForm.Instance.ShowMessage("请填写类名", "提示");
                return;
            }

            m_NodeClass.ClassType = classTypeTB.Text.Trim();
            m_NodeClass.NodeType = (NodeType)(nodeTypeCBB.SelectedIndex + 2);
            m_NodeClass.Describe = describeTB.Text.Trim();

            if (!MainForm.Instance.NodeClasses.AddClass(m_NodeClass))
                return;

            m_ClassForm.AddClass(m_NodeClass);

            this.Close();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exec("CopyField");
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exec("PasteField");
        }

        private void 选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exec("EditField");
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exec("NewField");
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exec("DeleteField");
        }

        private void listViewEvents_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        Exec("CopyField");
                        break;
                    case Keys.V:
                        Exec("PasteField");
                        break;
                    case Keys.Up:
                        Exec("SwapField", true);
                        break;
                    case Keys.Down:
                        Exec("SwapField", false);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.F12:
                        Exec("EditField");
                        break;
                    case Keys.N:
                        Exec("NewField");
                        break;
                    case Keys.Delete:
                        Exec("DeleteField");
                        break;
                }
            }
        }

        private void listViewEvents_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Tag = listViewFields;
                    contextMenuStrip1.Show(listViewFields, e.Location);
                }
            }
            else if (e.Clicks == 2)
            {
                if (listViewFields.SelectedItems.Count == 1)
                {
                    InputValueDialogForm dlg = new InputValueDialogForm("编辑字段", listViewFields.SelectedItems[0].Tag);
                    dlg.ShowDialog();
                }
                else
                {
                    Exec("NewField");
                }
                Exec("Refresh");
            }
        }

        private bool Exec(string cmd, params object[] agrs)
        {
            if (string.IsNullOrEmpty(cmd))
                return false;

            switch (cmd)
            {
                case "Refresh":
                    ShowFieldDataInPage(m_NodeClass);
                    break;
                case "NewField":
                    NewField();
                    break;
                case "EditField":
                    EditField();
                    break;
                case "DeleteField":
                    DeleteField();
                    break;
                case "SwapField":
                    SwapField((bool)agrs[0]);
                    break;
            }

            return true;
        }

        private void NewField()
        {
            FieldDesigner field = new FieldDesigner();
            InputValueDialogForm form = new InputValueDialogForm("添加字段", field);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (m_NodeClass.AddField(field))
                {
                    Exec("Refresh");
                }
            }
        }

        //刷新ListView
        private void ShowFieldDataInPage(NodeClass m_NodeClass)
        {
            if (m_NodeClass == null)
                return;

            listViewFields.Items.Clear();
            foreach (FieldDesigner field in m_NodeClass.Fields)
            {
                if (field.Field == null)
                    continue;
                ListViewItem listViewItem = listViewFields.Items.Add(field.Field.FieldName);
                listViewItem.Tag = field;
                listViewItem.SubItems.Add(field.FieldType.ToString());
                listViewItem.SubItems.Add(field.Field.Describe);
            }

        }

        //编辑字段
        private void EditField()
        {
            CollectionEditor.EditValue(this, m_NodeClass, "Fields");
        }

        public class FieldListContent
        {
            List<FieldDesigner> m_DataList = new List<FieldDesigner>();
            public List<FieldDesigner> DataList { get { return m_DataList; } }
        }

        //复制字段
        private void CopyField()
        {
            if (listViewFields.SelectedItems.Count > 0)
            {
                FieldListContent content = new FieldListContent();
                foreach (ListViewItem lvItem in listViewFields.SelectedItems)
                {
                    if (lvItem.Tag is FieldDesigner)
                        content.DataList.Add((FieldDesigner)lvItem.Tag);
                }

                if (content.DataList.Count > 0)
                    Clipboard.SetText(XmlUtility.ObjectToString(content));

                MainForm.Instance.ShowInfo("恭喜，您复制了" + content.DataList.Count.ToString() + "个！！！");
            }
            else
            {
                MainForm.Instance.ShowInfo("您必须选择至少一个进行复制！！！");
                MainForm.Instance.ShowMessage("您必须选择至少一个进行复制！！！", "警告");
            }
        }

        //粘贴字段
        private void PasteField()
        {
            try
            {
                // convert string to stream
                FieldListContent content = (FieldListContent)XmlUtility.StringToObject<FieldListContent>(Clipboard.GetText());

                for (int i = 0; i < content.DataList.Count; i++)
                {
                    string fieldName = content.DataList[i].Field.FieldName;

                    m_NodeClass.AddField();
                }


                int selectIdx = mCurrentEditAction.Events.Count;
                if (listViewEvents.SelectedIndices.Count > 0)
                    selectIdx = listViewEvents.SelectedIndices[listViewEvents.SelectedIndices.Count - 1] + 1;

                foreach (Event actionEvent in content.DataList)
                {
                    mCurrentEditAction.Events.Insert(selectIdx, actionEvent);
                    selectIdx++;
                }

                Exec("Refresh");

                MainForm.Instance.ShowInfo("恭喜，您粘贴了" + content.DataList.Count + "个劳动成果！！！");
            }
            catch (Exception ex)
            {
                MainForm.Instance.ShowInfo("无法进行粘贴，错误信息：" + ex.Message);
                MainForm.Instance.ShowMessage("无法进行粘贴，错误信息：" + ex.Message, "警告");
            }
        }

        //删除字段
        private void DeleteField()
        {
            if (listViewFields.SelectedIndices.Count == 1)
            {
                int selectIdx = listViewFields.SelectedIndices[0];
                m_NodeClass.Fields.RemoveAt(selectIdx);
                MainForm.Instance.ShowInfo("删除成功");
                Exec("Refresh");
                if (listViewFields.Items.Count > selectIdx)
                    listViewFields.Items[selectIdx].Selected = true;
            }
            else if (listViewFields.SelectedIndices.Count == 0)
            {
                MainForm.Instance.ShowInfo("请选择一条记录进行删除");
            }
            else
            {
                MainForm.Instance.ShowInfo("无法一次删除多个记录");
            }
        }

        //交换
        private void SwapField(bool up)
        {
            if (listViewFields.SelectedIndices.Count > 1)
            {
                MainForm.Instance.ShowInfo("请选择一条记录进行交换");
                MainForm.Instance.ShowMessage("请选择一条记录进行交换", "警告");
                return;
            }

            int selectIdx = listViewFields.SelectedIndices[0];
            if (up)
            {
                //第一个不能往上交换
                if (selectIdx == 0)
                    return;

                int preIdx = selectIdx - 1;

                FieldDesigner preField = m_NodeClass.Fields[preIdx];
                FieldDesigner selectedField = m_NodeClass.Fields[selectIdx];

                m_NodeClass.Fields[preIdx] = selectedField;
                m_NodeClass.Fields[selectIdx] = preField;

                selectIdx = preIdx;
            }
            else
            {
                //最后一个不能往下交换
                if (selectIdx == listViewFields.Items.Count - 1)
                    return;

                int nextIdx = selectIdx + 1;

                FieldDesigner preField = m_NodeClass.Fields[nextIdx];
                FieldDesigner selectedField = m_NodeClass.Fields[selectIdx];

                m_NodeClass.Fields[nextIdx] = selectedField;
                m_NodeClass.Fields[selectIdx] = preField;

                selectIdx = nextIdx;
            }

            Exec("Refresh");
            MainForm.Instance.ShowInfo("交换成功 时间:" + DateTime.Now);
            listViewFields.Items[selectIdx].Selected = true;
        }
    }
}