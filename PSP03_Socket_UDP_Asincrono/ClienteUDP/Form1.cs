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

namespace _06_SocketClienteUDP {
  public partial class Form1 : Form {

    private UdpClient clienteUDP;
    public Form1() {
      InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        try {

                //Socket creado para la conexión
                clienteUDP = new UdpClient(); 

                //IP y puerto del servidor para la conexión
                string ipServidor = textBox1.Text;//IP
                int puertoServidor = 2000;//puerto
                IPEndPoint servidorEndpoint = new IPEndPoint(IPAddress.Parse(ipServidor), puertoServidor);

                // Recupero el mensaje del textbox y lo preparo para enviar
                byte[] mensajeEnviar = Encoding.ASCII.GetBytes(textBox2.Text);

                // Envío el mensaje al servidor de forma asíncrona
                await clienteUDP.SendAsync(mensajeEnviar, mensajeEnviar.Length, servidorEndpoint); 

                // Esperar la recepción de la respuesta del servidor 
                UdpReceiveResult resultado = await clienteUDP.ReceiveAsync();

                // Convertir la respuesta recibida en una cadena de texto
                string respuesta = Encoding.ASCII.GetString(resultado.Buffer);

                // Mostrar la respuesta en el label
                label1.Text = "Servidor: " + respuesta;
            }
            catch (Exception error) {
                Debug.WriteLine("Error: {0}", error.ToString());
            }
            finally 
            {
                if (clienteUDP != null) 
                {
                    clienteUDP.Close();
                }
            }
    }
  }
}
