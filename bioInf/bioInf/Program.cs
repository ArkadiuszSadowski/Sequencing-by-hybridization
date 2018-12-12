using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioInf
{
    class Program
    {
        private static List<Node> errorsList;
        private static List<Node> graph;
        private static List<PossibleConnection> possibleBest;
        static void Main(string[] args)
        {
            errorsList= new List<Node>();
            possibleBest = new List<PossibleConnection>();
            string[] spectrum ={"ACT", "CTC", "GCC","CAA","TCT", "TGG"};
            graph = new List<Node>();
            createCompleteGraph(spectrum);
            getPossibleWays();
            printConnections();

            // Node n = new Node("CTC");
            //n.addNeighbour("TCD");
            //n.printNeighbours();

            Console.Read();
        }

        public static void createCompleteGraph(string[]spectrum)
        {
            for(int i=0; i<spectrum.Length;i++) 
            {   
                Node n = new Node(spectrum[i]);
                int next = i + 1;
                if (next >= spectrum.Length)
                {
                    next = spectrum.Length-1;
                }

              
                int before = i - 1;
                if (before >= 0)
                {



                    if (graph[graph.Count-1].getCoverage(n.getValue()) == 0)
                    {
                        if (n.getCoverage(spectrum[next]) == 0)
                        {
                            errorsList.Add(n);
                        }
                        graph.Add(n);
                        n.setId(graph.Count);

                    }
                    else
                    {
                        graph.Add(n);
                        n.setId(graph.Count);
                    }
                }
                else
                {
                    if ( n.getCoverage(spectrum[next]) == 0)
                    {
                        errorsList.Add(n);
                    }
                    else
                    {
                        graph.Add(n);
                        n.setId(graph.Count);
                    }
                }




            }

            int position = 0;
            foreach (var node in graph)
            {
                foreach (var node1 in graph)
                {
                    if (node1 != node)
                    {
                        node.addNeighbour(node1);
                    }
                    
                }
             
            }

           
            printGraph();
           


        }

       


        public static void printGraph()
        {
            foreach (var node in graph)
            {
                Console.Write(node.getValue()+"    "+node.getId()+"    "+node.getError()+"\n");
               
                node.printNeighbours();
                Console.Write("\n");
            }

        }

        private static void getPossibleWays()
        {
          
            
                PossibleConnection pc = new PossibleConnection(graph[0].getValue());
                pc.addCoveredNodes(graph[0]);
               getWay(pc, graph[0]);
                possibleBest.Add(pc);
            


        }

        private static void getWay(PossibleConnection pc, Node node)
        {
            foreach (var neighbour in node.getNeighbours())
            {
                if (!pc.getCoveredNodes().Exists(x=>x.getId()==neighbour.Key.getId()))
                {
                    pc.addCoveredNodes(neighbour.Key);
                    StringBuilder sb = new StringBuilder();
                    
                   if(neighbour.Value == 0 )
                    {
                        string currentSuperstring = pc.getSuperString();
                        string superstring = sb.Append(currentSuperstring).Append(" ").Append(neighbour.Key.getValue())
                            .ToString();
                        pc.setSuperString(superstring);
                    }
                    else
                    {
                        
                        string currentSuperstring = pc.getSuperString();
                        string toadd = neighbour.Key.getValue()
                            .Substring(neighbour.Key.getValue().Length - (neighbour.Key.getValue().Length - neighbour.Value));
                        string superstring = sb.Append(currentSuperstring).Append(toadd).ToString();
                        pc.setSuperString(superstring);
                    }

                    pc.addSumCoverage(neighbour.Value);
                    
                    getWay(pc, neighbour.Key);
                }
                
            }

            return;
        }
        
        private static void printConnections()
            {
                foreach (var possibleConnection in possibleBest)
                {
                        Console.Write(possibleConnection.getSuperString()+"\n");
                    List<Node> nodelist = possibleConnection.getCoveredNodes();
                    foreach (var node in nodelist)
                    {
                            Console.Write(node.getValue()+" ");
                    }
                    Console.Write("\n "+ possibleConnection.getSumCoverage());
                    Console.Write("\n");
                }
            }
        }
}


