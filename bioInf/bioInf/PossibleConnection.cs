using System.Collections.Generic;

namespace bioInf
{
    public class PossibleConnection
    {
        private string superString;
        private List<Node> mergedList;
        private List<Node> errorList;
        private int cover;
        public PossibleConnection(string superstring)
        {
            this.superString = superstring;
            mergedList = new List<Node>();
            errorList= new List<Node>();
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
        public List<Node> GetErrorList()
        {
            return errorList;
        }

        public void addErrorNode(Node node)
        {
            errorList.Add(node);
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