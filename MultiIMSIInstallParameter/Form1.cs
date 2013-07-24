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
using Microsoft.VisualBasic.PowerPacks;

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
        private List<TextBox> ListOfTextBox;
        public Form1()
        {
            InitializeComponent();
            EFParam = new EFInstallParam();
            BuffParam = new BufferInstallParam();
            AppParam = new ApplicationParameter();
            string[] DefFiles = System.IO.Directory.GetFiles(
                System.IO.Directory.GetCurrentDirectory() + "\\ParamDef\\", "*.param");
            ListOfTextBox = new List<TextBox>();
            if (DefFiles.Length > 0)
            {
                ListOfAvailableParser = new PhysicalFileParser[DefFiles.Length];
                ListOfRadioButton = new RadioButton[DefFiles.Length];
                int count = 0;
                foreach (string defFile in DefFiles)
                {
                    PhysicalFileParser currentFileParser = new PhysicalFileParser(defFile);
                  
                    RadioButton currentParserBtn =  new RadioButton();
                    currentParserBtn.CheckedChanged += currentParserBtn_CheckedChanged;
                    currentParserBtn.Text = currentFileParser.ParserName;
                    currentParserBtn.Location = new System.Drawing.Point(0,(count * 20));
                    currentParserBtn.Size = new System.Drawing.Size(400, 17);
                    this.ParserType.Controls.Add(currentParserBtn);

                    ListOfAvailableParser[count] = currentFileParser;
                    ListOfRadioButton[count] = currentParserBtn;
                    count++;
                }
            }
           
            pass = EFParam;
        }

        void currentParserBtn_CheckedChanged(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (RadioButton radioButton in ListOfRadioButton)
            {
                if (radioButton.Checked)
                {
                    pass = ListOfAvailableParser[counter];
                }
                counter++;
            }
            generateItemInPanel(pass);
        }

        private void generateItemInPanel(Parser pass)
        {
            int counter_Y = 0;
            int counter_X = 0;
            int startingOffset_Y = 20;
            int startingOffset_X = 250;

            ContainerPanel.Controls.Clear();
            ListOfTextBox.Clear();
            List<ItemRepresentation> items = pass.getItems();
            List<Shape> ListOfShapes = new List<Shape>();
            //foreach (var item in items)
            for (int i = 0; i < items.Count;i++ )
            {

                ItemRepresentation item = items[i];
                if (item.lengthType == ItemRepresentation.LengthType.affectNext)
                {
                    items[i + 1].ItemLength = 0;
                    continue;
                }
                // Label
                Label lbl = new Label();
                int numOfLines = 0;
                lbl.Text = GuiHelper.GuiHelper.chopToLines(item.ItemName, out numOfLines);
                lbl.Size = new System.Drawing.Size(20, numOfLines * 20);
                lbl.Location = new System.Drawing.Point((counter_X * 400) + 10, startingOffset_Y);
                lbl.AutoSize = true;
                ContainerPanel.Controls.Add(lbl);
                startingOffset_Y += lbl.Size.Height + 15;

                LineShape LS = new LineShape();
                LS.StartPoint = new Point(lbl.Location.X, lbl.Location.Y + lbl.Size.Height + 7);
                LS.EndPoint = new Point(lbl.Location.X + ContainerPanel.Width, lbl.Location.Y + lbl.Size.Height + 7);
                ListOfShapes.Add(LS);

                // Text box
                TextBox TB = new TextBox();
                if (item.ItemLength > 0)
                {
                    TB.MaxLength = item.ItemLength * 2;
                    TB.Size = new System.Drawing.Size(TB.MaxLength * 9, 23);
                }
                else
                {
                    TB.Size = new System.Drawing.Size(200, 23);
                }
                TB.DataBindings.Add("Text", item, "ItemValue");
                TB.DataBindings.Add("Name", item, "ItemName");
                TB.Location = new System.Drawing.Point(lbl.Location.X + startingOffset_X, lbl.Location.Y - 5);
                ContainerPanel.Controls.Add(TB);
                ListOfTextBox.Add(TB);
                TB.TextAlign = HorizontalAlignment.Left;
                counter_Y++;
            }

            ShapeContainer SP = new ShapeContainer();
            SP.Shapes.AddRange(ListOfShapes.ToArray());
            ContainerPanel.Controls.Add(SP);
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
                pass.Parse(LengthChanger.removeLength(textBox1.Text));
                counter = 0;
                foreach (ItemRepresentation IT in pass.getItems())
                {
                    if (IT.lengthType == ItemRepresentation.LengthType.affectNext)
                    {
                        continue;
                    }
                    ListOfTextBox[counter].Text = IT.ItemValue;
                    counter++;
                }
                //dataGridView1.DataSource = dt;
                //dataGridView1.Columns.Insert(2, new DataGridViewCheckBoxColumn());
                //dataGridView1.Columns[0].Width = 250;
                //dataGridView1.Columns[1].Width = 300;
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
            List<ItemRepresentation> items = pass.getItems();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].lengthType == ItemRepresentation.LengthType.affectNext)
                {
                    i++;
                    sb.Append((items[i].ItemValue.Length/2).ToString("X2"));
                }
                sb.Append(items[i].ItemValue);
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