using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree
{
    public class Matrix
    {
        public List<Tuple<string, bool,int>>[] Content;
        public int Instances;
        public Tools.Triple Info;


        public Matrix(int length)
        {
            Instances = 0;
            Info = new Tools.Triple();
            Content = new List<Tuple<string, bool,int>>[length];
            for (int i = 0; i < length; i++)
            {
                Content[i] = new List<Tuple<string, bool,int>>();

            }
        }
    }
}
