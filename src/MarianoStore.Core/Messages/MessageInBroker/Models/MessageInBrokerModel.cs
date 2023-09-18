using System;

namespace MarianoStore.Core.Messages.MessageInBroker.Models
{
    public class MessageInBrokerModel
    {
        public int MessageId { get; set; }
        public string Name { get; set; }
        public string CurrentContext { get; set; }
        public string Body { get; set; }

        /// <summary>
        /// Data de Criação da Mensagem
        /// </summary>
        public DateTimeOffset Stored { get; set; }

        /// <summary>
        /// Data de Processamento da Mensagem
        /// </summary>
        public DateTimeOffset? Processed { get; set; }
        public TimeSpan? TimeSpent() => Processed?.Subtract(Stored) ?? null;
        public int Num { get; set; }
        public bool IsEvent { get; set; }
        public string OriginalContext { get; set; }
        public int? MessageIdReference { get; set; }
        public DateTimeOffset? MessageInBroker { get; set; }
    }
}
