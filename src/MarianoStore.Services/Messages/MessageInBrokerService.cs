using Dapper;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Data.SqlClient;

namespace MarianoStore.Services.Messages
{
    public class MessageInBrokerService : IMessageInBrokerService
    {
        public MessageInBrokerModel CreateMessage(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string name,
            string currentContext,
            string body,
            bool isEvent,
            string originalContext,
            int? messageIdReference,
            bool processed = false)
        {
            string sql =
                $@"
                    INSERT INTO [MessageInBroker] 
                        ([Name], [CurrentContext], [Body], [Stored], [Num], [IsEvent], [OriginalContext], [MessageIdReference])
                    VALUES 
                        (@Name, @CurrentContext, @Body, GETUTCDATE(), 0, @IsEvent, @OriginalContext, @MessageIdReference);

                    SELECT
                        [MessageId]
                        ,[Name]
                        ,[CurrentContext]
                        ,[Body]
                        ,[Stored]
                        ,[Processed]
                        ,[Num]
                        ,[IsEvent]
                        ,[OriginalContext]
                        ,[MessageIdReference]
                    FROM
                        [MessageInBroker]
                    WHERE
                        [MessageId] = SCOPE_IDENTITY();
                ";

            return sqlConnection.QuerySingle<MessageInBrokerModel>(
                sql: sql,
                param: new
                {
                    Name = name,
                    CurrentContext = currentContext,
                    Body = body,
                    IsEvent = isEvent,
                    OriginalContext = originalContext,
                    MessageIdReference = messageIdReference
                },
                transaction: sqlTransaction);
        }

        public MessageInBrokerModel GetMessageByMessageIdReference(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int messageIdReference)
        {
            string sql =
                @"
                    SELECT
                        [MessageId]
                        ,[Name]
                        ,[CurrentContext]
                        ,[Body]
                        ,[Stored]
                        ,[Processed]
                        ,[Num]
                        ,[IsEvent]
                        ,[OriginalContext]
                        ,[MessageIdReference]
                    FROM
                        [MessageInBroker]
                    WHERE
                        [MessageIdReference] = @MessageIdReference;
                ";

            return sqlConnection.QueryFirstOrDefault<MessageInBrokerModel>(
                sql: sql,
                param: new
                {
                    MessageIdReference = messageIdReference
                },
                transaction: sqlTransaction);
        }

        public void MarkAsProcessed(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            string sql =
                @"
                    UPDATE [MessageInBroker]
                        SET
                            [Processed] = GETUTCDATE()
                            ,[Num] = [Num] + 1
                        WHERE
                            [MessageId] = @MessageId;
                ";

            sqlConnection.Execute(sql: sql, param: message, transaction: sqlTransaction);
        }

        public void IncrementNum(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            string sql =
                @"
                    UPDATE [MessageInBroker]
                        SET
                            [Num] = [Num] + 1
                        WHERE
                            [MessageId] = @MessageId;
                ";

            sqlConnection.Execute(sql: sql, param: message, transaction: sqlTransaction);
        }
    }
}
