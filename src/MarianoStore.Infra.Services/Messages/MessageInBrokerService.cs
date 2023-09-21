using Dapper;
using MarianoStore.Core.Messages;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarianoStore.Infra.Services.Messages
{
    public class MessageInBrokerService : IMessageInBrokerService
    {
        private readonly string _fields =
            @"
                [MessageId]
                ,[FullName]
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
            ";

        public MessageInBrokerModel CreateMessage(
            string fullName,
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
                        ([FullName], [Name], [CurrentContext], [Body], [Stored], [Num], [IsEvent], [OriginalContext], [MessageIdReference])
                    VALUES 
                        (@FullName, @Name, @CurrentContext, @Body, GETUTCDATE(), 0, @IsEvent, @OriginalContext, @MessageIdReference);

                    SELECT
                        {_fields}
                    FROM
                        [MessageInBroker]
                    WHERE
                        [MessageId] = SCOPE_IDENTITY();
                ";

            return sqlConnection.QuerySingle<MessageInBrokerModel>(
                sql: sql,
                param: new
                {
                    FullName = fullName,
                    Name = name,
                    CurrentContext = currentContext,
                    Body = body,
                    IsEvent = isEvent,
                    OriginalContext = originalContext,
                    MessageIdReference = messageIdReference
                },
                transaction: sqlTransaction);
        }

        public MessageInBrokerModel GetMessageByMessageId(
            int messageId,
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            string sql =
                $@"
                    SELECT
                        {_fields}
                    FROM
                        [MessageInBroker]
                    WHERE
                        [MessageId] = @MessageId;
                ";

            return sqlConnection.QueryFirstOrDefault<MessageInBrokerModel>(
                sql: sql,
                param: new
                {
                    MessageId = messageId
                },
                transaction: sqlTransaction);
        }

        public MessageInBrokerModel GetMessageByMessageIdReference(
            int messageIdReference,
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            string sql =
                $@"
                    SELECT
                        {_fields}
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

        public IEnumerable<MessageInBrokerModel> GetMessagesToPublish(
            bool isEvent,
            int secondsDelay,
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            string sql =
                $@"
                    SELECT
                        {_fields}
                    FROM
                        [MessageInBroker]
                    WHERE
                        [MessageInBroker] IS NULL
                        AND [Processed] IS NULL
                        AND [Stored] < DATEADD(second, -{secondsDelay}, GETUTCDATE())
                        AND [IsEvent] = '{(isEvent ? 1 : 0)}'
                ";

            return sqlConnection.Query<MessageInBrokerModel>(
                sql: sql,
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
