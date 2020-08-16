using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Numerics;

namespace Approximation
{
    class MyReader
    {
        public bool ReaderFlag = true;
        public string[] MyD;
        public double[] X;
        public double[] Y;
        public string[,] MyDE; 
        MyReadFromExcel MyEx = new MyReadFromExcel();

        public void Readerfile()
        {
            ReaderFlag = true;
            string NameOfFile = "";
            OpenFileDialog open_dialog = new OpenFileDialog();
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    NameOfFile = open_dialog.FileName;
                    string []MyData = File.ReadAllLines(NameOfFile);
                    MyD = new string[MyData.Length];
                    MyD = MyData;
                }
                catch
                {
                    ReaderFlag = false;
                    MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void MyReaderExcel()
        {
            ReaderFlag = true;
            OpenFileDialog open_dialog = new OpenFileDialog();
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MyEx.GetData(open_dialog.FileName);
                    MyDE = new string[MyEx.NRow, 2];
                    MyDE = MyEx.MyData;
                }
                catch
                {
                    ReaderFlag = false;
                    MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void MyArrayFromExcel()
        {
            try
            {
                X = new double[MyEx.NRow];
                Y = new double[MyEx.NRow];
                for (int i = 0; i < MyEx.NRow; i++)
                {
                    X[i] = Convert.ToDouble(MyEx.MyData[0,i]);
                    Y[i] = Convert.ToDouble(MyEx.MyData[1, i]);
                }
            }
            catch
            {
                ReaderFlag = false;
                for (int i = 0; i < MyEx.NRow; i++)
                {
                    MyDE = null;
                    X[i] = 0;
                    Y[i] = 0;
                }
                MessageBox.Show("Проверьте данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MyArray ()
        {
            try
            {
                X = new double[MyD.Length];
                Y = new double[MyD.Length];
                for (int i = 0; i < MyD.Length; i++)
                {
                    string[] My = MyD[i].Split(';');
                    X[i] = Convert.ToDouble(My[0]);
                    Y[i] = Convert.ToDouble(My[1]);
                }
            }
            catch
            {
                ReaderFlag = false;
                for (int i = 0; i < MyD.Length; i++)
                {
                    MyD[i] = "";
                    X[i] = 0;
                    Y[i] = 0;
                }
                MessageBox.Show ("Проверьте данные!","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
