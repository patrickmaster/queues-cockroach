using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue.Algorithm;
using Queue.Algorithm.Cockroach;
using Queue.Algorithm.Data;

namespace Queue.UnitTests
{
    [TestClass]
    public class BcmpParametersSolverTests
    {
        private BcmpParametersSolverBetter _solver;

        [TestInitialize]
        public void Initialize()
        {
            _solver = new BcmpParametersSolverBetter();
        }

        [TestMethod]
        public void GetParametersTest()
        {
            var m = new[] { 1, 1, 1, 1, 1 };
            var mi = new[]
            {
                new[] {0.5, 0.5, 0.25, 0.5, 0.5},
                new[] {0.5, 0.5, 0.25, 0.5, 0.2}
            };
            var lambda = new[]
            {
                new[] {0.25833, 0.041667, 0.041667, 0.041667, 0.06875},
                new[] {0.025, 0.00833, 0.00833, 0, 0.04999}
            };
            var type = new[] {BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.Three, BcmpType.One};

            var result = _solver.GetParameters(m, mi, lambda, type);
        }
    }
}
