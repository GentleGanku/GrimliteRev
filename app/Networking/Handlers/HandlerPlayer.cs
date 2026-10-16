using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using System.Threading.Tasks;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerSkills : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "sAct"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            await Task.Delay(10);
            Flash.Call("getRange", new string[0]);
            OptionsManager.SetInfiniteRange(OptionsManager.InfiniteRange);
        }

        #endregion
    }

    public class HandlerBotSkills : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            if (message.RawContent.IndexOf("\"msg\":", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.IndexOf("counter attack", StringComparison.OrdinalIgnoreCase) > -1 && message.RawContent.Contains($"\"strFrame\":\"{Player.Cell}\""))
            {
                Proxy.DisableBotSkill();
            }
            else if (message.RawContent.IndexOf("\"nam\":\"Counter Attack\"", StringComparison.OrdinalIgnoreCase) != -1)
            {
                JArray auraEvents = (JArray)message.DataObject?["a"];
                if (auraEvents != null)
                {
                    if (auraEvents.Any(a => ((a["cmd"].ToString() == "aura-") || (a["cmd"].ToString() == "aura--")) && a["aura"]["nam"].ToString().Equals("Counter Attack", StringComparison.OrdinalIgnoreCase)))
                    {
                        Proxy.EnableBotSkill();
                    }
                }
            }
        }

        #endregion
    }

        public class HandlerInfo : IJsonMessageHandler
    {
            #region Properties

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

            #endregion

            #region Methods

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
                    break;
                case "moveToArea":
                    break;
                case "uotls":
                    break;
            }
        }

            #endregion
        }

    public class HandlerAura : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            await Task.Delay(10);
            JToken auraEvents = message.DataObject?["a"];
            if (auraEvents == null)
                return;
            foreach (JToken a in auraEvents)
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

        #endregion
    }
}