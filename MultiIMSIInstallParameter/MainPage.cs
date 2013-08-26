using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MultiIMSIInstallParameter.CustomGui;
using ParserLibrary.ItemRep;
using System.IO;
using ParserLibrary.ItemFactory;
using ParserLibrary.ItemObject;
using ParserLibrary.TranslateFactory;
using ParserLibrary.ItemObject.Decorator;
namespace MultiIMSIInstallParameter
{
    public partial class MainPage : Form
    {
        private ItemParser[] ListOfAvailableDefinition;
        private RadioButton[] ListOfRadioButton;
        private ItemParser ActiveDefinition;
        private Dictionary<string,Control> ListOfTextBox;
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void ExtractDefinition()
        {
            try
            {
                string[] DefFiles = System.IO.Directory.GetFiles(
                    System.IO.Directory.GetCurrentDirectory() + "\\ParamDef\\", "*.param");
                if (DefFiles.Length > 0)
                {
                    ListOfAvailableDefinition = new ItemParser[DefFiles.Length];
                    ListOfRadioButton = new RadioButton[DefFiles.Length];
                    int count = 0;
                    foreach (string defFile in DefFiles)
                    {

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            //ListOfTextBox.Clear();
            List<ParserLibrary.ItemObject.Item> items = pass.Items;
            //clear up the items inside text box
            
            ListOfTextBox = new Dictionary<string, Control>();
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

                ParserLibrary.ItemObject.Item basicForm = item;
                if (basicForm is ItemDecorator)
                {
                    basicForm = (item as ItemDecorator).getBaseClass();
                }
                // Text box
                if (basicForm is RegularItem)
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
                    ListOfTextBox.Add(item.Name,TB);
                    TB.TextAlign = HorizontalAlignment.Left;

                    // setting of the next items offset
                    startingOffset_Y += lbl.Size.Height + 15;
                    dividerLocation = new Point(lbl.Location.X, lbl.Location.Y + lbl.Size.Height + 7);
                }
                if (basicForm is ItemComposite)
                {
                    ItemComposite currentItem = (item as ItemComposite);
                    CompositeInput CI = new CompositeInput(currentItem);
                    CI.Location = new System.Drawing.Point(lbl.Location.X + startingOffset_X, lbl.Location.Y - 5);
                    CI.Size = new System.Drawing.Size(200, currentItem.getItems().Count * 20);
                    ContainerPanel.Controls.Add(CI);
                    ListOfTextBox.Add(item.Name,CI);
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
        private StringTranslator DefaultStringTranslator = new StringTranslator();
        
        private void TranslateButton_Click(object sender, EventArgs e)
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

                ActiveDefinition.setTranslator(DefaultStringTranslator);
                DefaultStringTranslator.setValue(textBox1.Text);
                DefaultStringTranslator.Import();
                counter = 0;
                foreach (var IT in ActiveDefinition.Items)
                {
                    ParserLibrary.ItemObject.Item basicForm = IT;
                    if (basicForm is ItemDecorator)
                    {
                        basicForm = (IT as ItemDecorator).getBaseClass();
                    }
                    if (basicForm is RegularItem)
                    {
                        ListOfTextBox[IT.Name].Text = IT.Value;
                    }
                    if (basicForm is ItemComposite)
                    {
                        CompositeInput CI = (CompositeInput)ListOfTextBox[IT.Name];
                        CI.SetValue(IT.Value);
                    }
                    counter++;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data.ToString());
            }

        }

        private void RenerateButton_Click(object sender, EventArgs e)
        {
            ActiveDefinition.setTranslator(DefaultStringTranslator);
            DefaultStringTranslator.Export();
            textBox1.Text = DefaultStringTranslator.getValue();
        }

      
        private void ClipboardButton_Click(object sender, EventArgs e)
        {
            var data = new DataObject();
            RTFTranslator rtf_translator = new RTFTranslator();

            ActiveDefinition.setTranslator(rtf_translator);
            rtf_translator.Export();
            data.SetText(rtf_translator.getValue(), TextDataFormat.Rtf);
            Clipboard.SetDataObject(data);
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            ExtractDefinition();
        }
    }
}