using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace IncisionDKP
{
    public partial class Form1 : Form
    {
        public static Label infoBox; //this is why I don't like using form builder.
        private static System.Timers.Timer Timer1 = new System.Timers.Timer(1000);

        public Form1()
        {
            InitializeComponent();
            this.Icon = IncisionDKP.Properties.Resources.moose;
            infoBox = label2;

            Timer1.Elapsed += (s, e) =>
            {
                if (CDKP.reportReady)
                {
                    CDKP.reportReady = false;
                    CDKP.FindUserEncouters("heavendust");
                }
            };
            Timer1.AutoReset = true;
            Timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //RetrieveDKP blah = new RetrieveDKP();
        }

        public static void Status(string s, Label lbl, int type = 0) //type 1=notice, 2=warning, 3=critical exception caught
        {
            string _tmp = "";
            if (type == 1)
                _tmp = "[NOTICE]: ";
            else if (type == 2)
                _tmp = "[WARNING]: ";
            else if (type == 3)
                _tmp = "[CRITICAL]: ";
            else
                _tmp = "[INFO]: ";

            lbl.Text = _tmp + s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) { Status("You must enter an ID to fetch.", infoBox, 2); return; }

            //CDKP blah = new CDKP(textBox1.Text);
            CDKP.init(textBox1.Text);
        }

    }
}
