using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Numerics;

namespace Approximation
{
    public partial class Form1 : Form
    {
        MyReader Data = new MyReader();
        MyDLL CalcDll = new MyDLL();
        MyFunction MyValue = new MyFunction();

        public Form1()
        {
            InitializeComponent();

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            btnProcess.Enabled = true;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            lbData.Items.Clear();
            lbResult.Clear();
            Data.Readerfile();
            if (Data.ReaderFlag ==true && Data.MyD.Length != 0 && Data.MyD[0] != "")
            {
                Data.MyArray();
                if (Data.ReaderFlag == true)
                {
                    for (int i = 0; i < Data.MyD.Length; i++)
                        lbData.Items.Add(Data.MyD[i]);
                    try
                    {
                        for (int i = 0; i < Data.X.Length; i++)
                        {
                            chart1.Series[0].Points.AddXY(Convert.ToDecimal(Data.X[i]), Convert.ToDecimal(Data.Y[i]));
                        }
                    }
                    catch
                    {
                        chart1.Series[0].Points.Clear();
                        MessageBox.Show("Числа слишком большие для построения графика!");
                    }
                }
                else btnProcess.Enabled = false;
            }
            else
            {
                if (Data.MyD.Length == 0)
                    MessageBox.Show("Выбранный файл пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnProcess.Enabled = false;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            lbResult.Clear();
            int Power = Convert.ToInt32(numPower.Value);
            if (Power > Data.X.Length)
                MessageBox.Show("Степень должна быть меньше " + Data.X.Length + " ! ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string s = CalcDll.Calculating(Power, Data.X, Data.Y);
                if (CalcDll.ResultDll.Length != 0)
                {
                    lbResult.Text = s;
                    MyValue.Function(Power, Data.X,CalcDll.ResultDll);
                    try
                    {
                        for (int i = 0; i < Data.X.Length; i++)
                        {
                            chart1.Series[1].Points.AddXY(Convert.ToDecimal(Data.X[i]), Convert.ToDecimal(MyValue.MyValues[i]));
                        }
                    }
                    catch
                    {
                        chart1.Series[1].Points.Clear();
                        MessageBox.Show("Числа слишком большие для построения графика!");
                    }
                }
            }
        }

        private void numPower_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            lbResult.Clear();
        }
    }
}
