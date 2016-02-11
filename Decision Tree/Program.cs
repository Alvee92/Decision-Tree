using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = Tools.ReadFile("Qualitative_Bankruptcy.data.txt");

            Tools.Info(matrix.Content[0]);
            Console.WriteLine(Math.Log(3/5));
            double test = 3.00 / 5.00;
            
            Console.WriteLine(test);

            Console.ReadKey();
        }


    }
}
