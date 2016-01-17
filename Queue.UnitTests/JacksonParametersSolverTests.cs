using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue.Algorithm;

namespace Queue.UnitTests
{
    [TestClass]
    public class JacksonParametersSolverTests
    {
        private JacksonParametersSolver _solver;
        private const double Precision = 0.1;

        [TestInitialize]
        public void Initialize()
        {
            _solver = new JacksonParametersSolver();
        }

        [TestMethod]
        public void GetParametersTest()
        {
            var lambda = new[] {0.1115, 0.1154, 0.0612, 0.542, 0.1626};
            var m = new[] {1, 1, 1, 2, 1};
            var mi = new[] {0.5, 0.25, 0.3333, 0.5, 0.5};

            var result = _solver.SolveParameters(m, mi, lambda).ToArray();

            result.Should().HaveCount(5);
            result[0].ServiceTime.Should().BeApproximately(2.8, Precision);
            result[1].ServiceTime.Should().BeApproximately(10.4, Precision);
            result[2].ServiceTime.Should().BeApproximately(4, Precision);
            result[3].ServiceTime.Should().BeApproximately(4.2, Precision);
            result[4].ServiceTime.Should().BeApproximately(3.5, Precision);
        }
    }
}
