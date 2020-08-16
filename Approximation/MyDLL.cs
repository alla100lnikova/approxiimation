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
    class MyDLL
    {
        public double[] ResultDll;
        public string Calculating(int Power, double[] X, double[] Y)
        {
            double[] Fact = Process.MyData(Power, X, Y);
            if (Fact.Length != 0)
            {
                int i;
                string s = "";
                for (i = 0; i <= Power&&(!double.IsNaN(Fact[i])); i++)
                {
                    if (Fact[i] >= 0)
                    {
                        if (i == 0)
                            s += ((Math.Round(Fact[i], 4)) + "x^" + i + "");
                        else s += ("+" + (Math.Round(Fact[i], 4)) + "x^" + i);
                    }
                    else
                    {
                        if (i == 0)
                            s += ((Math.Round(Fact[i], 4)) + "x^" + i + "");
                        else s += ((Math.Round(Fact[i], 4)) + "x^" + i);
                    }
                }
                if (i > Power)
                {
                    ResultDll = new double[Fact.Length];
                    ResultDll = Fact;
                    return s;
                }
                else
                {
                    ResultDll = new double[Fact.Length];
                    ResultDll = Fact;
                    MessageBox.Show("Выход за границы типа double! ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
            }
            else
            {
                MessageBox.Show("Имеется несколько решений, проверьте данные! ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

        }
    }
}
