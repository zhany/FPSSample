using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using OpenMatch;

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

        public CreateTicketResponse CreateTicket(Ticket ticket)
        {
            Log("Calling CreateTicket with player {0}", ticket.Id);
            var request = new CreateTicketRequest
            {
                Ticket = ticket
            };
            return client.CreateTicket(request);
        }

        public void Dispose()
        {
            channel.ShutdownAsync().Wait();
        }

        public async Task GetUpdates(Ticket ticket)
        {
            try
            {
                Log("*** GetAssignments: ticket {0}", ticket.Id);
                var request = new GetAssignmentsRequest
                {
                    TicketId = ticket.Id
                };

                using (var call = client.GetAssignments(request))
                {
                    var responseStream = call.ResponseStream;
                    StringBuilder responseLog = new StringBuilder("Result: ");
                    Log("waiting");
                    while (await responseStream.MoveNext())
                    {
                        var assignment = responseStream.Current;
                        Log(assignment.Assignment);
                        this.assignment = new Assignment
                        {
                            ConnectionString = assignment.Assignment.Connection
                        };
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
