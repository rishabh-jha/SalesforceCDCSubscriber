using System;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;
using CometD.NetCore.Client;
using CometD.NetCore.Client.Transport;

namespace SalesforceCDCSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var authResponse = Authorization.AsyncAuthRequest();
            
            if (authResponse.Result != null)
            { 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                try
                {
                    int readTimeOut = 120000;
                    string streamingEndpointURI = "/cometd/45.0";
                    var options = new Dictionary<String, Object>
                    {
                        { ClientTransport.TIMEOUT_OPTION, readTimeOut }
                    };
                    NameValueCollection collection = new NameValueCollection();
                    collection.Add(HttpRequestHeader.Authorization.ToString(), "OAuth " + authResponse.Result.access_token);
                    var transport = new LongPollingTransport(options, new NameValueCollection { collection });
                    var serverUri = new Uri(authResponse.Result.instance_url);
                    String endpoint = String.Format("{0}://{1}{2}", serverUri.Scheme, serverUri.Host, streamingEndpointURI);
                    var bayeuxClient = new BayeuxClient(endpoint, new[] { transport });
                    var pushTopicConnection = new EventBusConnector(bayeuxClient);
                    pushTopicConnection.Connect();
                    //Close the connection
                    //Console.WriteLine("Press any key to shut down.\n");
                    Console.ReadKey();
                    //pushTopicConnection.Disconect();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
