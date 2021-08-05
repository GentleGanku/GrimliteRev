using AxShockwaveFlashObjects;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grimoire.Botting
{
    public static class BotUtilities
    {
        public static ManualResetEvent BankLoadEvent;

        public static AxShockwaveFlash flash;

        public static async Task WaitUntil(this IBotEngine instance, Func<bool> condition, Func<bool> prerequisite = null, int timeout = 15)
        {
            int iterations = 0;
            while ((prerequisite ?? (() => instance.IsRunning && Player.IsLoggedIn && Player.IsAlive))() && !condition() && (iterations < timeout || timeout == -1))
            {
                await Task.Delay(1000);
                iterations++;
            }
        }

        public static bool RequiresDelay(this IBotCommand cmd)
        {
            if (cmd is StatementCommand || cmd is CmdIndex || cmd is CmdLabel || cmd is CmdGotoLabel || cmd is CmdBlank || cmd is CmdSkillSet)
                return false;
            return true;
        }

        public static void LoadAllQuests(this IBotEngine instance)
        {
            List<int> list = new List<int>();
            foreach (IBotCommand command in instance.Configuration.Commands)
            {
                if (command is CmdAcceptQuest cmdAcceptQuest)
                {
                    list.Add(cmdAcceptQuest.Quest.Id);
                    
                }
                else if (command is CmdCompleteQuest cmdCompleteQuest)
                {
                    list.Add(cmdCompleteQuest.Quest.Id);
                }
            }
            list.AddRange(instance.Configuration.Quests.Select((Quest q) => q.Id));
            if (list.Count > 0)
            {
                Player.Quests.Get(list);
            }
        }

        public static void LoadBankItems(this IBotEngine instance)
        {
            if (instance.Configuration.Commands.Any((IBotCommand c) => c is CmdBankSwap || c is CmdBankTransfer))
            {
                Player.Bank.LoadItems();
            }
        }

        static BotUtilities()
        {
            BankLoadEvent = new ManualResetEvent(initialState: false);
        }
        
        /**
        public string CallGameFunction(string path, params object[] args)
        {
            return args.Length > 0 ? Flash.FlashUtil.Call("callGameFunction", new object[] { path }.Concat(args).ToArray()) : Flash.FlashUtil.Call("callGameFunction0", path);
        }

        public void SetGameObject(string path, object value)
        {
            Flash.FlashUtil.Call("setGameObject", path, value);
        }

        public string GetGameObject(string path)
        {
            return Flash.FlashUtil.Call("getGameObject", path);
        }
        **/

        public static string Call(string function, bool base64 = false)
        {
            try
            {
                string request = $"<invoke name=\"{function}\"returntype=\"xml\"></invoke>";
                string text = XElement.Parse(flash.CallFunction(request)).FirstNode?.ToString();
                if (text == null)
                {
                    return string.Empty;
                }
                if (!base64)
                {
                    return text.Correct().SanitizeXml();
                }
                return text.FromBase64().Correct().SanitizeXml();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}