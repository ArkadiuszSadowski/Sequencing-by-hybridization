using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioInf
{
    class Node
    {
        private string value;
       private int coverage;
        
        private List<Node> neighbourConnections;
         public  Node(string value)
         {
            neighbourConnections = new List<Node>();
           
            this.value = value;
         }

        private void setCoverage(int coverage)
        {
            this.coverage = coverage;
        }

        public void addNeighbour(string neighbour)
        {
            Node n = new Node(neighbour);
           
            int coverage = getCoverage(neighbour);
            n.setCoverage(coverage);
            neighbourConnections.Add(n);
           
        }

        private int getCoverage(string neighbourValue)
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

        public int getSavedCoverageValue()
        {
            return coverage;
        }

        public List<Node> getNeighbours()
        {
            return neighbourConnections;
        }

        public string getValue()
        {
            return value;
        }

        public void sortNeighbours()
        {
         
            Node temp;

            for (int write = 0; write < neighbourConnections.Count; write++)
            {
                for (int sort = 0; sort < neighbourConnections.Count - 1; sort++)
                {
                    if (neighbourConnections[sort].getSavedCoverageValue() > neighbourConnections[sort + 1].getSavedCoverageValue())
                    {
                        temp = neighbourConnections[sort + 1];
                        neighbourConnections[sort + 1] = neighbourConnections[sort];
                        neighbourConnections[sort] = temp;
                    }
                }
            }

            neighbourConnections.Reverse(0, neighbourConnections.Count);

        }

        public void printNeighbours()
        {
            sortNeighbours();
            foreach (Node n in neighbourConnections)
            {
               
                Console.Write( "Neighbour = {0}, Coverage = {1}", n.getValue(), n.getSavedCoverageValue());
                Console.Write("\n");
            }
        }
    }
}
