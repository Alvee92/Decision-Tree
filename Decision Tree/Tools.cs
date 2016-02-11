using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Decision_Tree;



public class Tools
{

    public static Matrix ReadFile(string fichier) 
    {
         //matrix composed byt an array of list of tuples
        StreamReader reader = new StreamReader(fichier);
        string ligne = reader.ReadLine();
        string[] temp = ligne.Split(',');

        string memoryBool = temp[temp.Length -1]; //est utilisé pour convertir en boolen la dernier colonne du tableau. 

        Matrix matrix = new Matrix(temp.Length - 1); //the matrix which will contain all the values
    
        while (ligne != null)
        {
           temp = ligne.Split(',');
            
            for (int i = 0; i < temp.Length - 1; i++)
            {
                if(temp[temp.Length-1] == memoryBool)
                {
                     var value = new Tuple<string, bool>(temp[i], true); //cration of a tuple which contain the value of the attribute and the class (true or not)
                     matrix.Content[i].Add(value);

                }
                else
                {
                    var value = new Tuple<string, bool>(temp[i], false);
                    matrix.Content[i].Add(value);

                }
            }

                ligne = reader.ReadLine();
                matrix.Instances++;
        }
        reader.Close();
        return matrix;
    }

    public static List<Triple> Sort(List<Tuple<string, bool>> list)
    {
        List<Triple> result = new List<Triple>();
        List<string> attribute = new List<string>();

        foreach(Tuple<string,bool> tuple in list)
        {
            if(attribute.Contains(tuple.Item1))
            {
                if(tuple.Item2)
                {
                    result.Find(item => item.Attribute == tuple.Item1).True++;
                }
                else
                {
                    result.Find(item => item.Attribute == tuple.Item1).False++;
                }
            }
            else
            {
                attribute.Add(tuple.Item1);
                result.Add(new Triple(tuple.Item1));
                if (tuple.Item2)
                {
                    result.Find(item => item.Attribute == tuple.Item1).True++;
                }
                else
                {
                    result.Find(item => item.Attribute == tuple.Item1).False++;
                }

            }
        }
        foreach(Triple triple in result)
        {
           // Console.WriteLine(AttributeEntropy(triple));
        }
        return result;
    }

    public static double AttributeEntropy(Triple info)
    {
        float total = info.True + info.False;
        float p1 = info.True / total;
        float p2 = (float)info.False / (float)total;

        //Console.WriteLine(p1 + ", " + p2);
        //Console.WriteLine("variable p1 " +p1);


        double result = (1/Math.Log(2))*(-p1 * Math.Log(p1) - p2 * Math.Log(p2));

        return result;
    }

    public static double Entropy(List<Tuple<string, bool>> list,int totalInstances)
    {
        List<Triple> attributes = Sort(list);
        double result = 0;
        foreach(Triple component in attributes)
        {
            //Console.WriteLine((double)totalInstances) ;
            result += ((component.True + component.False) / (double)totalInstances) * AttributeEntropy(component);
        }

        return result;

    }

    public class Triple
    {
        public string Attribute { get; set; }
        public int True { get; set; }
        public int False{ get; set; }

        public Triple(string attr)
        {
            this.Attribute = attr;
            this.True = 0;
            this.False =0;

        }
    }

}

     