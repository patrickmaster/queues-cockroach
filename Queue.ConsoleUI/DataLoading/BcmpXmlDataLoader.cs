﻿using System;
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
            var nodeType = bcmpInput.SelectSingleNode("/BCMP/Type");
            if (nodeType != null)
            {
                string[] typeValues = nodeType.InnerText.Trim().Split(';');
                input.Type = new BcmpType[typeValues.Length];
                for (int i = 0; i < typeValues.Length; i++)
                {
                    input.Type[i] = (BcmpType) Enum.Parse(typeof (BcmpType), typeValues[i]);
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
                input.K = new int[nodes.Count];
                input.Mi = new double[nodes.Count][];
                input.P = new double[nodes.Count][][];
                for(int i = 0; i < nodes.Count; i++)
                {
                    var nodeLambda = nodes[i].SelectSingleNode("Lambda");
                    if (nodeLambda != null)
                    {
                        input.Lambda[i] = Double.Parse(nodeLambda.InnerText.Trim());
                        if (input.Lambda[i] == 0)
                        {
                            var nodeK = nodes[i].SelectSingleNode("K");
                            if (nodeK != null)
                            {
                                input.K[i] = Int32.Parse(nodeK.InnerText.Trim());
                            }
                            else
                            {
                                throw new UserInputException("Wrong xml file format");
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
