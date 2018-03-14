using System;
using System.Text;
using System.Collections.Generic;
using Rpc.Synapse.Icarus;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace test
{
    public class EventServer : BaseCallback
    {
        public override Dictionary<string, string> RegAlias()
        {
            return new Dictionary<string, string>(){
                {"dotnet.test","tb"},
                {"ruby.test","tb"},
                {"golang.test","tb"},
                {"java.test","tb"},
                {"python.test","tb"},
                {"php.test","tb"},
            };
        }
        public bool tb(JObject data, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("**收到EVENT: {0}@{1} \n{2}", ea.BasicProperties.Type, ea.BasicProperties.ReplyTo, data.ToString());
            return true;
        }
    }
}
