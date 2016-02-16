using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Decision_Tree;



public class Tools
{

    public static Matrix ReadFile(string fichier) //Read a file and create a matrix with the gathered data
    {
         //matrix composed byt an array of list of tuples
        StreamReader reader = new StreamReader(fichier);
        string ligne = reader.ReadLine();
        string[] temp = ligne.Split(',');

        string memoryBool = temp[temp.Length -1]; //est utilisé pour convertir en boolen la dernier colonne du tableau. 
        
        Matrix matrix = new Matrix(temp.Length - 1); //the matrix which will contain all the values
        matrix.Info.Attribute = temp[temp.Length - 1];
        while (ligne != null)
        {
           temp = ligne.Split(',');
            
            if(temp[temp.Length-1] == memoryBool)
                {

                matrix.Info.True++;

                for (int i = 0; i < temp.Length - 1; i++)
                    {
                     var value = new Tuple<string, bool,int>(temp[i], true,i); //cration of a tuple which contain the value of the attribute and the class (true or not)
                     matrix.Content[i].Add(value);
    
                    }
              }
                else
                {
                    matrix.Info.False++;

                    for (int i = 0; i < temp.Length - 1; i++)
                    {
                        var value = new Tuple<string, bool,int>(temp[i], false,i);
                        matrix.Content[i].Add(value);
                    }

                }


            ligne = reader.ReadLine();
            matrix.Instances++;
            
            }

        
        reader.Close();
        return matrix;
    }

    public static List<Triple> Sort(List<Tuple<string, bool,int>> list) //sort a list of a matrix, return a list of triple(containing the name of the attribute, and the result true/false)
    {
        List<Triple> result = new List<Triple>();
        List<string> attribute = new List<string>();

        foreach(Tuple<string,bool,int> tuple in list)
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

    public static double Entropy(List<Triple> list,Triple matrixInfo, int totalInstances)
    {
        double result = 0;

        foreach(Triple component in list)
        {
            result += ((component.True + component.False) / (double)totalInstances) * AttributeEntropy(component);
        }
        return result;
    }

    public static double Gain(List<Triple> list,Triple matrixInfo, int totalInstances)
    {
        return AttributeEntropy(matrixInfo) - Entropy(list, matrixInfo, totalInstances);
    }
    public static double AttributeEntropy(Triple info) //Compute the value of the entropy for a given Triple (for example <Sunny,2,3> => Info([2,3]) )
    {
        float total = info.True + info.False;
        float p1 = (float)info.True / (float)total;
        float p2 = (float)info.False / (float)total;

        //Console.WriteLine(p1 + ", " + p2);
        //Console.WriteLine("variable p1 " +p2);

        //Console.WriteLine(Math.Log(1));
        if(p1 ==0 || p2 ==0)
        {
            return 0;
        }
        else
        {
            double result = (1 / Math.Log(2)) * (-p1 * Math.Log(p1) - p2 * Math.Log(p2));
            return result;


        }

    }
    public static List<Matrix> NewMatrix(Matrix matrix,List<Triple>matrixInfo, int indiceRemoveAt) //create the new matrixes after choosing the attribute
    {
        List<Matrix> ListMatrix = new List<Matrix>();

        foreach(Triple tripleAttr in matrixInfo)
        {
            string attribute = tripleAttr.Attribute;
            List<int> listIndice = new List<int>();

            for(int i =0; i<matrix.Content[indiceRemoveAt].Count;i++)
            {
                if (matrix.Content[indiceRemoveAt][i].Item1 == attribute)
                    listIndice.Add(i);
            }
            
            if (matrix.Content.Length > 0)
            {
                Matrix newMatrix = new Matrix(matrix.Content.Length - 1);
                int indiceNewMat = 0;
                for (int i = 0; i < matrix.Content.Length ; i++) 
                {
                    if (i != indiceRemoveAt)
                    {
                        for (int indiceDepth = 0; indiceDepth < listIndice.Count; indiceDepth++)
                        {
                             newMatrix.Content[indiceNewMat].Add(matrix.Content[i][listIndice[indiceDepth]]);
                            
                        }
                        indiceNewMat++;
                    }
                   
                    
                }

                if (newMatrix.Content.Length > 0)
                {
                    newMatrix.Instances = newMatrix.Content[0].Count;
                }
                else { Console.WriteLine("test1"); }
               
                ListMatrix.Add(newMatrix);
            }
            

        }

        return ListMatrix;
    }
    public static string DecisionTree(Matrix matrix,int totalInstances) //Compute the total entropy of an attribute in the matrix (corresponding to a column of the matrix)
    {
        List<Triple> [] matrixInformations = new List<Triple>[matrix.Content.Length];
        //Console.WriteLine(AttributeEntropy(matrix.Info));
        for (int i =0; i<matrix.Content.Length; i++)
        {
            matrixInformations[i] = Sort(matrix.Content[i]); //Sort the matrix to get the number of true/false of each attribute in the columns
        }

        
        string result = ""; //will contain the final tree path

        int indiceMax = 0; //used to get the indice of the columns which will be chosen for the tree
        double max = 0; //get the max gain of the matrix
         
        
        for (int i =0; i<matrixInformations.Length; i++) //compute the gain of each columns
        {
            double gain = Gain(matrixInformations[i],matrix.Info,matrix.Instances);
            if(gain> max) //find the max gain and it's indice
            {
                max = gain;
                indiceMax = i;
            }
        }

        List<Matrix> listd = new List<Matrix>();

        if (matrixInformations.Length > 0)
        {
            listd = NewMatrix(matrix, matrixInformations[indiceMax], indiceMax);
        }
        else { Console.WriteLine("test2"); }


        
        //Begining of recusrsivity
        if (max == AttributeEntropy(matrix.Info)) //if all the attributes have a null entropy, the path is finished
        {
            return result + "path completed \n";
        }
        else //else we have to create few new matrixes, one for each branch of the tree
        {
            
                result += "attribute " + matrix.Content[indiceMax][0].Item3 + " \n"; //write the first attribute which has been chosen
            
            List<Matrix> NewMatrixes = new List<Matrix>();
            
                NewMatrixes = NewMatrix(matrix, matrixInformations[indiceMax], indiceMax);
           

            foreach(Matrix matrixes in NewMatrixes)
            {
                Console.WriteLine(DecisionTree(matrixes, matrixes.Instances));
                //result += " " + DecisionTree(matrixes,matrixes.Instances);
            }
                }
        


        return result;

    }


    public class Triple
    {
        public string Attribute { get; set; }
        public int True { get; set; }
        public int False{ get; set; }


        public Triple()
        {
            this.True = 0;
            this.False = 0;

        }
        public Triple(string attr)
        {
            this.Attribute = attr;
            this.True = 0;
            this.False =0;

        }
    }

}

     