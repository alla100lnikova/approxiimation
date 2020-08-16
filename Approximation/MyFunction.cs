using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Approximation
{
    class MyFunction
    {
        public double[] MyValues;

        public void Function(int Power, double[] X, double[] MyFact)
        {
            MyValues = new double[X.Length];
            for (int i = 0; i < MyValues.Length; i++)
            {
                for (int j = 0; j < MyFact.Length; j++)
                {
                    MyValues[i] += Math.Pow(X[i], j) * MyFact[j];
                }
            }
        }
    }
}
