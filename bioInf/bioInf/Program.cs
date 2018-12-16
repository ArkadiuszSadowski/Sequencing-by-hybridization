using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bioInf
{
    class Program
    {
        private static List<Node> errorsList;
        private static List<Node> graph;
        private static List<PossibleConnection> possibleBest;
        private static string[] spectrum;
        static void Main(string[] args)
        {
            errorsList = new List<Node>();

            //string[] spectrum = { "ACT", "CTC", "GCC", "TCT", "TGG" };
            possibleBest = new List<PossibleConnection>();
            Console.Write(
                "Welcome to the DNA hybridization simulator! \nPlease follow the instructions printed on this screen.\n");
            Console.Write("Would you like to enter your own DNA sequence? y/n \n");
            string answer = "";
            answer = Console.ReadLine();
            string sequence = "";
            bool correctSequence = false;
            int sequenceLength = 0;
            int howManyToRemove = 0;
            bool performed = false;
            graph = new List<Node>();
            if (answer.ToLower().Equals("y"))
            {
                while (!correctSequence)
                {


                    Console.WriteLine("Please enter DNA sequence. Possible nucleobases 'A', 'C', 'G', 'T'.");
                    sequence = Console.ReadLine();
                    Regex regex1 = new Regex(@"[ACGT]");
                    if (!regex1.IsMatch(sequence))
                    {
                        Console.Write("Incorrect sequence!\n");
                    }
                    else
                    {
                        correctSequence = true;
                    }
                }

                spectrum = getSpectrum(sequence);
                PrintSpectrum(spectrum);
            }
            else
            {
                bool isDigit = false;
                while (!isDigit)
                {
                    Console.Write("Please enter the length of the generated DNA sequence.\n");
                    string length = Console.ReadLine();

                    bool isNumeric = int.TryParse(length, out sequenceLength);
                    if (isNumeric && sequenceLength > 3)
                    {
                        isDigit = true;
                    }
                    else
                    {
                        Console.Write("Invalid argument! Must be numeric and greater than 3.\n");
                    }
                }

                sequence = GenerateRandomSequence(sequenceLength);
                Console.WriteLine(sequence);
                spectrum = getSpectrum(sequence);
                PrintSpectrum(spectrum);

            }

            Console.WriteLine("Would you like to use the whole spectrum? y/n");
            answer = Console.ReadLine();
            if (answer.ToLower().Equals("y"))
            {
                createCompleteGraph(spectrum);
                getPossibleWays();
                printConnections();
                performed = true;


            }
            else
            {
                Console.WriteLine(
                    "Would you like to use part of the spectrum? (Please choose how many codons you'd like to remove from the sequence) 0-3");

                bool good = false;
                while (!good)
                {
                    correctSequence = int.TryParse(Console.ReadLine(), out howManyToRemove);

                    if (correctSequence && howManyToRemove < 4)
                    {
                        good = true;
                        if (howManyToRemove > 0)
                        {
                            Console.WriteLine("Should the codons be removed randomly? y/n");
                            answer = Console.ReadLine();
                            if (answer.ToLower().Equals("y"))
                            {
                                List<int> toRemove = GetRandomNumbers(howManyToRemove, spectrum.Length);
                                List<string> spectrumList = spectrum.ToList();
                                List<string> removedCodons = new List<string>();
                                foreach (var i in toRemove)
                                {
                                    removedCodons.Add(spectrumList[i]);
                                    spectrumList.RemoveAt(i);
                                }

                                spectrum = spectrumList.ToArray();
                                Console.Write("The removed codons were: [");
                                foreach (var s in removedCodons)
                                {
                                    Console.Write(" " + s + " ");
                                }

                                Console.Write("]\n");
                                PrintSpectrum(spectrum);
                            }
                        }
                        else
                        {
                            if (howManyToRemove > 0)
                            {
                                Console.WriteLine(
                                    "Please write the codons, each in a separate line, that should be removed from the spectrum. ");
                                List<string> spectrumList = spectrum.ToList();
                                for (int i = 0; i < howManyToRemove; i++)
                                {
                                    string toRemove = Console.ReadLine();
                                    if (spectrumList.Contains(toRemove))
                                    {
                                        spectrumList.Remove(toRemove);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Could not find following codon!");
                                        i--;
                                    }
                                }

                                spectrum = spectrumList.ToArray();
                                PrintSpectrum(spectrum);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect argument! Please try again!");
                    }
                }

            }

            if (howManyToRemove < 3&& !performed)
            {
                Console.WriteLine("Would you like to add any codon to the existing spectrum? y/n");
                answer = Console.ReadLine();
                if (answer.ToLower().Equals("y"))
                {
                    Console.WriteLine("How many would you like to add? 0-" + (3 - howManyToRemove));
                    List<string> spectrumList = spectrum.ToList();
                    int addCount = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Adding:");
                    for (int i = 0; i < addCount; i++)
                    {
                        spectrumList.Add(Console.ReadLine());
                    }

                    spectrum = spectrumList.ToArray();
                    PrintSpectrum(spectrum);
                }
            }

            if (!performed)
            {
                createCompleteGraph(spectrum);
                getPossibleWays();
                printConnections();
            }

           

            Console.ReadLine();
        }

        public static void PrintSpectrum(string[] spectrum)
        {
            Console.Write("The created spectrum for this sequence is: \n [");
            foreach (var s in spectrum)
            {
                Console.Write(" " + s + " ");
            }

            Console.Write("]\n");
        }

        public static List<int> GetRandomNumbers(int count, int spectrumLenth)
        {
            Random random = new Random();
            List<int> randomNumbers = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int number;

                do number = random.Next(0, spectrumLenth - 1);
                while (randomNumbers.Contains(number));

                randomNumbers.Add(number);
            }

            return randomNumbers;
        }
        private static string[] getSpectrum(string sequence)
        {
            string[] spectrum = new string[sequence.Length - 2];
            for (int i = 0; i < sequence.Length - 2; i++)
            {
                int start = i;

                spectrum[i] = sequence.Substring(start, 3);
            }

            return spectrum;

        }

        private static String GenerateRandomSequence(int length)
        {
            char[] letters = "GATC".ToCharArray();
            Random r = new Random();
            string rndString = "";
            for (int i = 0; i < length; i++)
            {
                rndString += letters[r.Next(0, 4)].ToString();
            }

            return rndString;
        }

     

        public static void createCompleteGraph(string[] spectrum)
        {
            for (int i = 0; i < spectrum.Length; i++)
            {
                Node n = new Node(spectrum[i]);
                int next = i + 1;
                if (next >= spectrum.Length)
                {
                    next = spectrum.Length - 1;
                }


                int before = i - 1;
                if (before >= 0)
                {



                    if (graph[graph.Count - 1].getCoverage(n.getValue()) == 0)
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
                    if (n.getCoverage(spectrum[next]) == 0)
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


          //    printGraph();



        }




        public static void printGraph()
        {
            foreach (var node in graph)
            {
                Console.Write(node.getValue() + "    " + node.getId() + "    " + node.getError() + "\n");

                node.printNeighbours();
                Console.Write("\n");
            }

        }

        private static void getPossibleWays()
        {

            for (int i = 0; i < graph.Count; i++)
            {
                PossibleConnection pc = new PossibleConnection(graph[i].getValue());
                pc.addCoveredNodes(graph[i]);
                getWay(pc, graph[i]);
                possibleBest.Add(pc);
            }



        }

        private static void getWay(PossibleConnection pc, Node node)
        {
            foreach (var neighbour in node.getNeighbours())
            {
                if (!pc.getCoveredNodes().Exists(x => x.getId() == neighbour.Key.getId()))
                {
                    pc.addCoveredNodes(neighbour.Key);
                    StringBuilder sb = new StringBuilder();

                    if (neighbour.Value == 0)
                    {
                        /*  if (!errorsList.Contains(neighbour.Key))
                          {
                              errorsList.Add(neighbour.Key);
                          }
                          */
                        pc.addErrorNode(neighbour.Key);
                        /*string currentSuperstring = pc.getSuperString();
                        string superstring = sb.Append(currentSuperstring).Append(" ").Append(neighbour.Key.getValue())
                            .ToString();
                     
                        pc.setSuperString(superstring);*/
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
            possibleBest= possibleBest.OrderByDescending(pc=> pc.getSumCoverage()).ToList();
            int bestCoverage = possibleBest[0].getSumCoverage();
            foreach (var possibleConnection in possibleBest)
            {
                if (possibleConnection.getSumCoverage() == bestCoverage)
                {
                    Console.Write("\n");
                    Console.WriteLine("Longest possible DNA sequence is:");
                    Console.Write(possibleConnection.getSuperString() + "\n");
                    //  Console.WriteLine("Length: "+ possibleConnection.getSuperString().Length);

                    List<Node> nodelist = possibleConnection.getCoveredNodes().Except(possibleConnection.GetErrorList())
                        .ToList();
                    Console.WriteLine("Sequence covers following codons from spectrum:");
                    Console.Write("[ ");
                    foreach (var node in nodelist)
                    {
                        Console.Write(node.getValue() + " ");
                    }

                    Console.Write(" ] ");
                    Console.Write("\nCovered " + nodelist.Count + " out of " + spectrum.Length);
                    //   Console.Write("\n " + );
                    Console.Write("\n");
                    Console.WriteLine(
                        "The following codons were classified as errors and are not possible to attach to this sequence:");
                    Console.Write("[ ");
                    foreach (var node in possibleConnection.GetErrorList())
                    {
                        Console.Write(node.getValue() + " ");
                    }

                    Console.Write(" ] ");
                    Console.Write("\n");
                }
            }
        }
    }
}


