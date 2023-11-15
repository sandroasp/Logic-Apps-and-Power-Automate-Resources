using System;
using System.Windows.Forms;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            // Customize the label with your application informatio
        }



        private void labelAbout_Click(object sender, EventArgs e)
        {

        }

        private void labelauthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://devscope.net/");

        }
    }

}
