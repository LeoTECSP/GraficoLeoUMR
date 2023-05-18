using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using UserControlAnimacion;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AForge.Controls;

namespace Graf2_LeonardoLoza
{
    public partial class Form1 : Form
    {
        private bool Tocado = false;
        System.Windows.Forms.PictureBox colision = new System.Windows.Forms.PictureBox();
        UserControl1 cholo = new UserControl1();
        private int contador =0;


        private int X, Y, limiteX, limiteY, XOriginal, YOriginal, mandoEstaConectado, tiempo, saltoY = 180, puntuacion = 0,dibujado; //se crean
        Joystick controlConectado;//Variable joystick que tendrá el mando usado por el usuario
        private string teclaAct;//Variable global que se usará para describir el estado de una tecla


        
        public Form1()
        {
            InitializeComponent();
            //le quite el borde y que salga al medio
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(cholo);
           
            cholo.Top = (180);

            contador = this.ClientSize.Width-80;

            colision.BackColor = Color.Azure;
            colision.Visible = false;
            Controls.Add(colision);
            this.DoubleBuffered = true;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
          
        

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            tiempo++;//Se incrementa una variable tiempo 
            if (tiempo < 15)//Se crea una condición en caso de que la variable aún no haya llegado a 15
            { cholo.Location = new Point(X, saltoY -= 20); }//Se desplaza 10 pixeles hacia arriba el cholo desde la posición que tenía
            else//en caso de no cumplirse, se realiza la siguiente condición
            {//Se abre el espacio de la condición 
                cholo.Location = new Point(X, saltoY += 20); //Baja del salto 10 pixeles
                if (cholo.Location.Y == 140)//Condición si se llega a bajar 120 pixeles
                {//Se abre la condición 
                    timer2.Enabled = false; //Se desactiva el salto cerrando con el ciclo
                    tiempo = 0;//el tiempo se reestablece para poder usarse nuevamente
                }//Se cierra la condición
            }//Se cierra el espacio de la condición
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            controlConectado = new Joystick(0);//Se crea el objeto mando e identifica si se encuentra en posición 0
            //timer3.Enabled = true;//Se activa el temporizador del modo mando
            mandoEstaConectado = 1;//Cambia la variable para indicar que si se encuentra conectado el mando
         
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //Creación de gráficos  de letras

            ///Esto es para cewar graficos y for para repetirlo
            //EL evento no va a activarse en load, necesita un iniciador
            //using (Graphics graphics = CreateGraphics())
            //using (Font font = new Font("Mandrake FF", 7,FontStyle.Regular))
            //{

            
            
            //    for (int i = 0; i < this.Width; i +=20)
            //    {
            //        //Genera verticales
            //        graphics.DrawLine(Pens.BlueViolet, i, 0, i, this.Height);

            //        //Conn graphics se puede dibujar figuras geometricas, imagenes y texto

            //        graphics.DrawString(i.ToString(), font, Brushes.Black,i,0);

            //        //graphics.DrawString(i.ToString(), font, Brushes.Black, i, this.Height - i);
            //    }
            //    for (int i = 0; i < this.Height; i += 20)
            //    {
            //        //Genera horizontales
            //        graphics.DrawLine(Pens.BlueViolet, 0, i, this.Width,i);

            //        graphics.DrawString(i.ToString(), font, Brushes.Black, 0, i);


            //    }
            //}

  
         


         }

        //Usamos onpaint para hacer dibujos desde un inicio (Desde que carga la forma, se carga
        //Una vez al inicio y modificarlo conforme la ejecución en el proyecto)

        //Se renderiza con onPaint, no se redibuja, su unica función son gráficos, aunque el cambio en la forma
        //Mediante un clic o x, vuelve a renderizarlo (lo quita y lo vuelve a dibujar
        //Onpaint es la mejor opción (Diferencia entre uno y u otro)
        //Ya no necesito using para usar graphics usando onpaint
        protected override void OnPaint(PaintEventArgs e)
        {
       
         
            //pinta del tamaño de client rectangle (Client rectangle regresa el ancho y alto del rectangulo y se actualiza si cambia)
            //Si quiero que en tiempo de ejecución me regrese el ancho y alto del formulario
            //this.DoubleBuffered = true;

            //1. Degradar el fondo (como es una instancia, debo llegar una sobrecarga)
            //Cuando trabajamos debemos crear el rectangulo o el punto

            Rectangle rectangle = new Rectangle(0, 0, ClientSize.Width, ((ClientSize.Height * 75) / 100));

            //Necesitamos la clase color para conseguir los colores disponibles en .NET, si no queremos de una gama
            //Disponible vamos a usar de fromargb
            //Un byte para un angulo de 0 a 360 o el ensamplado lineargradientmode


            LinearGradientBrush gradientBrush = new
                LinearGradientBrush(rectangle, Color.Moccasin, Color.SkyBlue, LinearGradientMode.ForwardDiagonal);

            //La diferencia entre fillrectangle es que es un rectangulo relleno dibujado, y un rectangle no se va
            //a ver, solo son medidas y colores pero no se ve en el formulario

            //primero colores y luego medidas


            //2. Dibujar el cielo
            //Color original en un inicio, un color fianl, sino que se van difuminando entre ellos
            //Los encontramos entre los 2 puntos

            e.Graphics.FillRectangle(gradientBrush, rectangle);
            //Desde el 75 al 100

            //original
            //e.Graphics.FillRectangle(gradientBrush, 0, 0, ClientSize.Width, ((ClientSize.Height * 75) / 100));



            e.Graphics.FillRectangle(Brushes.Green, 0, ((ClientSize.Height * 75) / 100), ClientSize.Width, ((ClientSize.Height * 100) / 100));
            base.OnPaint(e);
            Font font = new Font("Mandrake FF", 7, FontStyle.Regular);


            //Puede enviarse el puro color o alpha para la transparencia
            Pen penLines = new Pen(Color.FromArgb(40,52, 41, 115));
            Brush brushLines = new SolidBrush(Color.FromArgb(190, 12, 81, 96));

            //3. Dibujar una imagen con transparencia

            //Cuando uso diagonal inversa reconoce como los caracteres especiales
            //Pero como es un directorio necesito decirle que es una ruta por lo que pongo @
            Bitmap img = new Bitmap(@"C:\Users\Dell\Downloads\imagenes\sylveon.png");
            //X Y ubicación y las otras dos ancho y alto
            e.Graphics.DrawImage(img, 10, 10, 240, 240);
            //Transformar una imagen transparente
            img.MakeTransparent(Color.FromArgb(255, 255, 255));
            e.Graphics.DrawImage(img, 400, 10, 240, 240);

            //4. Dibujar una textura

            //Quiero usar una textura en un fondo
            Bitmap imgTextura = new Bitmap(@"C:\Users\Dell\Downloads\imagenes\texturachida.jpg");

            TextureBrush texture = new TextureBrush(imgTextura);

            Font font1 = new Font("Mandrake FF", 28, FontStyle.Bold);
            //en vez de pen va a dibujar con la textura, en x en el punto 10 y en y en un 85% de la foma
            e.Graphics.DrawString("GORDITAS DE NATA A 2X10 PESOS", font1, texture, 10, ((ClientSize.Height * 85) / 100));



            //Tarea - dibujar el sol (polígonos)
            //Point pt1 = new Point(170, 0);
            //Point pt11 = new Point(110, 100);
            //Point pt2 = new Point(0, 160);
            //Point pt23 = new Point(0, 0);
            //Point pt24 = new Point(170, 0);
            //Point ptpez = new Point(150, 60);
            //Point pt2 = new Point(135, 80);
            //Point pt23 = new Point(110, 100);
            //Point pt3 = new Point(0, 120);
            //Point pt4 = new Point(0, 0);
            //Point pt5 = new Point(140, 0);

            Point[] Sol =
            {
                new Point(170,0),
                new Point(110,100),
                new Point(0,160),
                new Point(0,0),
                new Point(170,0)

            };

          

            Point[] triangulo1 =
            {
                new Point(160,0),
                new Point(200,40),
                new Point(260,60),
                new Point(180,80),
                new Point(120,80),
                new Point(160,0)

            };

            Point[] triangulo2 =
            {
                new Point(120,80),
                new Point (180,140),
                new Point(180,160),
                new Point(130,130),
                new Point(80,120),
                new Point(120,80)
            };

            Point[] triangulo3 =
            {
                new Point(180,40),
                new Point(200,20),
                new Point(220,0),
                new Point(160,0),
                new Point(180,40)

            };

            Point[] triangulo4 =
            {
                new Point(50,140),
                new Point(40,180),
                new Point(20,200),
                new Point(0,160),
                new Point(50,140)


            };


            Point[] triangulo5 =
            {
                new Point(40,140),
                new Point(60,180),
                new Point(110,200),
                
                new Point(100,180),
                new Point(110,120),
                new Point(40,140)
            };
            
            Rectangle rectangle1 = new Rectangle(0, 0, 80, 80);

            e.Graphics.FillRectangle(Brushes.Red, rectangle1);
            e.Graphics.FillClosedCurve(Brushes.Orange, triangulo1);
            e.Graphics.FillClosedCurve(Brushes.Orange, triangulo2);
            e.Graphics.FillClosedCurve(Brushes.Orange, triangulo3);
            e.Graphics.FillClosedCurve(Brushes.Orange, triangulo4);
            e.Graphics.FillClosedCurve(Brushes.Orange, triangulo5);
            e.Graphics.FillClosedCurve(Brushes.Yellow, Sol);


            e.Graphics.DrawImage(Properties.Resources.maceta, contador, 240, 80, 80);

            colision.Size = new Size(80, 80);
            colision.Location = new Point(contador, 240);





            for (int i = 0; i < this.Width; i += 20)
            {
                //Genera verticales
                e.Graphics.DrawLine(penLines, i, 0, i, this.Height);

                //Conn graphics se puede dibujar figuras geometricas, imagenes y texto

                e.Graphics.DrawString(i.ToString(), font, brushLines, i, 0);

            }
            for (int i = 0; i < this.Height; i += 20)
            {
                //Genera horizontales
                e.Graphics.DrawLine(penLines, 0, i, this.Width, i);

                e.Graphics.DrawString(i.ToString(), font, brushLines, 0, i);


            }


         

            //IMPORTAR LIBRERÍA QUE CREAMOS, COPIAR EL DIRECTORIO
            //click derecho al proyecto 
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Coordenadas donde di clic
            MessageBox.Show(e.X.ToString() + " , " + e.Y.ToString());



        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = "Puntuación: "+(puntuacion += 1).ToString();
            Joystick.Status estadoControl = controlConectado.GetCurrentStatus();//Verifica el estado del control
            if (estadoControl.IsButtonPressed(Joystick.Buttons.Button2))//Condición en caso de precionar O en el mando, saltar
            {//Se abre el espacio para el código si se cumple la condición
             
                { timer2.Enabled = true; }//Se activa el temporizador para saltar
            }//Se cierra el espacio para el código si se cumple la condición




        
            if (contador == 10)
            {
                

                contador = this.ClientSize.Width - 80;
            }


         



            if (cholo.Bounds.IntersectsWith(colision.Bounds) && Tocado !=true)
                {
             
                timer2.Stop();
                timer1.Stop();
                Tocado = true;
                cholo.timer1.Stop();
               
                MessageBox.Show("juego terminado!");
                return;
            }


            contador -= 10;
            Invalidate();
            //La maceta se mueve, y el cholito topa con la maceta se topa  (usando el joystick)
            //Una propiedad, si no lo acabamos por mientras que brindan
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {


 
        
        }
         
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                cholo.Location = new Point(10, 200);


            }
            if (e.KeyCode == Keys.Space)
            {
                timer2.Enabled = true;
            }


        ///asasasas
        }
    }
}
