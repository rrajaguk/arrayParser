using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ParserLibrary.ItemObject;

namespace MultiIMSIInstallParameter.CustomGui
{
    public partial class CompositeInput : UserControl
    {
        private ItemComposite CV;
        private List<BitItem> lisfOfItems;
        public CompositeInput(ItemComposite CV)
        {
            InitializeComponent();
            this.CV = CV;
            lisfOfItems = CV.getItems();
            for(int i = 0; i<lisfOfItems .Count;i++ ){
                BitItem currentItem = lisfOfItems[i];
                CheckBox CB = new CheckBox();
                CB.Text = currentItem.name;
                CB.Name = currentItem.location.ToString();
                CB.Size = new System.Drawing.Size(80, 17);
                CB.AutoSize = true;
                CB.Location = new Point(7, 20 * i);
                CB.DataBindings.Add("Checked", currentItem, "isChecked");
                this.panel1.Controls.Add(CB);
            }
        }

        public void SetValue(string val)
        {
            short sb = short.Parse(val, System.Globalization.NumberStyles.HexNumber);
            for (int i = 0; i < lisfOfItems.Count; i++)
            {
                BitItem currentItem = lisfOfItems[i];
                short interestedBit = (short)( 1 << (currentItem.location -1));
                if ((sb & interestedBit) == interestedBit  )
                {
                    currentItem.isChecked = true;
                }
                else
                {
                    currentItem.isChecked = false;
                }
                CheckBox cl = (CheckBox)this.panel1.Controls.Find(currentItem.location.ToString(), true)[0];
                cl.Checked = currentItem.isChecked;
            }
        }
        //void CB_CheckedChanged(object sender, EventArgs e)
        //{
           
        //}
    }
}
