using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.IO;

namespace Spirograph2._0
{
    public partial class Form1 : Form
    {
        List<Point> MyPolygon = new List<Point>();
        Pen pen1;
        int x01, x02, x, y01, y02, y;
        Bitmap canvas1, canvas2;

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = textBox1.ForeColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                pen1.Color = MyDialog.Color;
            pen1.Width = Convert.ToInt32(textBox4.Text);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var image = new Bitmap(dialog.FileName);

                AutoScroll = true;
                AutoScrollMinSize = image.Size;

                //this.BackgroundImage = image;

                pictureBox1.Image = image;
                pictureBox1.Update();

                pictureBox2.Image = image;
                pictureBox2.Update();

                Invalidate();
            }


        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch   
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (pictureBox2.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        
                        pictureBox2.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        Graphics user_Graphics1, user_Graphics2;


        public Form1()
        {
            InitializeComponent();

            // 1
            pen1 = new Pen(Color.Red, Convert.ToInt32(textBox4.Text));
            canvas1 = new Bitmap(pictureBox1.Width,
            pictureBox1.Height);
            user_Graphics1 = Graphics.FromImage(canvas1);
            x01 = (int)(pictureBox1.Width / 2);
            y01 = (int)(pictureBox1.Height / 2);
            user_Graphics1.FillRectangle(Brushes.White, 0, 0,
                                           pictureBox1.Width, pictureBox1.Height);
            user_Graphics1.DrawLine(Pens.Gray, x01, 2, x01,
                                    pictureBox1.Height - 2);
            user_Graphics1.DrawLine(Pens.Gray, 2, y01,
                                     pictureBox1.Width - 2, y01);
            pictureBox1.Image = canvas1;

            // 2

            canvas2 = new Bitmap(pictureBox2.Width,
                                    pictureBox2.Height);
            user_Graphics2 = Graphics.FromImage(canvas2);
            x02 = (int)(pictureBox2.Width / 2);
            y02 = (int)(pictureBox2.Height / 2);
            user_Graphics2.FillRectangle(Brushes.White, 0, 0,
                                           pictureBox2.Width, pictureBox2.Height);
            user_Graphics2.DrawLine(Pens.Gray, x02, 2, x02,
                                    pictureBox2.Height - 2);
            user_Graphics2.DrawLine(Pens.Gray, 2, y02,
                                     pictureBox2.Width - 2, y02);
            pictureBox2.Image = canvas2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            int iA = Convert.ToInt32(textBox1.Text);
            int iB = Convert.ToInt32(textBox2.Text);
            int iD = Convert.ToInt32(textBox3.Text);


            pen1.Width = Convert.ToInt32(textBox4.Text);

            if (!(Int32.TryParse(textBox1.Text, out x) && Int32.TryParse(textBox2.Text, out y)))
                MessageBox.Show("Ввод данных произведен неверно!", "Ошибка ввода",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);

            if ((iA < iB) || (iB < iD))
            {
                return;
            }

            x = iA; y = iB;
            while (x %y != 0 && y%x != 0)
             {
                if (x > y)
                {
                    x = x - y;
                }
                else
                    y = y - x;
            }

            if (x > y) x = y; 

            double n = (double)iB / (double)x;
            double C = (double)iA / (double)iB;

            user_Graphics1.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
            user_Graphics1.DrawLine(Pens.Gray, x01, 2, x01,
                                     pictureBox1.Height - 2);
            user_Graphics1.DrawLine(Pens.Gray, 2, y01,
                                     pictureBox1.Width - 2, y01);

            Point[] points = new Point[(int)((double)(pictureBox1.Height + pictureBox1.Width)* Math.PI * n)];

            double Cx = (double)pictureBox1.Width /  (2.0 * (double)(iA + iB + iD));
            double Cy = (double)pictureBox1.Height / (2.0 * (double)(iA + iB + iD));

            int i = 0;
            while (i < points.Length)
            {
                x =  (int)(Cx * (double)(iA - iB) * Math.Cos((double)i / (double)(pictureBox1.Height + pictureBox1.Width)) + iD * Cx * Math.Cos(C*(double)i/ 100.0)) + x01;
                y =  (int)(Cy * (double)(iA - iB) * Math.Sin((double)i / (double)(pictureBox1.Height + pictureBox1.Width)) - iD * Cy * Math.Sin(C*(double)i/ 100.0)) + y01;
                
                points[i] = new Point(x, y);
                i++;
            }
            

            user_Graphics1.DrawCurve(pen1, points);
            pictureBox1.Image = canvas1;
            pictureBox1.Update();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int iA = Convert.ToInt32(textBox1.Text);
            int iB = Convert.ToInt32(textBox2.Text);
            int iD = Convert.ToInt32(textBox3.Text);


            pen1.Width = Convert.ToInt32(textBox4.Text);


            if (!(Int32.TryParse(textBox1.Text, out x) && Int32.TryParse(textBox2.Text, out y)))
                MessageBox.Show("Ввод данных произведен неверно!", "Ошибка ввода",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);

            if ((iA < iB) || (iB < iD))
            {
                return;
            }

            x = iA; y = iB;
            while (x % y != 0 && y % x != 0)
            {
                if (x > y)
                {
                    x = x - y;
                }
                else
                    y = y - x;
            }

            if (x > y) x = y;

            double n = (double)iB / (double)x;
            double C = (double)iA / (double)iB;

            // 2nd box draw axis
            user_Graphics2.FillRectangle(Brushes.White, 0, 0, pictureBox2.Width, pictureBox2.Height);
            user_Graphics2.DrawLine(Pens.Gray, x02, 2, x02,
                                    pictureBox2.Height - 2);
            user_Graphics2.DrawLine(Pens.Gray, 2, y02,
                                     pictureBox2.Width - 2, y02);

            Point[] points = new Point[(int)((double)(pictureBox2.Height + pictureBox2.Width) * Math.PI * n)];

            double Cx = (double)pictureBox2.Width / (2.0 * (double)(iA + iB + iD));
            double Cy = (double)pictureBox2.Height /(2.0 * (double)(iA + iB + iD));

            int i = 0;
            while (i < points.Length)
            {
                x = (int)(Cx * (double)(iA - iB) * Math.Cos((double)i / (double)(pictureBox2.Height + pictureBox2.Width)) + iD * Cx * Math.Cos(C * (double)i / 100.0)) + x02;
                y = (int)(Cy * (double)(iA - iB) * Math.Sin((double)i / (double)(pictureBox2.Height + pictureBox2.Width)) - iD * Cy * Math.Sin(C * (double)i / 100.0)) + y02;

                points[i] = new Point(x, y);
                i++;
            }
            user_Graphics2.DrawCurve(pen1, points);
            pictureBox2.Image = canvas2;
            pictureBox1.Update();
        }

    }
}
