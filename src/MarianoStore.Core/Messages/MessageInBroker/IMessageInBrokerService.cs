using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Data.SqlClient;

namespace MarianoStore.Core.Messages.MessageInBroker
{
    public interface IMessageInBrokerService
    {
        MessageInBrokerModel CreateMessage(string name, string currentContext, string body, bool isEvent, string originalContext, int? messageIdReference, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        
        MessageInBrokerModel GetMessageByMessageIdReference(int messageIdReference, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        
        void IncrementNum(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        
        void MarkAsProcessed(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        void MarkAsMessageInBroker(MessageInBrokerModel message, SqlConnection sqlConnection, SqlTransaction sqlTransaction);
    }
}