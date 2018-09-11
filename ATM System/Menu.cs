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
            Form transfer = new Balance_Transfer();
            Form PINs = new PIN();
            PINs.ShowDialog();
            Hide();
            transfer.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form withdraw = new Withdraw();
            Form PINs = new PIN();
            PINs.ShowDialog();
            Hide();
            withdraw.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form pay = new Paybills();
            Form PINs = new PIN();
            PINs.ShowDialog();
            Hide();
            pay.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form rewards = new ClaimRewards();
            Form PINs = new PIN();
            PINs.ShowDialog();
            Hide();
            rewards.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form changepin = new ChangePin();
            Form PINs = new PIN();
            PINs.ShowDialog();
            Hide();
            changepin.Show();
        }

        private void Inquiry_Click(object sender, EventArgs e)
        {
            Form PINins = new PIN();
            PINins.ShowDialog();
            Hide();
            Form inquiry = new Balance_Inquiry();
            inquiry.Show();
        }
    }
}
