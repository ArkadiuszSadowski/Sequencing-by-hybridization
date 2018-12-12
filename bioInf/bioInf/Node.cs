using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioInf
{
    public class Node
    {
        private string value;
        private bool isError;
        private int id;
        private Dictionary<Node,int> neighbourConnections;
         public  Node(string value)
         {
             isError = false;
            neighbourConnections = new Dictionary<Node, int>();
           
            this.value = value;
         }

        public void setError(bool error)
        {
            isError = error;
        }

        public bool getError()
        {
            return isError;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

   

        public void addNeighbour(Node neighbour)
        {
            
          
            int coverage = getCoverage(neighbour.getValue());
            
            neighbourConnections.Add(neighbour,coverage);
           
        }

        public int getCoverage(string neighbourValue)
        {
            int coverage = 0;
            if (neighbourValue.Equals(value))
            {
                coverage = neighbourValue.Length;
                return coverage;
            }
            else
            {
                for (int i = 1; i < value.Length; i++)
                {

                    if (value.Substring(i).Equals(neighbourValue.Substring(0, neighbourValue.Length - i)))
                    {
                        coverage = neighbourValue.Length - i;
                        return coverage;
                    }


                }
        }

            return coverage;


        }

     

        public Dictionary<Node,int> getNeighbours()
        {
            return neighbourConnections;
        }

        public string getValue()
        {
            return value;
        }

        public void sortNeighbours()
        {
            Dictionary<Node, int> sortedNeighbours = new Dictionary<Node, int>();
            foreach (KeyValuePair<Node, int> pair in neighbourConnections.OrderByDescending(key => key.Value))
            {
                sortedNeighbours.Add(pair.Key,pair.Value);
            }

           neighbourConnections.Clear();
            neighbourConnections = sortedNeighbours;



        }

        public void printNeighbours()
        {
            sortNeighbours();
            foreach (KeyValuePair<Node,int> n in neighbourConnections)
            {
               
                Console.Write( "Neighbour = {0}, Coverage = {1}", n.Key.getValue(), n.Value);
                Console.Write("\n");
            }
        }
    }
}
