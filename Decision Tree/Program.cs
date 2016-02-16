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
            Matrix matrix = Tools.ReadFile("test.txt");
            string test = Tools.DecisionTree(matrix,matrix.Instances);
            Console.WriteLine(test);
            
           
            Console.ReadKey();
        }


    }
}
