using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Rpc.Synapse.Icarus;
using RabbitMQ.Client.Events;
using Newtonsoft.Json.Linq;
namespace test
{
    public class RpcServer : BaseCallback
    {
        public override Dictionary<string, string> RegAlias()
        {
            return new Dictionary<string, string>()
            {
                {"test","tb"}
            };
        }
        public JObject tb(JObject data, BasicDeliverEventArgs ea)
        {
            var ret = new JObject();
            ret.Add("suceess", "I 收到了");
            ret.Add("m", data.GetValue("msg"));
            ret.Add("number", 5233);
            return ret;
        }

    }
}
