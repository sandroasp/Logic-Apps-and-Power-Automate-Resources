using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenameJsonLogicApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAction.Text != "")
            {
             txtResult.Text = txtAction.Text.Replace(" ", "_");
            }
            else
            {
                label3.Text = "Texto Vazio.";
            }
            label4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtResult.Text);
            label4.Text = "Texto Copiado";
            label3.Text = "";
        }

        private void txtTexto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTransformado_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtAction.Text != "")
            {
                txtResult.Text = txtAction.Text.Replace(" ", "_");
                label3.Text = "";
            }
            else
            {
                label3.Text = "Empty Action Name";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (txtAction.Text != "")
            {
                Clipboard.SetText(txtResult.Text);
                label3.Text = "";
            }
            else
            {
                label3.Text = "Empty Action Name";
            }
        }
    }
}
