using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Grpc.Core;
using Api;
using Messages;
using System.Collections.Generic;

public class Example : MonoBehaviour
{
    public Text Response;

    public class OpenMatchClient
    {
        readonly Frontend.FrontendClient client;

        public OpenMatchClient(Frontend.FrontendClient client)
        {
            this.client = client;
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

        public async Task GetUpdates(Player player, Text response)
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
                        response.text = update.Player.ToString();

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
    public async void Send()
    {
        try
        {
            var channel = new Channel("127.0.0.1:50504", ChannelCredentials.Insecure);

            var client = new OpenMatchClient(new Frontend.FrontendClient(channel));
            Player player = PlayerUtil.GeneratePlayer();
            client.CreatePlayer(player);
            await client.GetUpdates(player, Response);
            channel.ShutdownAsync().Wait();
        }
        catch (Exception e)
        {
            Response.text = "exception: " + e.Message;
        }
    }
}
