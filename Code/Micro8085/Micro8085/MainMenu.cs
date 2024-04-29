using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micro8085
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }


        private void Start_Click(object sender, EventArgs e)
        {

            //  this.Visible = false;


            Micro8085 start = new Micro8085();
          //  this.WindowState = FormWindowState.Minimized;
            start.ShowDialog();


            //  start.Show();

        }

        private void Help_Click(object sender, EventArgs e)
        {
            
        }

        private void Instruction_Set_Click(object sender, EventArgs e)
        {

        }

        private void About_us_Click(object sender, EventArgs e)
        {

            string st = "\tArpan Ghosh\n\tShalini Guha\n\t          and\n\tTonmoy Golder\n\t(Dept. Of CMSA)";
            MessageBox.Show(st);
        }

        private void Res_and_Ref_Click(object sender, EventArgs e)
        {
            string sst = "Ajit Pal\nN.K. Srinath\nRamesh Gaonker\nM.Rafiquzzaman\nAnd\nInternet";
            MessageBox.Show(sst);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
