using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace ECS.Dynamo.CallHistory
{
    [DynamoDBTable("CallHistory")]
    public class CallHistoryDynamo
    {
        public CallHistoryDynamo()
        {
        }

        [DynamoDBHashKey]
        public int CallHistoryId { get; set; }
        [DynamoDBRangeKey]
        public int UCID { get; set; }
        public Int64 Custnum { get; set; }
        public string DNIS { get; set; }
        public String ANI { get; set; }
    }
}
