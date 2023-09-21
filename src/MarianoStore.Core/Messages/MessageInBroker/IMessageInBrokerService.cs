using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarianoStore.Core.Messages.MessageInBroker
{
    public interface IMessageInBrokerService
    {
        MessageInBrokerModel CreateMessage(string fullName, string name, string currentContext, string body, bool isEvent, string originalContext, int? messageIdReference, SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        MessageInBrokerModel GetMessageByMessageId(int messageId, SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        MessageInBrokerModel GetMessageByMessageIdReference(int messageIdReference, SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        IEnumerable<MessageInBrokerModel> GetMessagesToPublish(bool isEvent, int secondsDelay, SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        void IncrementNum(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        
        void MarkAsProcessed(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        void MarkAsMessageInBroker(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
    }
}