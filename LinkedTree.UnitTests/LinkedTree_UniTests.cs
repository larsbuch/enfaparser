using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LinkedTree.UnitTests
{
    public class LinkedTree_UniTests
    {
        [Theory, LinkedTreeTestConvensions]
        public void BuildLinkedTree()
        {
            Exception expected = null;
            Exception actual = null;
            LinkedTreeNode linkedTreeNode;
            try
            {
                linkedTreeNode = new LinkedTreeNode("Program", new SourceData("UnitTest", 0, 0))
                    .AddChild("Statement", new List<char>() { '{' }, new SourceData("UnitTest", 0, 1))
                    .AddNext("Number", new List<char>() { '9' }, new SourceData("UnitTest", 0, 2))
                    .AddChild("Statement End", new List<char>() { '}' }, new SourceData("UnitTest", 0, 3));
            }
            catch (Exception ex)
            {
                actual = ex;
            }
            Assert.Equal(expected, actual);
        }
    }
}
