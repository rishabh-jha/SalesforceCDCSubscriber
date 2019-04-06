using System;
using CometD.NetCore.Client;

namespace SalesforceCDCSubscriber
{
    class EventBusConnector
    {
        BayeuxClient _bayeuxClient = null;
        string channel = "/data/ChangeEvents";
        //string channel = "/topic/LeadUpdates";
        public EventBusConnector(BayeuxClient bayeuxClient)
        {
            _bayeuxClient = bayeuxClient;
        }
        public void Connect()
        {
            _bayeuxClient.Handshake();
            _bayeuxClient.WaitFor(1000, new[] { BayeuxClient.State.CONNECTED });
            _bayeuxClient.GetChannel(channel, -2).Subscribe(new EventListener());
            Console.WriteLine("Waiting event from salesforce for the changed data " + channel.ToString());
        }
        public void Disconect()
        {
            _bayeuxClient.Disconnect();
            _bayeuxClient.WaitFor(1000, new[] { BayeuxClient.State.DISCONNECTED });
        }
    }
}
