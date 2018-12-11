using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioInf
{
    class Program
    {
        private static List<Node> graph;
        static void Main(string[] args)
        {
            string[] spectrum ={"ACT", "CTC", "GCC", "TCT", "TGG"};
            graph = new List<Node>();
            createCompleteGraph(spectrum);
            
            // Node n = new Node("CTC");
            //n.addNeighbour("TCD");
            //n.printNeighbours();

            Console.Read();
        }

        public static void createCompleteGraph(string[]spectrum)
        {
            int position = 0;
            foreach (var word in spectrum)
            {   
                Node n = new Node(word);
                for (int i = 0; i < spectrum.Length; i++)
                {
                    if (position != i)
                    {
                        n.addNeighbour(spectrum[i]);
                    }

               
                }
                position++;
                graph.Add(n);


            }

            printGraph();
        }

        public static void printGraph()
        {
            foreach (var node in graph)
            {
                Console.Write(node.getValue()+"    ");
               
                node.printNeighbours();
                Console.Write("\n");
            }

        }

        private void getPossibleWays()
        {   
            
        }
        

        public class PossibleConnection
        {
            private string superString;
            private List<Node> mergedList;
            private int cover;
            public PossibleConnection(string superstring)
            {
                this.superString = superstring;
                mergedList = new List<Node>();
                cover = 0;

            }

            public string getSuperString()
            {
                return superString;
            }

            public void setSuperString(string superString)
            {
                this.superString = superString;
            }
            public List<Node> getCoveredNodes()
            {
                return mergedList;
            }

            public void addCoveredNodes(Node node)
            {
               mergedList.Add(node);
            }
            public int getSumCoverage()
            {
                return cover;
            }

            public void addSumCoverage(int coverage)
            {
                cover += coverage;
            }
        }

    }
}
