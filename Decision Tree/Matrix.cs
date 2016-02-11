using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree
{
    public class Matrix
    {
        public List<Tuple<string, bool>>[] Content;


        public Matrix(int length)
        {
            Content = new List<Tuple<string, bool>>[length];
            for (int i = 0; i < length; i++)
            {
                Content[i] = new List<Tuple<string, bool>>();

            }
        }
    }
}
