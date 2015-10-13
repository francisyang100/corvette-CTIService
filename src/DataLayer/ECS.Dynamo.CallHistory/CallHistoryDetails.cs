using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace ECS.Dynamo.CallHistory
{
    [DynamoDBTable("CallHistoryDetails")]
    public class CallHistoryDetailsDynamo
    {
        [DynamoDBHashKey]
        public int UCID { get; set; }
        [DynamoDBRangeKey]
        public int CallHistoryDetailsId { get; set; }
        public string EventDescription { get; set; }
        public string Type { get; set; }    // MENU, APICall, Log
        public string Action { get; set; }  // InProgress, Transfer, Hangup
    }
}
