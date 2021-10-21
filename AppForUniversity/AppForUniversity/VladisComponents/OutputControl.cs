using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Lab1Appanov.Components
{
    public partial class OutputControl : UserControl
    {
        public OutputControl()
        {
            InitializeComponent(); 
        }

        public PropertyInfo[] Hierarchy;
        private PropertyInfo[] Fields;

        public void FillHierarchy<T>(Queue<string> fieldQueue)
        {
            Type type = Type.GetType(typeof(T).FullName);
            this.Fields = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Hierarchy = new PropertyInfo[fieldQueue.Count];
            for (int i = 0; i < Hierarchy.Length; i++)
            {
                foreach (PropertyInfo field in Fields)
                {
                    String str = field.Name;
                    if (("<" + fieldQueue.Peek() + ">k__BackingField").Equals(field.Name.Trim()) || fieldQueue.Peek().Equals(field.Name.Trim()))
                    {
                        Hierarchy[i] = field;
                        fieldQueue.Dequeue();
                        break;
                    }
                }
            }

        }

        public void FillTree<T>(List<T> ListObjects)
        {
            if(Hierarchy == null)
            {
                Type type = Type.GetType(typeof(T).FullName);
                Fields = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                Hierarchy = Fields;
            }
            treeView1.Nodes.Clear();
            foreach (T obj in ListObjects)
            {
                bool firstElem = true;
                foreach (PropertyInfo propertyInfo in Hierarchy)
                {
                    if (firstElem) {
                        if (treeView1.Nodes.Find(propertyInfo.GetValue(obj).ToString(), false).Length == 0)
                        {
                            TreeNode node = new TreeNode();
                            node.Name = propertyInfo.GetValue(obj).ToString();
                            node.Text = propertyInfo.GetValue(obj).ToString();
                            treeView1.Nodes.Add(node);
                            treeView1.SelectedNode = node;
                        }
                        else
                        {
                            treeView1.SelectedNode = treeView1.Nodes.Find(propertyInfo.GetValue(obj).ToString(), false)[0];
                        }
                        firstElem = false;
                        continue;
                    }
                    if (treeView1.SelectedNode.Nodes.Find(propertyInfo.GetValue(obj).ToString(), false).Length == 0)
                    {
                        TreeNode node1 = new TreeNode();
                        node1.Name = propertyInfo.GetValue(obj).ToString();
                        node1.Text = propertyInfo.GetValue(obj).ToString();
                        treeView1.SelectedNode.Nodes.Add(node1);
                        treeView1.SelectedNode = node1;
                    }
                    else
                    {
                        treeView1.SelectedNode = treeView1.SelectedNode.Nodes.Find(propertyInfo.GetValue(obj).ToString(), false)[0];
                    }
                }
            }


        }

        public T GetSelectedEntry<T>() where T : class, new()
        {
            T obj = new T();
            for (int i = Hierarchy.Length-1; i > -1; i--)
            {
                if (Hierarchy[i].Name == "Id")
                {
                    Hierarchy[i].SetValue(obj, Convert.ChangeType(treeView1.SelectedNode.Text, Hierarchy[i].PropertyType));
                }
                treeView1.SelectedNode = treeView1.SelectedNode.Parent;
            }
            return obj;
        }

        public int SelectedNodeIndex
        {
            get
            {
                return treeView1.SelectedNode.Index;
            }

            set
            {
                TreeNode treeNode = (TreeNode)treeView1.SelectedNode.Clone();

                if (treeView1.SelectedNode.Parent == null)
                {
                    
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                    treeView1.Nodes.Insert(value, treeNode);
                    return;
                }
                treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
                treeView1.SelectedNode.Parent.Nodes.Insert(value, treeNode);
            }
        }

    }
}
