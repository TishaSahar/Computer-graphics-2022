using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2ndLab
{
    public partial class Form1 : Form
    {
        private Pen myPen;
        private SolidBrush face;
        private SolidBrush forPnt;
        private Image canvas;
        private Graphics myGraphics;
        private List<int[]> polygons;


        private double alf, bet;        // alf - угол вертикального поворота    bet - угол горизонтального поворота

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.PictureBox pictureBox1;

        public Form1()
        {
            InitializeComponent();

            myPen = new Pen(Color.Black) { Width = 2 };
            face = new SolidBrush(Color.LightBlue);
            forPnt = new SolidBrush(Color.Blue);

            canvas = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            myGraphics = Graphics.FromImage(canvas);

            myGraphics.FillRectangle(Brushes.SkyBlue, 0, 0, pictureBox1.Height, pictureBox1.Width);

            alf = -Math.PI * (double)trackBar2.Value / (2.0 * (double)trackBar2.Maximum);
            bet = Math.PI * (double)trackBar1.Value / (2.0 * (double)trackBar1.Maximum);

            LoadPolygons();
            Draw();
        }

        private void changeAlf(object sender, EventArgs e)
        {
            alf = -Math.PI * (double)trackBar2.Value / (2.0 * (double)trackBar2.Maximum);
            Draw();
        }

        private void changeBet(object sender, EventArgs e)
        {
            bet = Math.PI * (double)trackBar1.Value / (2.0 * (double)trackBar1.Maximum);
            Draw();
        }


        private double[,] TransformPoints()
        {
            //(float)Math.PI / (176.0f / 50)
            double sinA =  Math.Sin(alf), 
                    cosA = Math.Cos(alf), 
                    sinB =  Math.Sin(bet), 
                    cosB =  Math.Cos(bet);
            double[,] Ry = new double[,]
            {
                {cosB, 0, -sinB, 0},
                { 0, 1, 0 ,0},
                { sinB, 0, cosB, 0},
                {0, 0 ,0 ,1 },
            };
            double[,] Rx = new double[,]
            {
                { 1, 0, 0, 0},
                { 0, cosA, sinA, 0},
                { 0, -sinA, cosA, 0},
                {0, 0, 0, 1},
            };
            double[,] P = new double[,]
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0, 0, 0, 0},
                { 0, 0, 0, 1},
            };

            double[,] T = MatrixMult(Ry, Rx);
            T = MatrixMult(T, P);

            return MatrixMult(points, T);
        }


        private readonly double[,] points = new double[,]
        {
                                                                        //КРЫША

            { 0, 0, 75, 1},  //0
            { 0, 35, 75, 1}, //1
            { 25, 35, 57, 1},//2
            { 25, 0, 57, 1 },//3
            { 35, 0, 57, 1 },//4
            { 35, 35, 57, 1 },//5
            { 35, 35, 55, 1 },//6
            { 35, 0, 55, 1 }, //7
            { 25, 0, 55, 1 },//8
            { 25, 35, 55, 1 },//9
            { 0, 35, 73, 1 },//10
            { 0, 0, 73, 1 }, //11
      
            { -25, 35, 57, 1},//12
            { -25, 0, 57, 1 },//13
            { -35, 0, 57, 1 },//14
            { -35, 35, 57, 1 },//15
            { -35, 35, 55, 1 },//16
            { -35, 0, 55, 1 }, //17
            { -25, 0, 55, 1 },//18
            { -25, 35, 55, 1 },//19
                                                                                    // ЗАДНЯЯ СТЕНА
            { 33, 35, 55, 1}, // 20
            { -33, 35, 55, 1},// 21
            { -33, 35, 10, 1},// 22
            { 33, 35, 10, 1 },// 23
            { 35, 35, 10, 1 },// 24
            { 35, 35, 0, 1 }, // 25
            { -35, 35, 0, 1 },// 26
            { -35, 35, 10, 1 },//27

                                                                                    // БОКОВЫЕ СТЕНЫ

            { 33, 0, 55, 1 }, // 28
            { 33, 0, 10, 1 }, // 29

            { 35, 0, 10, 1 }, // 30
            { 33, 0, 10, 1 }, // 31
            { 35, 0, 0, 1 },   // 32
            // 25
            //24


            { -33, 0, 55, 1 }, // 33
            { -33, 0, 10, 1 }, // 34
            //22 21

            { -35, 0, 10, 1 }, // 35
            { -33, 0, 10, 1 }, // 36
            // 22 27

            // 35 
            { -35, 0, 0, 1 },   // 37
            // 26 27

                                                            // ВЫСТУП СО СТУПЕНЯМИ
            //30 32 
            { 35, -22, 0, 1 }, // 38
            { 35, -22, 10, 1 },// 39

            // 38, 39
            { 20, -22, 10, 1 },// 40
            { 20, -22, 0, 1 }, // 41

            // 40 41
            { 20, 0, 0, 1 }, // 42
            { 20, 0, 10, 1 },// 43

            
            //35 37 
            { -35, -22, 0, 1 }, // 44
            { -35, -22, 10, 1 },// 45

            // 44 45
            { -20, -22, 10, 1 },// 46
            { -20, -22, 0, 1 }, // 47

            // 46 47
            { -20, 0, 0, 1 }, // 48
            { -20, 0, 10, 1 },// 49

            // 48 42
            { 20, 0, 8, 1 }, // 50
            { -20, 0, 8, 1 },// 51

            // 50 51
            { -20, -12, 8, 1 },// 52
            { 20, -12, 8, 1 }, // 53

            // 52, 53
            { 20, -12, 5, 1 }, // 54
            { -20, -12, 5, 1 },// 55

            // 54 55
            { -20, -20, 5, 1 },// 56
            { 20, -20, 5, 1 }, // 57

            // 56 57
            { 20, -20, 0, 1 }, // 58
            { -20, -20, 0, 1 },// 59


                                                            // ДВЕРЬ И ОКНО

            { 10, 0, 10, 1 }, // 60
            { 10, 0, 40, 1 }, // 61
            { -10, 0, 40, 1 },// 62
            { -10, 0, 10, 1 },// 63

            { 5, 0, 57, 1 }, // 64
            { 2.5, 0, 61.33, 1 },//65
            { 0, 0, 62, 1 }, //66
            { -2.5, 0, 61.33, 1 },//67
            { -5, 0, 57, 1, }, // 68
            { -2.5, 0, 52.67, 1 },//69
            { 0, 0, 52, 1 },//70
            { 2.5, 0, 52.67, 1 }, // 71

                                                            // КОЛОННЫ

            { 30, -15, 10, 1 }, // 72
            { 28.5, -17.6, 10, 1 },//73
            { 27, -18, 10, 1 }, // 74
            { 25.5, -17.6, 10, 1 },//75
            { 24, -15, 10, 1 }, // 76
            { 25.5, -12.4, 10, 1 },//77
            { 27, -12, 10, 1 }, //78
            { 28.5, -12.4, 10, 1 }, //79

            { -30, -15, 10, 1 }, // 80
            { -28.5, -17.6, 10, 1 },//81
            { -27, -18, 10, 1 }, // 82
            { -25.5, -17.6, 10, 1 },//83
            { -24, -15, 10, 1 }, // 84
            { -25.5, -12.4, 10, 1 },//85
            { -27, -12, 10, 1 }, //86
            { -28.5, -12.4, 10, 1 }, //87

            { 30, -15, 50, 1 }, // 88
            { 28.5, -17.6, 50, 1 },//89
            { 27, -18, 50, 1 }, // 90
            { 25.5, -17.6, 50, 1 },//91
            { 24, -15, 50, 1 }, // 92
            { 25.5, -12.4, 50, 1 },//93
            { 27, -12, 50, 1 }, //94
            { 28.5, -12.4, 50, 1 }, //95

            { -30, -15, 50, 1 }, // 96
            { -28.5, -17.6, 50, 1 },//97
            { -27, -18, 50, 1 }, // 98
            { -25.5, -17.6, 50, 1 },//99
            { -24, -15, 50, 1 }, // 100
            { -25.5, -12.4, 50, 1 },//101
            { -27, -12, 50, 1 }, //102
            { -28.5, -12.4, 50, 1 }, //103
        };

        private void LoadPolygons()
        {
            polygons = new List<int[]>();
                                                            //КРЫША
            polygons.Add(new int[] { 0, 1, 2, 3});
            polygons.Add(new int[] { 2, 3, 4, 5});
            polygons.Add(new int[] { 4, 5, 6, 7 });
            polygons.Add(new int[] { 6, 7, 8, 9 });
            polygons.Add(new int[] { 8, 9, 10, 11});
            polygons.Add(new int[] { 0, 1, 12, 13 });
            polygons.Add(new int[] { 12, 13, 14, 15 });
            polygons.Add(new int[] { 14, 15, 16, 17 });
            polygons.Add(new int[] { 16, 17, 18, 19 });
            polygons.Add(new int[] { 18, 19, 10, 11 });

                                                            //ЗАДНЯЯ СТЕНА
            polygons.Add(new int[] { 20, 21, 22, 23 });
            polygons.Add(new int[] { 23, 24, 25, 26, 27, 22 });

                                                            // БОКОВЫЕ СТЕНЫ

            polygons.Add(new int[] { 28, 20, 23, 29 });
            polygons.Add(new int[] { 23, 24, 30, 31 });
            polygons.Add(new int[] { 30, 32, 25, 24 });
            polygons.Add(new int[] { 33, 34, 22, 21 });
            polygons.Add(new int[] { 35, 36, 22, 27 });
            polygons.Add(new int[] { 35, 37, 26, 27 });
            polygons.Add(new int[] { 37, 32, 30, 35});

                                                            // ВЫСТУП СО СТУПЕНЯМИ

            polygons.Add(new int[] { 30, 32, 38, 39 });
            polygons.Add(new int[] { 38, 39, 40, 41 });
            polygons.Add(new int[] { 40, 41, 42, 43 });
            polygons.Add(new int[] { 35, 37, 44, 45 });
            polygons.Add(new int[] { 44, 45, 46, 47 });
            polygons.Add(new int[] { 46, 47, 48, 49});
            polygons.Add(new int[] { 48, 42, 50, 51 });
            polygons.Add(new int[] { 50, 51, 52, 53 });
            polygons.Add(new int[] { 52, 53, 54, 55 });
            polygons.Add(new int[] { 54, 55, 56, 57 });
            polygons.Add(new int[] { 56, 57, 58, 59 });

                                                            // ДВЕРЬ И ОКНО

            polygons.Add(new int[] { 60, 61, 62, 63 });
            polygons.Add(new int[] { 64, 65, 66, 67, 68, 69, 70, 71 });

                                                            // КОЛОННЫ

            polygons.Add(new int[] { 72, 73, 74, 75, 76, 77, 78, 79 });
            polygons.Add(new int[] { 88, 89, 90, 91, 92, 93, 94, 95 });
            polygons.Add(new int[] { 88, 72 });
            polygons.Add(new int[] { 89, 73 });
            polygons.Add(new int[] { 90, 74 });
            polygons.Add(new int[] { 91, 75 });
            polygons.Add(new int[] { 92, 76 });
            polygons.Add(new int[] { 93, 77 });
            polygons.Add(new int[] { 94, 78 });
            polygons.Add(new int[] { 95, 79 });


            polygons.Add(new int[] { 80, 81, 82, 83, 84, 85, 86, 87 });
            polygons.Add(new int[] { 96, 97, 98, 99, 100, 101, 102, 103 });
            polygons.Add(new int[] { 80, 96 });
            polygons.Add(new int[] { 81, 97 });
            polygons.Add(new int[] { 82, 98 });
            polygons.Add(new int[] { 83, 99 });
            polygons.Add(new int[] { 84, 100 });
            polygons.Add(new int[] { 85, 101 });
            polygons.Add(new int[] { 86, 102 });
            polygons.Add(new int[] { 87, 103 });

        }



        private static double[,] MatrixMult(double[,] A, double[,] B)
        {
            int ARows = A.GetLength(0), AColumns = A.GetLength(1);
            int BRows = B.GetLength(0), BColumns = B.GetLength(1);

            if (AColumns != BRows)
                throw new Exception("Нельзя перемножить данные матрицы");

            double[,] res = new double[ARows, BColumns];
            for (int rowA = 0; rowA < ARows; rowA++)
                for (int columnB = 0; columnB < BColumns; columnB++)
                    for (int columnA = 0; columnA < AColumns; columnA++)
                        res[rowA, columnB] += A[rowA, columnA] * B[columnA, columnB];
            return res;
        }

        private PointF GetPoint(double x, double y, double scale = 1.0) => 
                                    new PointF((float)(pictureBox1.Width /2 + x * scale), 
                                               (float)(pictureBox1.Height/2  -  y * scale));
        private RectangleF GetCircleRectangle(double x, double y, double scale = 1.0, double r = 2.0) => 
                                    new RectangleF((float)(pictureBox1.Width   / 2    + x * scale - r/2), 
                                                    (float)(pictureBox1.Height / 2  - y * scale),
                                                    (float)r * 2, 
                                                    (float)r * 2);

        private void Draw()
        {
            myGraphics.FillRectangle(Brushes.PapayaWhip, 0, 0, pictureBox1.Width, pictureBox1.Height);
            double scale = 2.5;
            double[,] transformedPoints = TransformPoints();
            PointF[] p;
            for (int i = 0; i < polygons.Count; i++)
            {
                p = new PointF[polygons[i].Length];
                for (int point = 0; point < polygons[i].Length; point++)
                    p[point] = GetPoint(transformedPoints[polygons[i][point], 0], transformedPoints[polygons[i][point], 1], scale);
                for (int point = 1; point < p.Length; point++)
                    myGraphics.DrawLine(myPen, p[point - 1], p[point]);
                myGraphics.DrawLine(myPen, p[0], p[p.Length - 1]);
                myGraphics.DrawPolygon(myPen, p);
                //if (isFill)
                //    graphics.FillPolygon(faceBrush, p);
            }
            //if (isPoint)
            for (int point = 0; point < transformedPoints.GetLength(0); point++)
                myGraphics.FillEllipse(forPnt, GetCircleRectangle(transformedPoints[point, 0], transformedPoints[point, 1], scale, 2));
            
            pictureBox1.Image = canvas;
        }
    }
}
