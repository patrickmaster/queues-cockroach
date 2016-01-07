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
                if (input.Lambda == 0)
                {
                    var nodeK = jacksonInput.SelectSingleNode("/Jackson/K");
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
            else
            {
                throw new UserInputException("Wrong xml file format");
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
                throw new UserInputException("Wrong xml file format");
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
                throw new UserInputException("Wrong xml file format");
            }
            return input;
        }
    }
}
