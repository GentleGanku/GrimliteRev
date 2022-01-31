using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
    public class HandlerChat : IXtMessageHandler
    {
        
        public string[] HandledCommands
        {
            get;
        } = new string[4]
        {
            "chatm",
            "warning",
            "whisper",
            "message"
        };

        Dictionary<string, string> tercesstravels = new Dictionary<string, string>
        {
                { ".oblivion",   "Enter,Spawn"  },
                { ".twins",     "Twins,Left"   },
                { ".swindle",   "Swindle,Left"   },
                { ".nulgath",   "Boss2,Right"  },
                { ".carnage",   "m4,Top"    },
                { ".lae",   "m5,Top"    },
                { ".polish",    "m12,Top"    },
                { ".taro",    "Taro,Left"    },
        };

        Dictionary<string, string> mapnames = new Dictionary<string, string>
        {
                { ".engineer",   "ultraengineer"  },
                { ".warden",     "ultrawarden"   },
                { ".ezrajal",     "ultraezrajal"   },
                { ".mondummy",   "classhall"   },
                { ".isa",   "icestormarena"  },
                { ".raxgore",   "doomvaultb"  },
        };

        Dictionary<string, string> maptravels = new Dictionary<string, string>
        {
                { ".engineer",   "r2,Left"  },
                { ".warden",     "r2,Left"   },
                { ".ezrajal",     "r2,Left"   },
                { ".mondummy",   "r4,Left"   },
                { ".isa",   "r3c,Top"  },
                { ".raxgore",   "r26,Left"  },
        };

        Dictionary<string, string> chatcommands = new Dictionary<string, string>
        {
                { ".pvp",    $"Server, %xt%zm%PVPQr%{Player.UserID}%doomarena%"  },
                { ".server", "Server, {arg5}"},
                { ".client", "Client, {arg5}"}
        };

        public void Handle(XtMessage message)
		{
            if (message.Arguments[2] == "zm" && message.Arguments[5].StartsWith("."))
            {
                //string CustomRoomnum = "-";
                string roomnum = "-";
                string text = message.Arguments[5];
                string[] texts;
                try
                {
                    texts = message.Arguments[5].Split(' ');
                }
                catch
                {
                    texts = new string[0];
                }
                if (chatcommands.ContainsKey(text) || chatcommands.ContainsKey(texts[0]))
                {
                    string[] cmdargs = chatcommands[text].Split(',');
                    string tosend = cmdargs[1];
                    tosend = new Regex("{arg(.)}", RegexOptions.IgnoreCase).Replace(tosend, (Match m) => texts[1].Replace("#037:", "%"));
                    if (cmdargs[0] == "Client")
                    {
                        Proxy.Instance.SendToClient(tosend);
                    }
                    else
                    {
                        Proxy.Instance.SendToServer(tosend);
                    }
                    MessageBox.Show(tosend);
                }
                else if (tercesstravels.ContainsKey(text))
                {
                    try
                    {
                        roomnum += message.Arguments[5].Split('-')[1];
                        roomnum = roomnum.Split('-')[1].Contains("e") ? "1e99" : roomnum;
                        text = message.Arguments[5].Split('-')[0];
                    }
                    catch
                    {
                        roomnum += "1e99";
                    }
                    roomnum = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(roomnum, (Match m) => new Random().Next(1001, 99999).ToString());
                    if (Player.Map.ToLower() != "citadel")
                    {
                        Player.ExecuteTravel(new List<IBotCommand>{
                            Player.CreateJoinCommand(
                                "citadel" + roomnum, 
                                "m22", "Left"),
                        });
                    }
                    Player.ExecuteTravel(new List<IBotCommand>{
                        Player.CreateJoinCommand(
                            map: "tercessuinotlim" + roomnum, 
                            cell: tercesstravels[text].Split(',')[0], 
                            pad: tercesstravels[text].Split(',')[1]),
                    });
                }
                else if (maptravels.ContainsKey(text))
                {
                    try
                    {
                        //CustomRoomnum += message.Arguments[5].Split('-')[1];
                        roomnum += message.Arguments[5].Split('-')[1];
                        roomnum = roomnum.Split('-')[1].Contains("e") ? "1e99" : roomnum;
                        text = message.Arguments[5].Split('-')[0];
                    }
                    catch
                    {
                       /*if (text.Contains(".public"))
                            roomnum += "1";
                        else if (text.Contains($".{CustomRoomnum}"))
                            roomnum += CustomRoomnum;
                        else*/
                            roomnum += "1e99";
                    }
                    roomnum = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(roomnum, (Match m) => new Random().Next(1001, 99999).ToString());
                    Player.ExecuteTravel(new List<IBotCommand>{
                        Player.CreateJoinCommand(
                            map: mapnames[text] + roomnum,
                            cell: maptravels[text].Split(',')[0],
                            pad: maptravels[text].Split(',')[1]),
                    });
                }
                message.Send = false;
            }
            string type = message.Arguments[2];
            string tolog = message.Arguments[4];
            message.Arguments[5] = (message.Arguments[5] == Player.Username && OptionsManager.ChangeChat) ? "You" : message.Arguments[5];
            switch (type)
            {
                case "chatm":
                    tolog = (message.Arguments[5] + message.Arguments[4]).Replace("zone~", ": ");
                    break;

                case "whisper":
                    tolog = message.Arguments[6] == Player.Username ? "From " + message.Arguments[5] : "To " + message.Arguments[6];
                    tolog = $"{tolog}: {message.Arguments[4]}";
                    break;
            }
            LogForm.Instance.AppendChat(string.Format("[{0:hh:mm:ss}] {1} \r\n", DateTime.Now, tolog));
        }
	}
}
