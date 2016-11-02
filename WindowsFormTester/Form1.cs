using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            paramtest("wsewerwr{0}sdfsfd{1}", "333", "4444");
        }

        public void paramtest(string fmt, params object[] vars)
        {
            MessageBox.Show(string.Format(fmt, vars));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimeSpan span = new TimeSpan(1, 2, 0, 30, 0);
            MessageBox.Show(span.ToString());
        }


        private string Params2String(params object[] arrays)
        {
            string returnString = string.Empty;
            for (int i = 0; i<arrays.Length; i++)
            {
                if (i != arrays.Length - 1)
                    returnString += arrays[i].ToString() + ", ";
                else
                    returnString += arrays[i].ToString();

            }
            return returnString;
        }
    }
}
