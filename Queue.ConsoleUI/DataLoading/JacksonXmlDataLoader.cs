using System;
using System.Xml;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    class JacksonXmlDataLoader : IJacksonFileDataLoader
    {
        public Input LoadInputForJackson(string filename)
        {
            XmlDocument jacksonInput = new XmlDocument();
            jacksonInput.Load(filename);
            Input input = new Input();
            var nodeLambda = jacksonInput.SelectSingleNode("/Jackson/Lambda");
            if (nodeLambda != null)
            {
                input.Lambda = Double.Parse(nodeLambda.InnerText.Trim());             
            }
            else
            {
                throw new Exception("Wrong xml file format");
            }
            var nodeMi = jacksonInput.SelectSingleNode("/Jackson/Mi");
            if (nodeMi != null)
            {
                string[] miValues = nodeMi.InnerText.Trim().Split(';');
                input.Mi = new double[miValues.Length];
                for (int i = 0; i < miValues.Length; i++)
                {
                    input.Mi[i] = Double.Parse(miValues[i]);
                }
            }
            else
            {
                throw new Exception("Wrong xml file format");
            }
            var nodeM = jacksonInput.SelectSingleNode("/Jackson/M");
            if (nodeM != null)
            {
                string[] mValues = nodeM.InnerText.Trim().Split(';');
                input.M = new double[mValues.Length];
                for (int i = 0; i < mValues.Length; i++)
                {
                    input.M[i] = Double.Parse(mValues[i]);
                }
            }
            else
            {
                throw new Exception("Wrong xml file format");
            }
            var nodeP = jacksonInput.SelectSingleNode("/Jackson/P");
            if (nodeP != null)
            {
                string[] pRows = nodeP.InnerText.Trim().Split('\n');
                input.P = new double[pRows.Length][];
                for (int i = 0; i < pRows.Length; i++)
                {
                    var row = pRows[i];
                    string[] pValues = row.Trim().Split(';');
                    input.P[i] = new double[pValues.Length];
                    for (int j = 0; j < pValues.Length; j++)
                    {
                        input.P[i][j] = Double.Parse(pValues[j]);
                    }
                }
            }
            else
            {
                throw new Exception("Wrong xml file format");
            }
            return input;
        }
    }
}
