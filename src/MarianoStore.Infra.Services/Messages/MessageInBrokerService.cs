using Dapper;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Data.SqlClient;

namespace MarianoStore.Infra.Services.Messages
{
    public class MessageInBrokerService : IMessageInBrokerService
    {
        public MessageInBrokerModel CreateMessage(
            string name,
            string currentContext,
            string body,
            bool isEvent,
            string originalContext,
            int? messageIdReference,
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
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
                        ,[MessageInBroker]
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
            int messageIdReference,
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
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
                        ,[MessageInBroker]
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

        public void MarkAsMessageInBroker(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            string sql =
                @"
                    UPDATE [MessageInBroker]
                        SET
                            [MessageInBroker] = GETUTCDATE()
                        WHERE
                            [MessageId] = @MessageId;
                ";

            sqlConnection.Execute(sql: sql, param: message, transaction: sqlTransaction);
        }
    }
}
