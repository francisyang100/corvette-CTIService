using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using ECS.Dynamo.CallHistory;

namespace ECS.DataLayer.CTI
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class CallHistoryDynamoManager
    {
        const string TABLE_CALL_HISTORY = "CallHistory-develop-data";
        const string TABLE_CALL_HISTORY_DETAILS = "CallHistoryDetails-develop-data";

        private readonly IAmazonDynamoDB _client;
        private readonly DynamoDBContext _context;

        public CallHistoryDynamoManager()
        {
            _client = new AmazonDynamoDBClient(RegionEndpoint.USWest2);
            _context = new DynamoDBContext(_client, new DynamoDBOperationConfig() { OverrideTableName = TABLE_CALL_HISTORY });
        }

        public void CreateCallHistoryTable()
        {
            if (CheckTableExists(TABLE_CALL_HISTORY))
            {
                Console.WriteLine(TABLE_CALL_HISTORY + " table already exists");
                return;
            }

            try
            {
                List<AttributeDefinition> lstAttribDefinitions = new System.Collections.Generic.List<AttributeDefinition>();
                List<KeySchemaElement> lstSchemaElements = new List<KeySchemaElement>();
                ProvisionedThroughput throughput = new ProvisionedThroughput() { ReadCapacityUnits = 10, WriteCapacityUnits = 5 };

                lstAttribDefinitions.Add(new AttributeDefinition { AttributeName = "CallHistoryId", AttributeType = ScalarAttributeType.N });
                lstAttribDefinitions.Add(new AttributeDefinition { AttributeName = "UCID", AttributeType = ScalarAttributeType.N });

                lstSchemaElements.Add(new KeySchemaElement() { AttributeName = "CallHistoryId", KeyType = "HASH" });
                lstSchemaElements.Add(new KeySchemaElement() { AttributeName = "UCID", KeyType = "RANGE" });

                CreateTableRequest tbRequest = new CreateTableRequest(TABLE_CALL_HISTORY, lstSchemaElements, lstAttribDefinitions, throughput);

                var response = _client.CreateTable(tbRequest);

                WaitUntilTableReady(TABLE_CALL_HISTORY);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private bool CheckTableExists(string tableName)
        {
            bool bExists = false;
            try
            {
                var res = _client.DescribeTable(new DescribeTableRequest { TableName = tableName });
                if (res != null && res.Table != null && res.Table.TableStatus != null && res.Table.TableStatus == "ACTIVE")
                {
                    bExists = true;
                }
            }
            catch (ResourceNotFoundException ex)
            {
                Console.WriteLine("Table not found");
            }

            return bExists;
        }

        private void WaitUntilTableReady(string tableName)
        {
            string status = null;
            do
            {
                try
                {
                    var res = _client.DescribeTable(new DescribeTableRequest { TableName = tableName });
                    Console.WriteLine("Table name: {0}, status: {1}",
                                   res.Table.TableName,
                                   res.Table.TableStatus);
                    status = res.Table.TableStatus;
                }
                catch (ResourceNotFoundException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                if (status != "ACTIVE")
                {
                    System.Threading.Thread.Sleep(2000);
                }

            } while (status != "ACTIVE");
        }

        public void InsertUpdateCallHistoryRecord()
        {
            try
            {
                CallHistoryDynamo callHistory = new CallHistoryDynamo()
                {
                    CallHistoryId = 1,
                    UCID = 12345,
                    Custnum = 10016845933616,
                    DNIS = "1-800-111-2222",
                    ANI = "714-222-3333"
                };

                _context.Save<CallHistoryDynamo>(callHistory, new DynamoDBOperationConfig() { OverrideTableName = TABLE_CALL_HISTORY });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void CreateCallHistoryDetailsTable()
        {
            if (CheckTableExists(TABLE_CALL_HISTORY_DETAILS))
            {
                Console.WriteLine(TABLE_CALL_HISTORY_DETAILS + " table already exists");
                return;
            }

            try
            {
                List<AttributeDefinition> lstAttribDefinitions = new System.Collections.Generic.List<AttributeDefinition>();
                List<KeySchemaElement> lstSchemaElements = new List<KeySchemaElement>();
                ProvisionedThroughput throughput = new ProvisionedThroughput() { ReadCapacityUnits = 10, WriteCapacityUnits = 5 };

                lstAttribDefinitions.Add(new AttributeDefinition { AttributeName = "CallHistoryDetailsId", AttributeType = ScalarAttributeType.N });
                lstAttribDefinitions.Add(new AttributeDefinition { AttributeName = "UCID", AttributeType = ScalarAttributeType.N });

                lstSchemaElements.Add(new KeySchemaElement() { AttributeName = "UCID", KeyType = "HASH" });
                lstSchemaElements.Add(new KeySchemaElement() { AttributeName = "CallHistoryDetailsId", KeyType = "RANGE" });

                CreateTableRequest tbRequest = new CreateTableRequest(TABLE_CALL_HISTORY_DETAILS, lstSchemaElements, lstAttribDefinitions, throughput);

                var response = _client.CreateTable(tbRequest);

                WaitUntilTableReady(TABLE_CALL_HISTORY);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void InsertUpdateCallHistoryDetailsRecord()
        {
            try
            {
                CallHistoryDetailsDynamo callHistoryDetail = new CallHistoryDetailsDynamo()
                {
                    UCID = 12345,
                    CallHistoryDetailsId = 1,
                    EventDescription = "My event Description",
                    Type = "MENU",
                    Action = "InProgress"
                };

                _context.Save<CallHistoryDetailsDynamo>(callHistoryDetail, new DynamoDBOperationConfig() { OverrideTableName = TABLE_CALL_HISTORY_DETAILS });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
