using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    class BcmpXmlDataLoader : IBcmpFileDataLoader
    {
        public BcmpInput LoadInputForBcmp(string filename)
        {
            BcmpInput input = new BcmpInput();
            XmlDocument bcmpInput = new XmlDocument();
            bcmpInput.Load(filename);
            bool closedSystem = false;
            var nodeType = bcmpInput.SelectSingleNode("/BCMP/Type");
            if (nodeType != null)
            {
                string[] typeValues = nodeType.InnerText.Trim().Split(';');
                input.Type = new int[typeValues.Length];
                for (int i = 0; i < typeValues.Length; i++)
                {
                    input.Type[i] = Int32.Parse(typeValues[i]);
                }
            }
            else
            {
                throw new UserInputException("Wrong xml file format");
            }
            XmlNodeList nodes = bcmpInput.SelectNodes("/BCMP/Class");
            if (nodes != null)
            {
                input.Lambda = new double[nodes.Count];
                input.Mi = new double[nodes.Count][];
                input.P = new double[nodes.Count][][];
                for(int i = 0; i < nodes.Count; i++)
                {
                    var nodeLambda = nodes[i].SelectSingleNode("Lambda");
                    if (nodeLambda != null)
                    {
                        if (closedSystem == true)
                        {
                            input.Lambda[i] = 0;
                        }
                        else
                        {
                            input.Lambda[i] = Double.Parse(nodeLambda.InnerText.Trim());
                            if (i == 0)
                            {
                                if (input.Lambda[0] == 0)
                                {
                                    closedSystem = true;
                                    var nodeK = bcmpInput.SelectSingleNode("/BCMP/K");
                                    if (nodeK != null)
                                    {
                                        input.K = Int32.Parse(nodeK.InnerText.Trim());
                                    }
                                    else
                                    {
                                        throw new UserInputException("Wrong xml file format");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new UserInputException("Wrong xml file format");
                    }
                    var nodeMi = nodes[i].SelectSingleNode("Mi");
                    if (nodeMi != null)
                    {
                        string[] miValues = nodeMi.InnerText.Trim().Split(';');
                        input.Mi[i] = new double[miValues.Length];
                        for (int j = 0; j < miValues.Length; j++)
                        {
                            input.Mi[i][j] = Double.Parse(miValues[j]);
                        }
                    }
                    else
                    {
                        throw new UserInputException("Wrong xml file format");
                    }
                    var nodeP = nodes[i].SelectSingleNode("P");
                    if (nodeP != null)
                    {
                        string[] pRows = nodeP.InnerText.Trim().Split('\n');
                        input.P[i] = new double[pRows.Length][];
                        for (int j = 0; j < pRows.Length; j++)
                        {
                            var row = pRows[j];
                            string[] pValues = row.Trim().Split(';');
                            input.P[i][j] = new double[pValues.Length];
                            for (int k = 0; k < pValues.Length; k++)
                            {
                                input.P[i][j][k] = Double.Parse(pValues[k]);
                            }
                        }
                    }
                    else
                    {
                        throw new UserInputException("Wrong xml file format");
                    }
                }
            }

            return input;
        }
    }
}
