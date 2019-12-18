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
        private delegate void DelegadoAcceso(string accion);

        private string strBufferIn;

        public double[] Datos = new double[20];
    
        clsCalculos mate = new clsCalculos();

        public Form1()
        {
            InitializeComponent();
        }

        private void AccesoForm(string accion) // funcion que permite obtener acceso al form
        {
            strBufferIn = accion;

            for (int i = 0; i < 19; i++)
            {
                Datos[i] = Datos[i + 1];
            }

            Datos[19] = Double.Parse(accion);

            double media = mate.calcMedia(Datos, 20);

            double desvest = mate.calcDesvest(Datos, 20, media);

            double result = (desvest / media) * 100;

            if (result < 0.3)
            {
                DatosRecibidos.Items.Add(media);
            }

            
        }

        private void AccesoInterrupcion(string accion) // funcion que adapta la interrupcion al acceso del form
        {
            DelegadoAcceso Var_DelegaadoAcceso; // variable del delegado

            Var_DelegaadoAcceso = new DelegadoAcceso(AccesoForm); // la variable apunta a la funcion AccesoForm

            object[] arg = { accion }; // genera un argumento con la info que se obtiene en el puerto

            base.Invoke(Var_DelegaadoAcceso, arg); // invoca la variable del delegado con su argumento
        }

        private void Form1_Load(object sender , EventArgs e) // inicializa las variables
        {
            strBufferIn = "";

            for (int i = 0; i < 20; i++)
            {
                Datos[i] = 0;
            }

            BotBuscarPuerto.Enabled = false;

            BotAbrirPuerto.Enabled = false;
        }

        private void BotBuscarPuerto_Click(object sender, EventArgs e) // al hacer click sobre el boton de busqueda de puerto
        {
            string[] Puertosdisponibles = SerialPort.GetPortNames(); // Carga un vector de string con los nombre de los puertos encontrados 

            ComboPuertos.Items.Clear(); // limpia la lista anterior de puertos

            foreach (string puerto_simple in Puertosdisponibles)
            {
                

                ComboPuertos.Items.Add(puerto_simple); // agrega a la lista los nombres de puertos encontrados


            }

            if (ComboPuertos.Items.Count > 0) // si encuentro algun puerto dice que seleccione el puerto deseado y habilita el boton de abrir puerto
            {
                ComboPuertos.SelectedIndex = 0;

                MessageBox.Show("SELECCIONAR PUERTO DE TRABAJO");

                BotAbrirPuerto.Enabled = true;
            }

            else // si no se encuetra el puerto dispone un mensaje
            {
                MessageBox.Show("NO HAY PUERTO DISPONIBLE");

                strBufferIn = "";


                BotAbrirPuerto.Enabled = false;
            }
        }

        private void BotAbrirPuerto_Click(object sender, EventArgs e) // Al hacer click sobre el boton de abrir puerto 
        {
            try // en el caso de no haber error
            {
                if (BotAbrirPuerto.Text == "ABRIR") // se el puerto estaba cerrado
                {
                    SpPuertos.BaudRate = Int32.Parse(comboBaudRate.Text); // toma el BaudRate definido

                    SpPuertos.DataBits = 8; // fija los bits del dato en 8

                    SpPuertos.Parity = Parity.None; // define datos sin paridad

                    SpPuertos.StopBits = StopBits.One; // define un bit de parada

                    SpPuertos.Handshake = Handshake.None; 

                    SpPuertos.PortName = ComboPuertos.Text; // guarda el nombre del puerto

                    try
                    {
                        SpPuertos.Open(); // abre el puerto

                        BotAbrirPuerto.Text = "CERRAR"; // modifica el texto del boton a "cerrar"


                    }
                    catch (Exception exc) // al producirse un error dispone el mensaje pertinente
                    {

                        MessageBox.Show(exc.Message.ToString());
                    }
                }

                else if (BotAbrirPuerto.Text == "CERRAR") // si el puerto estaba abierto, se cierra el puerto y cambia el texto del boton 
                {
                    SpPuertos.Close();

                    BotAbrirPuerto.Text = "ABRIR";
                }
            }
            catch (Exception exc) // se dispone un mensaje ante cualquier error
            {
                MessageBox.Show(exc.Message.ToString());

            }
        }

        private void Datorecibido(object sender, SerialDataReceivedEventArgs e)
        {
            /*string Data_in = SpPuertos.ReadExisting();

            MessageBox.Show(Data_in);

            //string Data2_in = Data_in;

            //DatosRecibidos.Items.Add(Data2_in);*/

            AccesoInterrupcion(SpPuertos.ReadExisting());
        }
    }
}
