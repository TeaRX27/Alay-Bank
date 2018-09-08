using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_System
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PIN.transaction = "Transfer";
            Form PINs = new PIN();
            PINs.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PIN.transaction = "withdraw";
            Form PINs = new PIN();
            PINs.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PIN.transaction = "paybills";
            Form PINs = new PIN();
            PINs.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PIN.transaction = "rewards";
            Form PINs = new PIN();
            PINs.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PIN.transaction = "pinchange";
            Form PINs = new PIN();
            PINs.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PIN.transaction = "inquiry";
            Form PINins = new PIN();
            PINins.Show();
            Hide();
        }
    }
}
