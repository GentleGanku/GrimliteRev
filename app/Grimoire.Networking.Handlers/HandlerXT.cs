using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using Grimoire.Tools;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Grimoire.Networking.Handlers
{
    public class HandlerConnection : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[2]
        {
            "loginResponse",
            "logoutWarning"
        };

        //%xt%loginResponse%-1%true%22333%Koringa%%2022-22-22T08:49:13%sNews=154,sMap=news/Map-UI_r37.swf,sBook=news/spiderbook3.swf,sAssets=Assets_20211229.swf,gMenu=dynamic-gameMenu-17Jan22.swf,sVersion=R0015%3.0065%
        //%xt%logoutWarning%-1%%30%
        //{"t":"xt","b":{"r":-1,"o":{"cmd":"balance","intGold":11122233,"intExp":122,"intCoins":665,"iUpgDays":-333}}}
        public void Handle(XtMessage message)
        {
            switch (message.Command)
            {
                case "loginResponse":
                    Player.IsLoggedIn = true;
                    break;
                case "logoutWarning":
                    Player.IsLoggedIn = false;
                    break;
            }
        }
    }

    //public class HandlerRespawn : IXtMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "respawnMon",
    //        "resTimed"
    //    };

    //    //%xt%respawnMon%-1%3%
    //    //%xt%resTimed%-1%
    //    //%xt%zm%resPlayerTimed%69%3969922%
    //    public void Handle(XtMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "respawnMon":
    //                World.CurrentMap.Monsters.Where(m => m.MonMapID == int.Parse(message.Arguments[4]))
    //                .Select(m => {
    //                    m.CurrentState = 1;
    //                    m.Health = m.MaxHealth;
    //                    m.Mana = m.MaxMana;
    //                    m.Auras.Clear();
    //                    return m;
    //                });
    //                break;
    //            case "resTimed":
    //                Map.Players.Where(a => a.Username.Equals(Player.Username, StringComparison.OrdinalIgnoreCase))
    //                .Select(a => {
    //                    //a.Cell = a.Cell;
    //                    //a.Pad = a.Pad;
    //                    a.CurrentState = 1;
    //                    a.HP = a.MaxHP;
    //                    a.MP = a.MaxMP;
    //                    a.Auras.Clear();
    //                    return a;
    //                });
    //                break;
    //        }
    //    }
    //}

    //public class HandlerEntity : IXtMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[3]
    //    {
    //        "exitArea",
    //        "uotls",
    //        "mtls"
    //    };

    //    //%xt%uotls%-1%Koringa%sp:8,tx:538,ty:331,strFrame:Enter%
    //    //%xt%uotls%-1%Koringa%mvts:-1,px:500,py:375,strPad:Left,bResting:false,mvtd:0,tx:0,ty:0,strFrame:Frame1%
    //    //%xt%exitArea%-1%38165%Koringa%
    //    public void Handle(XtMessage message)
    //    {
    //        switch (message.Command)
    //        {
    //            case "exitArea":
    //                Map.Players.Remove(Map.Players.Find(a => a.Username.Equals(message.Arguments[5], StringComparison.OrdinalIgnoreCase)));
    //                break;
    //            case "uotls":
    //                List<string> datas = message.Arguments[5].Split(',').ToList();
    //                Map.Players.Where(a => a.Username.Equals(message.Arguments[4], StringComparison.OrdinalIgnoreCase))
    //                .Select(a => {
    //                    a.X = float.Parse(datas.Find(d => d.StartsWith("tx")).Split(':')[1]);
    //                    a.Y = float.Parse(datas.Find(d => d.StartsWith("ty")).Split(':')[1]);
    //                    a.Cell = datas.Find(d => d.StartsWith("strFrame")).Split(':')[1];
    //                    a.Pad = datas.Find(d => d.StartsWith("strPad")).Split(':')[1] ?? a.Pad;
    //                    a.AFK = bool.Parse(datas.Find(d => d.StartsWith("afk")).Split(':')[1] ?? a.AFK.ToString());
    //                    return a;
    //                }).ToList();
    //                break;
    //            case "mtls":
    //                List<string> mDatas = message.Arguments[5].Split(',').ToList();
    //                World.CurrentMap.Monsters.Where(a => a.MonMapID == int.Parse(message.Arguments[4]))
    //                .Select(a => {
    //                    a.Cell = mDatas.Find(d => d.StartsWith("strFrame")).Split(':')[1] ?? a.Cell;
    //                    a.Health = int.Parse(mDatas.Find(d => d.StartsWith("intHP")).Split(':')[1].ToString() ?? a.Health.ToString());
    //                    a.MaxHealth = int.Parse(mDatas.Find(d => d.StartsWith("intHPMax")).Split(':')[1].ToString() ?? a.MaxHealth.ToString());
    //                    a.Mana = int.Parse(mDatas.Find(d => d.StartsWith("intMP")).Split(':')[1].ToString() ?? a.Mana.ToString());
    //                    a.MaxMana = int.Parse(mDatas.Find(d => d.StartsWith("intMPMax")).Split(':')[1].ToString() ?? a.MaxMana.ToString());
    //                    return a;
    //                }).ToList();
    //                break;
    //        }
    //    }
    //}

    //public class HandlerMTC : IXtMessageHandler
    //{
    //    public string[] HandledCommands
    //    {
    //        get;
    //    } = new string[2]
    //    {
    //        "mtcid",
    //        "gtc"
    //    };

    //    public void Handle(XtMessage message)
    //    {
    //        Map.Players.Where(a => a.Username.Equals(Player.Username, StringComparison.OrdinalIgnoreCase))
    //        .Select(a => {
    //            a.Cell = Flash.Call<string>("Cell", new string[0]);
    //            a.Pad = Flash.Call<string>("Pad", new string[0]);
    //            return a;
    //        }).ToList();
    //    }
    //}

}