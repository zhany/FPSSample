using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Api;
using Messages;
using XidNet;
using Newtonsoft.Json;


public static class PlayerUtil
{
    public static Player GeneratePlayer()
    {
        Player player = new Player
        {
            Id = Xid.NewXid().ToString()
        };
        var properties = new Dictionary<string, Dictionary<string, int>>();
        var mmr = new Dictionary<string, int>
        {
            { "rating", 1000 }
        };
        properties.Add("mmr", mmr);
        player.Properties = JsonConvert.SerializeObject(properties);
        return player;
    }
}