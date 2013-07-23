using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MultiIMSIInstallParameter.Parsers;
using MultiIMSIInstallParameter.Item;
using MultiIMSIInstallParameter.PreParser;

namespace MultiIMSIInstallParameter
{
    public partial class Form1 : Form
    {
        private EFInstallParam EFParam;
        private BufferInstallParam BuffParam;
        private ApplicationParameter AppParam;
        private PhysicalFileParser PhysicalParam;
        private PhysicalFileParser[] ListOfAvailableParser;
        private RadioButton[] ListOfRadioButton;
        private Parser pass;
        public Form1()
        {
            InitializeComponent();
            EFParam = new EFInstallParam();
            BuffParam = new BufferInstallParam();
            AppParam = new ApplicationParameter();
            string[] DefFiles = System.IO.Directory.GetFiles(
                System.IO.Directory.GetCurrentDirectory() + "\\ParamDef\\", "*.param");
            
            if (DefFiles.Length > 0)
            {
                ListOfAvailableParser = new PhysicalFileParser[DefFiles.Length];
                ListOfRadioButton = new RadioButton[DefFiles.Length];
                int count = 0;
                foreach (string defFile in DefFiles)
                {
                    PhysicalFileParser currentFileParser = new PhysicalFileParser(defFile);
                  
                    RadioButton currentParserBtn =  new RadioButton();
                    currentParserBtn.Text = currentFileParser.ParserName;
                    currentParserBtn.Location = new System.Drawing.Point(13, 70 + (count * 20));
                    currentParserBtn.Size = new System.Drawing.Size(400, 17);
                    this.Controls.Add(currentParserBtn);

                    ListOfAvailableParser[count] = currentFileParser;
                    ListOfRadioButton[count] = currentParserBtn;
                    count++;
                }
            }
           
            pass = EFParam;
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value");
                dt.Columns.Add("Description");
                int counter = 0;
                foreach (RadioButton radioButton in ListOfRadioButton)
                {
                    if (radioButton.Checked)
                    {
                        pass = ListOfAvailableParser[counter];
                    }
                    counter++;
                }
                
                List<ItemTranslation> data = new List<ItemTranslation>();
                data = pass.Parse(LengthChanger.removeLength(textBox1.Text));
                foreach (ItemTranslation itemTranslate in data)
                {
                    dt.Rows.Add(itemTranslate.Value, itemTranslate.Description);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns.Insert(2, new DataGridViewCheckBoxColumn());
                dataGridView1.Columns[0].Width = 250;
                dataGridView1.Columns[1].Width = 300;
            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Wrong data!");
                //MessageBox.Show(e.Message);
//            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (dataGridView1.DataSource != null)
            {
                DataTable dt = (DataTable) dataGridView1.DataSource;
                foreach (DataRow dr in dt.Rows   )
                {
                    if (dr[0].ToString() != ItemTranslation.NOT_DEFINED)
                    {
                        sb.Append(dr[0].ToString());
                    }
                }
            }
            textBox1.Text = sb.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = new DataObject();
            data.SetText(@"{\rtf1\ansi\deff0
{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}
\cf2 02 \tab \cf1 the default color\line
\cf2
This line\cf1 is red\line

This line is the default color
}", TextDataFormat.Rtf);
            Clipboard.SetDataObject(data);
        }
    }
}