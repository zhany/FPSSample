using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenMatch;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;

namespace OpenMatch
{
    public class TicketUtil
    {
        public static Ticket GenerateTicket(string name)
        {
            Ticket ticket = new Ticket
            {
                Properties = new Struct()
            };
            ticket.Properties.Fields.Add("name", Value.ForString(name));
            ticket.Properties.Fields.Add("mode.demo", Value.ForNumber(1));
            return ticket;
        }
    }
}