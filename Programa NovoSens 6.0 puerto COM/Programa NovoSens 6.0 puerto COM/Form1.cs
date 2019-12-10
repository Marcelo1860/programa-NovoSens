using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Programa_NovoSens_6._0_puerto_COM
{
    public partial class Form1 : Form
    {
        string strBufferIn;

        string strBufferOut;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender , EventArgs e)
        {
            strBufferIn = "";

            strBufferOut = "";

            BotBuscarPuerto.Enabled = false;
        }
    }
}
