﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.DataLayer.CTI;
using Xunit;

namespace Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Class1
    {
        public Class1()
        {
        }

        public void TestCreateCallHistoryTable()
        {
            CallHistoryDynamoManager mgr = new CallHistoryDynamoManager();
            mgr.CreateTable();

            int temp = 1;
        }
    }
}
