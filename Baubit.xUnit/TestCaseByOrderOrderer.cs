using Xunit.Abstractions;
using Xunit.Sdk;

namespace Baubit.xUnit
{
    public sealed class TestCaseByOrderOrderer : ITestCaseOrderer
    {
        public const string Name = "Baubit.xUnit.TestCaseByOrderOrderer";
        public const string Assembly = "Baubit.xUnit";
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase => string.IsNullOrEmpty(testCase.TestMethod.Method.GetOrder()))
                            .ThenBy(testCase => testCase.TestMethod.Method.GetOrder());
        }
    }
}
