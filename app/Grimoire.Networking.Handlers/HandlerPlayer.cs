using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Grimoire.Networking.Handlers
{
    public class HandlerSkills : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "sAct"
        };

        public async void Handle(JsonMessage message)
        {
            await Task.Delay(10);
            Flash.Call("getRange", new string[0]);
            OptionsManager.SetInfiniteRange(OptionsManager.InfiniteRange);
        }
    }

    public class HandlerBotSkills : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        public async void Handle(JsonMessage message)
        {
            if (message.RawContent.IndexOf("\"msg\":", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.IndexOf("counter attack", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.Contains($"\"strFrame\":\"{Player.Cell}\""))
            {
                Proxy.DisableBotSkill();
            }
            else if (message.RawContent.IndexOf("\"nam\":\"Counter Attack\"", StringComparison.OrdinalIgnoreCase) != -1)
            {
                JArray list = (JArray)message.DataObject?["a"];
                if (list != null)
                {
                    if (list.Any(a => ((a["cmd"].ToString() == "aura-") || (a["cmd"].ToString() == "aura--")) && a["aura"]["nam"].ToString().Equals("Counter Attack", StringComparison.OrdinalIgnoreCase)))
                    {
                        Proxy.EnableBotSkill();
                    }
                }
            }
        }
    }

        public class HandlerInfo : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[4]
        {
            "ct",
            "cb",
            "moveToArea",
            "uotls"
        };

        public async void Handle(JsonMessage message)
        {
            await ParseInfo(message, message.Command);
        }

        public async Task ParseInfo(JsonMessage message, string command)
        {
            switch (command)
            {
                case "ct":
                case "cb":
                    //Dictionary<string, PlayerInfo> dictionary = message.DataObject?["p"]?.ToObject<Dictionary<string, PlayerInfo>>();
                    //if (dictionary != null && dictionary.Count > 0)
                    //{
                    //    World.Avatars.(dictionary.Select((KeyValuePair<string, PlayerInfo> q) => q.Value).ToList());
                    //}
                    //JToken player = message.DataObject["p"][Player.Username.ToLower()];
                    //if ((player == null) || (player.Values<JToken>().Count() < 1))
                    //    return;
                    //foreach (JToken data in player)
                    //{
                    //    if (data.ToString().Contains("intHP"))
                    //        Player.Health = int.Parse(data.ToString().Split(':')[1]);
                    //    else if (data.ToString().Contains("intMP"))
                    //        Player.Mana = int.Parse(data.ToString().Split(':')[1]);
                    //}
                    break;
                case "moveToArea":
                    //JToken uoBranch = message.DataObject?["uoBranch"];
                    //if (uoBranch == null)
                    //    return;
                    //foreach (JObject user in uoBranch)
                    //{
                    //    if (user["strUsername"].Value<string>() == Player.Username)
                    //    {
                    //        if (Proxy.Instance.firstJoin)
                    //        {
                    //            Proxy.Instance.firstJoin = false;
                    //            return;
                    //        }
                    //        Player.Health = (user["intHP"].Value<int>() != null) ? user["intHP"].Value<int>() : Player.Health;
                    //        Player.HealthMax = (user["intHPMax"].Value<int>() != null) ? user["intHPMax"].Value<int>() : Player.HealthMax;
                    //        Player.Mana = (user["intMP"].Value<int>() != null) ? user["intMP"].Value<int>() : Player.Mana;
                    //        break;
                    //    }
                    //}
                    break;
                case "uotls":
                    //JToken datas = message.DataObject?["o"];
                    //string unm = message.DataObject?["unm"].Value<string>();
                    //if ((datas == null) || (unm != Player.Username.ToLower()))
                    //    return;
                    //foreach (JToken data in datas)
                    //{
                    //    if (data.ToString().Contains("intHP"))
                    //        Player.Health = int.Parse(data.ToString().Split(':')[1]);
                    //    else if (data.ToString().Contains("intHPMax"))
                    //        Player.HealthMax = int.Parse(data.ToString().Split(':')[1]);
                    //    else if (data.ToString().Contains("intMP"))
                    //        Player.Mana = int.Parse(data.ToString().Split(':')[1]);
                    //}
                    break;
            }
        }
        //{"t":"xt","b":{"r":-1,"o":{"cmd":"ct","p":{"gayifyoureadthis":{"intMP":100,"intSP":100,"intHP":3069}}}}}
        //{"t":"xt","b":{"r":-1,"o":{"cmd":"cb","p":{"dishonoured":{}},"m":{"3":{},"2":{},"1":{},"6":{},"5":{},"4":{}}}}}
        //{"t":"xt","b":{"r":-1,"o":{"userID":7.0879727E7,"monPad":2,"cmd":"playerDeath","entType":"m","did":7.0863999E7,"kid":"undefined"}}}

        //{"t":"xt","b":{"r":-1,"o":{"cmd":"uotls","o":{"intHPMax":3180,"intMP":100,"intMPMax":100,"intHP":3180},"unm":"dishonoured"}}}

        //: {"t":"xt","b":{"r":-1,"o":{"cmd":"uotls","o":{"strFrame":"Enter","intMP":100,"intState":1,"strPad":"Spawn","intSP":100,"intHP":3275},"unm":"dishonoured"}}}
        //{"t":"xt","b":{"r":-1,"o":{"cmd":"moveToArea","areaName":"battleon-99693","uoBranch":[{"strFrame":"Enter","intMP":100,"intLevel":100,"entID":3529,"strPad":"Spawn","intMPMax":100,"intHP":3275,"afk":false,"intHPMax":3275,"ty":0,"tx":0,"intState":1,"entType":"p","showHelm":true,"showCloak":true,"strUsername":"Dishonoured","uoName":"dishonoured"}],"strMapFileName":"Battleon/town-Battleon-11Feb22.swf","intType":"2","monBranch":[],"sExtra":"","areaId":13150,"strMapName":"battleon"}}}
    }

    public class HandlerAura : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        public async void Handle(JsonMessage message)
        {
            await Task.Delay(10);
            JToken list = message.DataObject?["a"];
            if (list == null)
                return;
            foreach (JToken a in list)
            {
                bool onEnemy = a["tInf"].ToString().Split(':')[0] == "m";
                int entityID = int.Parse(a["tInf"].ToString().Split(':')[1]);
                var cmd = a["cmd"];
                switch (cmd.ToString())
                {
                    case "aura+":
                    case "aura++":
                    case "aura+p":
                        addAura(a["auras"], onEnemy, entityID);
                        break;
                    case "aura-":
                    case "aura--":
                        removeAura(a["aura"], onEnemy, entityID);
                        break;

                }
            }
        }

        public async void addAura(JToken datas, bool onEnemy, int entityID)
        {
            if (datas == null)
                return;
            foreach (JToken data in datas)
            {
                Aura aura = !onEnemy ? World.PlayerAuras.FirstOrDefault((Aura x) => x.Name.Equals(data["nam"].ToString()) && x.entityID.Equals(entityID)) : World.EnemyAuras.FirstOrDefault((Aura x) => x.Name.Equals(data["nam"].ToString()) && x.entityID.Equals(entityID));
                if (!onEnemy)
                {
                    if (aura == null)
                    {
                        Aura newAura = new Aura
                        {
                            entityID = entityID,
                            Name = data["nam"].ToString(),
                            Value = 1,
                            realValue = data["val"] == null ? null : data["val"].ToString(),
                            disableType = data["cat"] == null ? null : data["cat"].ToString(),
                            durationSpan = int.Parse(data["dur"].ToString()),
                            timeNow = DateTime.Now,

                        };
                        World.PlayerAuras.Add(newAura);
                        World.auraCountdown += newAura.countdown;
                    }
                    else
                    {
                        World.auraCountdown -= aura.countdown;
                        World.PlayerAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.Value++);
                        World.PlayerAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.realValue = data["val"] == null ? null : data["val"].ToString());
                        World.PlayerAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.disableType = data["cat"] == null ? null : data["cat"].ToString());
                        World.PlayerAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.durationSpan = int.Parse(data["dur"].ToString()));
                        World.PlayerAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.timeNow = DateTime.Now);
                        World.auraCountdown += aura.countdown;
                    }
                }
                else
                {
                    if (aura == null)
                    {
                        Aura newAura = new Aura
                        {
                            entityID = entityID,
                            Name = data["nam"].ToString(),
                            Value = 1,
                            realValue = data["val"] == null ? null : data["val"].ToString(),
                            disableType = data["cat"] == null ? null : data["cat"].ToString(),
                            durationSpan = int.Parse(data["dur"].ToString()),
                            timeNow = DateTime.Now
                        };
                        World.EnemyAuras.Add(newAura);
                        World.auraCountdown += newAura.countdown;
                    }
                    else
                    {
                        World.auraCountdown -= aura.countdown;
                        World.EnemyAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.Value++);
                        World.EnemyAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.realValue = data["val"] == null ? null : data["val"].ToString());
                        World.EnemyAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.disableType = data["cat"] == null ? null : data["cat"].ToString());
                        World.EnemyAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.durationSpan = int.Parse(data["dur"].ToString()));
                        World.EnemyAuras.Where(a => a.Name == data["nam"].ToString()).ToList().ForEach(v => v.timeNow = DateTime.Now);
                        World.auraCountdown += aura.countdown;
                    }
                }
            }

        }

        public void removeAura(JToken datas, bool onEnemy, int entityID)
        {
            if (datas == null)
                return;
            Aura aura = !onEnemy ? World.PlayerAuras.FirstOrDefault((Aura x) => x.Name.Equals(datas["nam"].ToString()) && x.entityID.Equals(entityID)) : World.EnemyAuras.FirstOrDefault((Aura x) => x.Name.Equals(datas["nam"].ToString()) && x.entityID.Equals(entityID));
            if (!onEnemy)
            {
                World.PlayerAuras.Remove(aura);
            }
            else
            {
                World.EnemyAuras.Remove(aura);
            }
            World.auraCountdown -= aura.countdown;
        }

    }
}