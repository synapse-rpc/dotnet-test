using System;
using RabbitMQ.Client.Events;
using System.Text;
using Rpc.Synapse.Icarus;
using Newtonsoft.Json.Linq;

namespace test
{
    public class BLogger : BaseLogger
    {
        public override void All(JObject data, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("所有LOG记录: {0} \n{1}", ea.RoutingKey, data.ToString());
        }

        public override void Event(JObject data, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("事件LOG记录: {0} \n{1}", ea.RoutingKey, data.ToString());
        }
        public override void Request(JObject data, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("请求LOG记录: {0} \n{1}", ea.RoutingKey, data.ToString());
        }
        public override void Response(JObject data, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("响应LOG记录: {0} \n{1}", ea.RoutingKey, data.ToString());
        }
    }
}
