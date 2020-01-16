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

        private double[] Datos = new double[20];

        private double[] Datossec = new double[10];

        int cond = 0;

        int contador = 0;

        private double[] Escalones = new double[2];

        public double[] Saltos = new double[3];

    
        clsCalculos mate = new clsCalculos();

        public Form1()
        {
            InitializeComponent();
        }

        private void AccesoForm(string accion) // funcion que permite obtener acceso al form
        {
            strBufferIn = accion;

            if (cond == 0) // compara si se observa estabilidad o salto
            {
                for (int i = 0; i < 19; i++) // carga vector
                {
                    Datos[i] = Datos[i + 1];
                }

                Datos[19] = Double.Parse(accion);

                double media = mate.calcMedia(Datos, 20); // calcula media

                double desvest = mate.calcDesvest(Datos, 20, media); // cacula desviacion 

                double result = (desvest / media) * 100; // calcula cv%

                if (result < 0.3) // compara CV% para estimar estabilidad
                {

                    cond = 1; // cambia a condicion de deteccion de saltos
                    
                    int j = 0;

                    if (contador > 0) // guarda datos y calcula saltos si es necesario
                    {



                        switch (contador)
                        {
                            

                            case 1:
                                Escalones[1] = media;
                                Saltos[0] = Escalones[1] - Escalones[0];
                                DatosRecibidos.Items.Add("Salto 1");
                                DatosRecibidos.Items.Add(Saltos[0]);
                                contador++;
                                break;
                            case 2:
                                Escalones[1] = media;
                                Saltos[1] = Escalones[1] - Escalones[0];
                                DatosRecibidos.Items.Add("Salto 2");
                                DatosRecibidos.Items.Add(Saltos[1]);
                                contador++;
                                break;
                            case 3:
                                Escalones[1] = media;
                                Saltos[2] = Escalones[1] - Escalones[0];
                                DatosRecibidos.Items.Add("Salto 2");
                                DatosRecibidos.Items.Add(Saltos[2]);
                                contador = 0;
                                cond = 0;
                                break;

                        }

                    }

                    else
                    {
                        contador++;
                    }

                    for (int i = 19; i > 9 ; i--) // prepara el vector de diez elementos para la fase de deteccion de saltos
                    {

                        Datossec[j] = Datos[i];

                        j++;
                    }
                }
            }

            else if (cond == 1) // fase de deteccion de saltos
            {
                double media = mate.calcMedia(Datossec, 10); // calcula la media del vector de 10 elementos

                double control = Double.Parse(accion); // obtiiene un dato para revisar salto

                double aux = media + 25; // estima el tamaño del posible salto

                if (control > aux) // revisa la existencia de un salto
                {

                    Escalones[0] = media; // fija la base delcalculo de saltos

                    cond = 0;
                }

                else // renueva los valores del vector
                {
                    for (int i = 0; i < 9; i++)
                    {
                        Datossec[i] = Datossec[i + 1];
                    }

                    Datossec[9] = Double.Parse(accion);
                }

     

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
