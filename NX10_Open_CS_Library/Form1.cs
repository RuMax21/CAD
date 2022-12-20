using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NX10_Open_CS_Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int diametr = Convert.ToInt32(this.comboBox1.Text);
            switch (diametr)
            {
                case 5:
                    Program.drawBolt(diametr);
                    break;
                case 6:
                    Program.drawBolt(diametr);
                    break;
                case 8:
                    Program.drawBolt(diametr);
                    break;
                case 10:
                    Program.drawBolt(diametr);
                    break;
                case 12:
                    Program.drawBolt(diametr);
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
