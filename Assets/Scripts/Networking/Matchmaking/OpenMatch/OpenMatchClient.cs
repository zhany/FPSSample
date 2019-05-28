using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Api;
using Messages;

namespace UnityEngine.Ucg.Matchmaking.OpenMatch
{
    public class OpenMatchClient: IDisposable
    {
        private Channel channel;
        readonly Frontend.FrontendClient client;
        Assignment assignment;

        public Assignment Assignment => assignment;

        public OpenMatchClient(string endpoint)
        {
            this.channel = new Channel(endpoint, ChannelCredentials.Insecure);

            this.client = new Frontend.FrontendClient(channel);
        }

        public void CreatePlayer(Player player)
        {
            Log("Calling CreatePlayer with player {0}", player.Id);
            var request = new CreatePlayerRequest
            {
                Player = player
            };
            var response = client.CreatePlayer(request);
        }

        public void Dispose()
        {
            channel.ShutdownAsync().Wait();
        }

        public async Task GetUpdates(Player player)
        {
            try
            {
                Log("*** GetUpdates: player {0}", player.Id);
                var request = new GetUpdatesRequest
                {
                    Player = player
                };

                using (var call = client.GetUpdates(request))
                {
                    var responseStream = call.ResponseStream;
                    Log("waiting");
                    while (await responseStream.MoveNext())
                    {
                        Log("awaiting");
                        var update = responseStream.Current;
                        Log(update.Player);
                        this.assignment = new Assignment();
                        this.assignment.ConnectionString = update.Player.Assignment;
                    }
                }
            }
            catch (RpcException e)
            {
                Log("RPC failed " + e);
                throw;
            }
        }

        private void Log(string s, params object[] args)
        {
            Debug.Log(string.Format(s, args));
        }

        private void Log(object s)
        {
            Debug.Log(s);
        }
    }
}
