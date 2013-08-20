using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MultiIMSIInstallParameter.Parsers;
using MultiIMSIInstallParameter.Item;
using MultiIMSIInstallParameter.PreParser;
using MultiIMSIInstallParameter.CustomGui;
using ParserLibrary.ItemRep;
using System.IO;
using ParserLibrary.ItemFactory;
using ParserLibrary.ItemObject;
namespace MultiIMSIInstallParameter
{
    public partial class Form1 : Form
    {
        private ItemParser[] ListOfAvailableDefinition;
        private RadioButton[] ListOfRadioButton;
        private ItemParser ActiveDefinition;
        private List<Control> ListOfTextBox;
        public Form1()
        {
            InitializeComponent();
            string[] DefFiles = System.IO.Directory.GetFiles(
                System.IO.Directory.GetCurrentDirectory() + "\\ParamDef\\", "*.param");
            ListOfTextBox = new List<Control>();
            if (DefFiles.Length > 0)
            {
                ListOfAvailableDefinition = new ItemParser[DefFiles.Length];
                ListOfRadioButton = new RadioButton[DefFiles.Length];
                int count = 0;
                foreach (string defFile in DefFiles)
                {
                    PhysicalFileParser currentFileParser = new PhysicalFileParser(defFile);

                    RadioButton currentParserBtn = new RadioButton();
                    currentParserBtn.CheckedChanged += currentParserBtn_CheckedChanged;
                    currentParserBtn.Text = Path.GetFileNameWithoutExtension(defFile);
                    currentParserBtn.Location = new System.Drawing.Point(0, (count * 20));
                    currentParserBtn.Size = new System.Drawing.Size(400, 17);
                    this.ParserType.Controls.Add(currentParserBtn);

                    ListOfRadioButton[count] = currentParserBtn;
                    ItemParser currentParser = new ItemParser();
                    currentParser.setFactory(new NormalItemFactory(new StreamReader(defFile)));
                    ListOfAvailableDefinition[count] = currentParser;

                    count++;

                }
            }
        }

        void currentParserBtn_CheckedChanged(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (RadioButton radioButton in ListOfRadioButton)
            {
                if (radioButton.Checked)
                {
                    ActiveDefinition = ListOfAvailableDefinition[counter];
                }
                counter++;
            }
            generateItemInPanel(ActiveDefinition);
        }

        private void generateItemInPanel(ItemParser pass)
        {
            int counter_Y = 0;
            int counter_X = 0;
            int startingOffset_Y = 20;
            int startingOffset_X = 250;

            ContainerPanel.Controls.Clear();
            ListOfTextBox.Clear();
            List<ParserLibrary.ItemObject.Item> items = pass.Items;
            //foreach (var item in items)
            for (int i = 0; i < items.Count; i++)
            {
                Point dividerLocation = new Point();
                ParserLibrary.ItemObject.Item item = items[i];

                if (!item.canBeDisplayed())
                {
                    continue;
                }
                // Label
                Label lbl = new Label();
                int numOfLines = 0;
                lbl.Text = GuiHelper.GuiHelper.chopToLines(item.Name, out numOfLines);
                lbl.Size = new System.Drawing.Size(20, numOfLines * 20);
                lbl.Location = new System.Drawing.Point((counter_X * 400) + 10, startingOffset_Y);
                lbl.AutoSize = true;
                ContainerPanel.Controls.Add(lbl);



                // Text box
                if (item is RegularItem)
                {
                    TextBox TB = new TextBox();
                    if (item.Length > 0)
                    {
                        TB.MaxLength = item.Length* 2;
                        TB.Size = new System.Drawing.Size(TB.MaxLength * 9, 23);
                    }
                    else
                    {
                        TB.Size = new System.Drawing.Size(200, 23);
                    }
                    TB.DataBindings.Add("Text", item, "Value");
                    TB.DataBindings.Add("Name", item, "Name");
                    TB.Location = new System.Drawing.Point(lbl.Location.X + startingOffset_X, lbl.Location.Y - 5);
                    ContainerPanel.Controls.Add(TB);
                    ListOfTextBox.Add(TB);
                    TB.TextAlign = HorizontalAlignment.Left;

                    // setting of the next items offset
                    startingOffset_Y += lbl.Size.Height + 15;
                    dividerLocation = new Point(lbl.Location.X, lbl.Location.Y + lbl.Size.Height + 7);
                }
                if (item is ItemComposite)
                {
                    ItemComposite currentItem = (item as ItemComposite);
                    CompositeInput CI = new CompositeInput(currentItem);
                    CI.Location = new System.Drawing.Point(lbl.Location.X + startingOffset_X, lbl.Location.Y - 5);
                    CI.Size = new System.Drawing.Size(200, currentItem.getItems().Count * 20);
                    ContainerPanel.Controls.Add(CI);
                    ListOfTextBox.Add(CI);
                    // setting of the next items offset
                    startingOffset_Y += CI.Size.Height + 15;
                    dividerLocation = new System.Drawing.Point(lbl.Location.X, lbl.Location.Y + CI.Size.Height);
                }

                Label Label_HorizontalLine = new Label();
                Label_HorizontalLine.Text = "";
                Label_HorizontalLine.BorderStyle = BorderStyle.Fixed3D;
                Label_HorizontalLine.AutoSize = false;
                Label_HorizontalLine.Height = 2;
                Label_HorizontalLine.Width = lbl.Location.X + ContainerPanel.Width;
                Label_HorizontalLine.Location = dividerLocation;
                ContainerPanel.Controls.Add(Label_HorizontalLine);
                counter_Y++;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int counter = 0;
                foreach (RadioButton radioButton in ListOfRadioButton)
                {
                    if (radioButton.Checked)
                    {
                        ActiveDefinition = ListOfAvailableDefinition[counter];
                    }
                    counter++;
                }

                List<ItemTranslation> data = new List<ItemTranslation>();
              //  ActiveDefinition.Parse(LengthChanger.removeLength(textBox1.Text));
                counter = 0;
                //foreach (ItemRepresentation IT in ActiveDefinition.getItems())
                //{
                //    if (IT.lengthType == ItemRepresentation.LengthType.affectNext)
                //    {
                //        continue;
                //    }
                //    if (IT.valueType == ItemRepresentation.ValueType.normal)
                //    {
                //        ListOfTextBox[counter].Text = IT.ItemValue;
                //    }
                //    else
                //    {
                //        CompositeInput CI = (CompositeInput)ListOfTextBox[counter];
                //        CI.SetValue(IT.ItemValue);
                //    }
                //    counter++;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegerateData();
        }

        private string[] RegerateData()
        {
            List<string> choppedData = new List<string>();
            StringBuilder sb = new StringBuilder();
            List<ParserLibrary.ItemObject.Item> items = ActiveDefinition.Items;
            foreach (var item in ActiveDefinition.Items)
	        {
                        sb.Append(item.Value);
                        choppedData.Add(item.Value);
		 
	        }            
            textBox1.Text = sb.ToString();
            return choppedData.ToArray();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String[] choppedData =  RegerateData();
            var data = new DataObject();
            // setting of color pallet
            var colorPalet = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}";
            StringBuilder SB = new StringBuilder();
            int counter = 0;
            foreach (var item in choppedData)
            {
                SB.Append(((counter % 2 == 0) ? @"\cf1" : @"\cf2" )+ " " + item);
                counter++;
            }
            SB.Append(@"\line ");
            //List<ItemRepresentation> listOfPass = ActiveDefinition.getItems();
            //for(int i = 0; i<listOfPass.Count;i++)
            //{
            //    var item = listOfPass[i];
            //    if (item.lengthType == ItemRepresentation.LengthType.affectNext)
            //    {
            //        SB.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
            //        SB.Append((listOfPass[i + 1].ItemValue.Length / 2).ToString("X2") + @"\tab ");
            //        SB.Append(@"\cf1 = ");
            //        SB.Append(item.ItemName + @"\line ");
            //        continue;
            //    }
            //    if (item.valueType == ItemRepresentation.ValueType.normal)
            //    {
            //        SB.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
            //        SB.Append(item.ItemValue + @"\tab ");
            //        SB.Append(@"\cf1 = ");
            //        SB.Append(item.ItemName + @"\line ");
            //        continue;
            //    }
            //    if (item.valueType == ItemRepresentation.ValueType.composite)
            //    {
            //        SB.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
            //        SB.Append(item.compositeValues.ToString() + @"\tab ");
            //        SB.Append(@"\cf1 = ");
            //        SB.Append(item.ItemName + @"\line");
            //        foreach (var compositeItem in item.compositeValues.getItems())
            //        {
            //            SB.Append(@"\tab " + compositeItem.name + @"\tab = " + (compositeItem.isChecked ? "activated" : "deactivated") + @"\line ");
            //        }
            //    }
            //}
            data.SetText(colorPalet + SB.ToString() + "}", TextDataFormat.Rtf);
            Clipboard.SetDataObject(data);
        }
    }
}