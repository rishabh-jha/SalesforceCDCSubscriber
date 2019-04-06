using System;
using Newtonsoft.Json;
using CometD.NetCore.Bayeux;
using CometD.NetCore.Bayeux.Client;

namespace SalesforceCDCSubscriber
{
    class EventListener : IMessageListener
    {
        public void OnMessage(IClientSessionChannel channel, IMessage message)
        {
            var convertedJson = message.Json;
            var obj = JsonConvert.DeserializeObject<CDCResponse>(convertedJson);
            Console.WriteLine(convertedJson);
            Console.WriteLine("Schema: " + obj.data.schema + " , Event:" + obj.data._event);
            Console.WriteLine("Payload: " + JsonConvert.SerializeObject(obj.data.payload));
        }
    }
}
