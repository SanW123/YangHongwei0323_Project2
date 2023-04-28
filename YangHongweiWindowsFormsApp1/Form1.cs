using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YangHongweiWindowsFormsApp1
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PoplateTreeView();
            initalizeListview();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void statusBar1_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           ;
        }
        private void PoplateTreeView()
        {
            statusBar1.Tag = "Refresh Employee Code. Please Wait ...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Emplyoee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);//ok

            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("C:\\Users\\YHW\\source\\repos\\YangHongweiWindowsFormsApp1\\YangHongweiWindowsFormsApp1\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        String strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();

                        }
                        TreeNode EcodeNode = new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);

                    }
                }

                statusBar1.Tag = "Click on an employee code to see  their record. ";
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void initalizeListview()
        {
            listView1.Clear();
            listView1.Columns.Add("Emplyoee Name", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Gread", 105, HorizontalAlignment.Left);
            listView1.Columns.Add("Salary", 105, HorizontalAlignment.Left);
        }

        protected void PopulateListview(TreeNode crrNode)
        {
            initalizeListview();
            XmlTextReader listRead = new XmlTextReader("C:\\Users\\YHW\\source\\repos\\YangHongweiWindowsFormsApp1\\YangHongweiWindowsFormsApp1\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new string[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if (strNodeName == strNodePath)
                {
                    ListViewItem lvi;
                    listRead.MoveToNextAttribute();
                    name = listRead.Value;//name "Michael Preey"
                    lvi = listView1.Items.Add(name);
                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj = listRead.Value;//DateofJoin="02-02-1999"
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute();
                    gread = listRead.Value;//Gread="A"
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute();
                    sal = listRead.Value;//Salary="1750"
                    lvi.SubItems.Add(sal);
                    listRead.MoveToNextAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();


                }
            }
        }

        private void treeview1_Afterselect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                statusBar1.Text = "Double click the Employee Records.";
                return;
            }
            else
            {
                statusBar1.Text = "Click an Emplyoee code to view Individual";
            }
            PopulateListview(currNode);
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                statusBar1.Text = "Double click the Employee Records.";
                return;
            }
            else
            {
                statusBar1.Text = "Click an Emplyoee code to view Individual";
            }
            PopulateListview(currNode);
        }
    }
}
