using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.Data
{
    class XmlDataLoader : IFileDataLoader
    {
        public Input LoadInputForJackson(string filename)
        {
            XmlDocument JacksonInput = new XmlDocument();
            JacksonInput.Load(filename);
            Input input = new Input();
            var nodeLambda = JacksonInput.SelectSingleNode("/Jackson/Lambda");
            if (nodeLambda != null)
            {
                input.Lambda = Double.Parse(nodeLambda.InnerText.Trim());             
            }
            else
            {
                throw new Exception("Wrong xml file format");
            }
            var nodeMi = JacksonInput.SelectSingleNode("/Jackson/Mi");
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
            var nodeP = JacksonInput.SelectSingleNode("/Jackson/P");
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

        public BcmpInput LoadInputForBcmp(string filename)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IFileDataLoader
    {
        Input LoadInputForJackson(string filename);

        BcmpInput LoadInputForBcmp(string filename);
    }
}
