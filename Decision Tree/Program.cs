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
            Matrix matrix = Tools.ReadFile("test2.txt");
            string test = Tools.DecisionTree(matrix,matrix.Instances);
            //Console.WriteLine(test);
            /*for (int i = 0; i < matrix.Content.Length;i++ )
            {
                Console.WriteLine(Tools.AttributeEntropy(matrix.Info)-Tools.Entropy(matrix.Content[i], matrix.Instances) );
            }*/
             
   /*
            foreach(Tuple<string,bool> tuple in matrix.Content[4])
            {
                Console.WriteLine(tuple.Item1 + " ," + tuple.Item2);
            }
            foreach (Tools.Triple triple in Tools.Sort(matrix.Content[4]))
            {
                Console.WriteLine(triple.Attribute + ", " + triple.True + ", " + triple.False);
            }
            */
           // Console.WriteLine(Tools.Entropy(matrix.Content[4], matrix.Instances));
            //Console.WriteLine(matrix.Info.Attribute + " " +matrix.Info.True + " " +matrix.Info.False);
           // Console.WriteLine(Tools.AttributeEntropy(matrix.Info));
            Console.ReadKey();
        }


    }
}
