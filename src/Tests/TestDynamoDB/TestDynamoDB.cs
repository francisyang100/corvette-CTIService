using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ECS.DataLayer.CTI;

namespace TestDynamoDB
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TestDynamoDB
    {
        public TestDynamoDB()
        {
        }

        [Fact]
        public void TestMethod1()
        {
            CallHistoryDynamoManager mgr = new CallHistoryDynamoManager();

            mgr.CreateCallHistoryTable();

            mgr.InsertUpdateCallHistoryRecord();

            Assert.Equal(4, 2 + 2);
        }

        [Fact]
        public void TestMethod2()
        {
            CallHistoryDynamoManager mgr = new CallHistoryDynamoManager();

            mgr.CreateCallHistoryDetailsTable();

            mgr.InsertUpdateCallHistoryDetailsRecord();

            Assert.Equal(4, 2 + 2);
        }
    }
}
