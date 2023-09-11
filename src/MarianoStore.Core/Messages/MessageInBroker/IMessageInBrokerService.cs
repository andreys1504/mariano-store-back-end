using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Data.SqlClient;

namespace MarianoStore.Core.Messages.MessageInBroker
{
    public interface IMessageInBrokerService
    {
        MessageInBrokerModel CreateMessage(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string name, string currentContext, string body, bool isEvent, string originalContext, int? messageIdReference, bool processed = false);
        MessageInBrokerModel GetMessageByMessageIdReference(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int messageIdReference);
        void IncrementNum(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        void MarkAsProcessed(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
    }
}