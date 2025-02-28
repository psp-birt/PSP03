using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _05_SocketServidorUDP
{
    public partial class Form1 : Form
    {

        private UdpClient miSocketServidor;
        private const int puerto = 2000;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Se crea el socket con el puerto en el que se queda escuchando el servidor.
                miSocketServidor = new UdpClient(puerto);


                while (true)
                {
                    
                    //Recibe información de forma asíncrona por parte del cliente
                    UdpReceiveResult result = await miSocketServidor.ReceiveAsync();

                    //Guarda los datos del contenido en un array y los vuelca a un string
                    byte[] datosRecibidos = result.Buffer;
                    string mensajeRecibido = Encoding.ASCII.GetString(datosRecibidos);

                    //Guarda los datos de la información del IPEndPoint del cliente conectado, para retransmitirle posteriormente la información.
                    IPEndPoint clienteEndpoint = result.RemoteEndPoint;

                    //Muestra el mensaje de la conexión del cliente en el label
                    label1.Text = "Datos recibidos del cliente: " + clienteEndpoint.ToString();

                    //Recoge info del textbox de ese momento y se lo envía al cliente para que lo procese.
                    byte[] mensajeRespuesta = Encoding.ASCII.GetBytes(textBox1.Text);
                    await miSocketServidor.SendAsync(mensajeRespuesta, mensajeRespuesta.Length, clienteEndpoint);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (miSocketServidor != null)
                {
                    miSocketServidor.Close();
                }
            }
        }
    }
}
        

