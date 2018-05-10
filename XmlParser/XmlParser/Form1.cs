using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "Text documents (.xml)|*.xml";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
                using (XmlTextReader reader = new XmlTextReader(openFileDialog.OpenFile()))
                {
                    
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.HasAttributes)
                            {
                                string key = reader.GetAttribute(0);
                                if (!keyValuePairs.ContainsKey(key))
                                {
                                    keyValuePairs.Add(key, 1);
                                }
                                else
                                {
                                    int value = keyValuePairs[key];
                                    keyValuePairs[key] = ++keyValuePairs[key];
                                }

                            }
                            
                        }
                    }
                }
                foreach(KeyValuePair<string, int> entry in keyValuePairs)
                {
                    rtb_roles.Text += entry.Key + " : " + entry.Value + "\r\n";
                }
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
