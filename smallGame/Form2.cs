using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smallGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 18)
            {
                MessageBox.Show("Pod minimalnata vuzrast");
            }
            else
            {
                MessageBox.Show("Uspeh");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
