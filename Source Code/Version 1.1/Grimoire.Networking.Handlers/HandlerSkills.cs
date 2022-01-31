using Grimoire.Game;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Grimoire.Botting;
using System.Windows.Forms;
using Grimoire.UI;
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

        public void Handle(JsonMessage message)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string mana = "0";
            try
            {
                mana = c.Get("mana");
            }
            catch {}
            JToken jToken = message.DataObject["actions"];
            if (jToken.Type != JTokenType.Null)
            {
                JToken passives = jToken["passive"];
                if (passives.Type != JTokenType.Null)
                    foreach (JToken item in passives)
                    { 
                        item["range"] = OptionsManager.InfiniteRange ? "20000" : item["range"];
                        item["mp"] = mana == "1" ? "0" : item["mp"];
                    }
                JToken actives = jToken["active"];
                if (actives.Type != JTokenType.Null)
                    foreach (JToken item in actives)
                    {
                        item["range"] = OptionsManager.InfiniteRange ? "20000" : item["range"];
                        item["mp"] = mana == "1" ? "0" : item["mp"];
                    }
            }
        }
    }

    public class HandlerDPS : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "ct"
        };

        public void Handle(JsonMessage message)
        {
            JObject obj = JObject.Parse(message.RawContent);
            JToken actions = obj["b"]["o"]["sarsa"];
            //JToken targets = message.DataObject["p"]["username"].ToString();
            if (actions.Type != JTokenType.Null)
            {
                foreach(JToken action in actions)
                {
                    var hitInfos = action["a"];
                    foreach (JToken hit in hitInfos)
                    {
                        var hp = hit["hp"];
                        int dmg = int.Parse(hp.ToString());
                        
                        /* not used yet
                        if(!DPSForm.Instance.DamagePerSecond.TryGetValue(Player.Username, out int num))
                            DPSForm.Instance.DamagePerSecond.Add(Player.Username, dmg);
                        else
                            DPSForm.Instance.DamagePerSecond[Player.Username] += dmg;

                        if (!DPSForm.Instance.Damage.TryGetValue(Player.Username, out int _))
                            DPSForm.Instance.Damage.Add(Player.Username, dmg);
                        else
                            DPSForm.Instance.Damage[Player.Username] += dmg;
                        */

                        LogForm.Instance.AppendDebug($"Damage by you: {hp.ToString()}\r\n");
                    }
                }
            }
        }
    }
}