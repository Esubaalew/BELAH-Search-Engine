using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Player
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            panelResult.Visible = false;
            panelBarolder.Anchor = AnchorStyles.None;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            this.Activate();
            panelResult.Visible = false;
            panelBarolder.Anchor = AnchorStyles.None;
            panelBarolder.Visible = true;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelResult.Visible = true;
            panelResult.Dock = DockStyle.Top;
            panelBarolder.Visible = false;

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                textBox2.Text = null;
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                textBox1.Text = null;
            }
        }
    }
}
