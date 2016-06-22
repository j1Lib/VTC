using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debug
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // new j1Lib.VTC.info("150595510", "Jack11320331");
            j1Lib.VTC.info.checkAndroidVersion((i)=>
            {
                
            });
        }
    }
}
