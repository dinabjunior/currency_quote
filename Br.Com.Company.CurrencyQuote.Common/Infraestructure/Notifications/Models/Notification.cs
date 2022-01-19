using System;
using MediatR;
using Newtonsoft.Json;

namespace Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications
{
    public class Notification : INotification
    {
        [JsonProperty(Order = -2)]
        public string Key { get; }

        [JsonProperty(Order = -1)]
        public string Message { get; }

        public Notification(string key, string message)
        {
            Key = key != null ? char.ToLowerInvariant(key[0]) + key[1..] : null;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
