using System;
using Rpc.Synapse.Icarus;
using Newtonsoft.Json.Linq;
using Shuttle.Core.Cli;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new Arguments(args);
            if (!cmd.Contains("host") || !cmd.Contains("user") || !cmd.Contains("pass") || !cmd.Contains("sys_name"))
            {
                Console.WriteLine("Usage: dotnet test.dll --host MQ_HOST --user MQ_USER --pass MQ_PASS --sys_name SYSTEM_NAME [--debug] [--log]");
                return;
            }
            var app = new Synapse();
            app.MqHost = cmd.Get<string>("host");
            app.MqUser = cmd.Get<string>("user");
            app.MqPass = cmd.Get<string>("pass");
            app.SysName = cmd.Get<string>("sys_name");
            app.AppName = "dotnet";
            if (cmd.Contains("debug"))
            {
                app.Debug = true;
            }
            app.EventCallback = new EventServer();
            app.RpcCallback = new RpcServer();
            if (cmd.Contains("log"))
            {
                app.LoggerCallback = new BLogger();
            }
            app.Serve();
            string input;
            JObject ht;
            string[] inputs;
            showHelp();
            while (true)
            {
                Console.Write("Input >> ");
                input = Console.ReadLine();
                inputs = input.Split(" ");
                switch (inputs[0])
                {
                    case "event":
                        if (inputs.Length != 3)
                        {
                            showHelp();
                            continue;
                        }
                        ht = new JObject();
                        ht.Add("msg", inputs[2]);
                        app.SendEvent(inputs[1], ht);
                        break;
                    case "rpc":
                        if (inputs.Length != 4)
                        {
                            showHelp();
                            continue;
                        }
                        ht = new JObject();
                        ht.Add("msg", inputs[3]);
                        JObject res = app.SendRpc(inputs[1], inputs[2], ht);
                        Console.WriteLine("{0}", res.ToString());
                        break;
                    default:
                        showHelp();
                        break;
                }
            }
        }

        static void showHelp()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("|   event usage:                             |");
            Console.WriteLine("|     > event [event] [msg]                  |");
            Console.WriteLine("|   rpc usage:                               |");
            Console.WriteLine("|     > rpc [app] [method] [msg]             |");
            Console.WriteLine("----------------------------------------------");
        }
    }
}
