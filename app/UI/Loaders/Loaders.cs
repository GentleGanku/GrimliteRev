using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Grimoire.UI
{
    public partial class Loaders : DarkForm
    {
        #region Fields

        private const string XmlNodeTag = "n";
        private const string XmlNodeTextAtt = "t";
        private const string XmlNodeTagAtt = "tg";
        private const string XmlNodeImageIndexAtt = "imageindex";
        private readonly string font = Config.Instance.GetValue<string>("font");
        private readonly float? fontSize = float.Parse(Config.Instance.GetValue<string>("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        #endregion

        #region Constructors

        private Loaders()
        {
            InitializeComponent();
        }

        #endregion

        #region Enums

        public enum Type
        {
            ShopItems,
            QuestIDs,
            Quests,
            InventoryItems,
            TempItems,
            BankItems,
            Monsters
        }

        #endregion

        #region Properties

        public static Loaders Instance
        {
            get;
        } = new Loaders();

        public static Type TreeType
        {
            get;
            set;
        }

        #endregion

        #region Methods

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                int result;
                switch (cbLoad.SelectedIndex)
                {
                    case 0:
                        if (int.TryParse(txtLoaders.Text, out result))
                        {
                            Shop.LoadHairShop(result);
                        }
                        break;

                    case 1:
                        if (int.Parse(txtLoaders.Text) == 399)
                        {
                            string secretShop = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"shopinfo\":{\"bUpgrd\":\"0\",\"items\":[{\"ItemID\":\"69\",\"sFaction\":\"None\",\"iClass\":\"0\",\"sElmt\":\"None\",\"sLink\":\"EIoDA\",\"bStaff\":\"0\",\"iRng\":\"10\",\"iDPS\":\"100\",\"bCoins\":\"1\",\"sES\":\"None\",\"sType\":\"Item\",\"iCost\":\"0\",\"iRty\":10,\"iQty\":\"1\",\"sIcon\":\"iibag\",\"iLvl\":\"1\",\"FactionID\":\"1\",\"bTemp\":\"0\",\"iQtyRemain\":\"-1\",\"iReqRep\":\"0\",\"iQSvalue\":\"0\",\"ShopItemID\":\"69420\",\"EnhID\":\"0\",\"iStk\":\"1\",\"sDesc\":\"Please talk to Swaggy or visit account.aq.com for information on how to redeem this token for an Awesome Prize!\",\"bHouse\":\"0\",\"bUpg\":\"0\",\"iReqCP\":\"0\",\"sName\":\"Epic Item of Digital Awesomeness\",\"iQSindex\":\"-1\"},{\"ItemID\":\"30629\",\"sFaction\":\"None\",\"iClass\":\"0\",\"sElmt\":\"None\",\"sLink\":\"NecroticSwordOfDoom\",\"bStaff\":\"0\",\"iRng\":\"10\",\"iDPS\":\"100\",\"bCoins\":\"1\",\"sES\":\"Weapon\",\"sType\":\"Sword\",\"iCost\":\"0\",\"iRty\":\"100\",\"iQty\":\"1\",\"sIcon\":\"iwsword\",\"iLvl\":\"1\",\"FactionID\":\"1\",\"bTemp\":\"0\",\"iQtyRemain\":\"-1\",\"iReqRep\":\"0\",\"iQSvalue\":\"0\",\"ShopItemID\":\"3619\",\"sFile\":\"items/swords/NecroticSwordOfDoomr1.swf\",\"EnhID\":\"0\",\"iStk\":\"1\",\"sDesc\":\"The darkness compels... DOOOOOOOOOOOM!!!\",\"bHouse\":\"0\",\"bUpg\":\"0\",\"iReqCP\":\"0\",\"sName\":\"Necrotic Sword of Doom\",\"iQSindex\":\"-1\"},{\"ItemID\":\"38259\",\"sFaction\":\"None\",\"iClass\":\"0\",\"sElmt\":\"None\",\"sLink\":\"VoidArmor\",\"bStaff\":\"0\",\"iRng\":\"10\",\"iDPS\":\"100\",\"bCoins\":\"1\",\"sES\":\"ar\",\"sType\":\"Class\",\"iCost\":\"0\",\"iRty\":\"100\",\"iQty\":\"1\",\"sIcon\":\"iiclass\",\"iLvl\":\"50\",\"FactionID\":\"1\",\"bTemp\":\"0\",\"iQtyRemain\":\"-1\",\"iReqRep\":\"0\",\"iQSvalue\":\"0\",\"ShopItemID\":\"5128\",\"sFile\":\"VoidArmor.swf\",\"EnhID\":\"0\",\"iStk\":\"1\",\"sDesc\":\"Recommended enhancement: Fighter. Only the strongest, toughest, most dedicated (and insane) members of the Nulgath Nation can survive the trials required to unlock the Void Highlord Class!\",\"bHouse\":\"0\",\"bUpg\":\"0\",\"iReqCP\":\"0\",\"sName\":\"Void Highlord\",\"iQSindex\":\"-1\"},{\"ItemID\":48571,\"sLink\":\"LegionRevenant\",\"sElmt\":\"None\",\"bStaff\":0,\"iRng\":10,\"bCoins\":1,\"iDPS\":0,\"sES\":\"ar\",\"sType\":\"Class\",\"iCost\":0,\"iRty\":13,\"iQSValue\":0,\"iQty\":1,\"sReqQuests\":\"\",\"sIcon\":\"iiclass\",\"iLvl\":1,\"bTemp\":0,\"bPTR\":0,\"sFile\":\"LegionRevenantr2.swf\",\"iQSIndex\":-1,\"iStk\":1,\"sDesc\":\"Steeped in darkness, Legion Revenants derive their power from the essence responsible for the Legion's un-death. The Legion flows through them, and they are the Legion.\",\"bHouse\":0,\"bUpg\":0,\"sName\":\"Legion Revenant\",\"sMeta\":1820},{\"ItemID\":56776,\"sElmt\":\"None\",\"sLink\":\"PollyRogerPet\",\"bStaff\":0,\"iRng\":10,\"iDPS\":0,\"bCoins\":1,\"sES\":\"pe\",\"sType\":\"Pet\",\"iCost\":0,\"iRty\":50,\"iQSValue\":0,\"iQty\":1,\"sReqQuests\":\"\",\"sIcon\":\"iipet\",\"iLvl\":1,\"bTemp\":0,\"bPTR\":0,\"sFile\":\"items/pets/PollyRogerPet.swf\",\"iQSIndex\":-1,\"iStk\":1,\"sDesc\":\"Whether you sail the seas or skies, victory is always within your reach.(Item does 30% more damage to all tagged monsters when equipped.)\",\"bHouse\":0,\"bUpg\":0,\"sName\":\"Polly Roger\"}],\"sField\":\"\",\"ShopID\":\"6969\",\"bStaff\":\"0\",\"bHouse\":\"0\",\"Location\":\"Menu\",\"iIndex\":\"-1\",\"sName\":\"Secret Shop\"},\"cmd\":\"loadShop\"}}}";
                            World.GameMessage("You have found the Secret Shop!");
                            _ = Proxy.Instance.SendToClient(secretShop);
                            break;
                        }
                        if (int.TryParse(txtLoaders.Text, out result))
                        {
                            Shop.Load(result).Forget("loading a shop");
                        }
                        break;

                    case 2:
                        if (txtLoaders.Text.Contains(","))
                        {
                            LoadQuests(txtLoaders.Text);
                        }
                        else if (int.TryParse(txtLoaders.Text, out result))
                        {
                            Player.Quests.Load(result);
                        }
                        break;

                    case 3:
                        Shop.LoadArmorCustomizer();
                        break;
                }
            }
        }

        private void LoadQuests(string str)
        {
            string[] source = str.Split(',');
            if (source.All((string s) => s.All(char.IsDigit)))
            {
                Player.Quests.Load(source.Select(int.Parse).ToList());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Save grabber data",
                CheckFileExists = false,
                Filter = "XML files|*.xml"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextWriter textWriter = new XmlTextWriter(openFileDialog.FileName, System.Text.Encoding.ASCII)
                {
                    Formatting = Formatting.Indented
                };
                textWriter.WriteStartDocument();
                textWriter.WriteRaw("\r\n");
                textWriter.WriteStartElement("TreeView");
                SaveNodes(treeGrabbed.Nodes, textWriter);

                textWriter.WriteEndElement();

                textWriter.Close();
            }
        }

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                try
                {
                    string toadd = "";
                    for (int times = node.Text.Split(':')[0].Length; 9 > times; times++)
                        toadd += " ";
                    textWriter.WriteAttributeString(XmlNodeTextAtt, $"{node.Text.Split(':')[0]}:{toadd}{node.Text.Split(':')[1]}");
                }
                catch
                {
                    textWriter.WriteAttributeString(XmlNodeTextAtt, $"{node.Text}");
                }
                if (node.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt, node.Tag.ToString());
                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                treeGrabbed.BeginUpdate();
                treeGrabbed.Nodes.Clear();
                switch (cbGrab.SelectedIndex)
                {
                    case 0:
                        Grabber.GrabShopItems(treeGrabbed);
                        break;

                    case 1:
                        Grabber.GrabQuestIds(treeGrabbed);
                        break;

                    case 2:
                        Grabber.GrabQuests(treeGrabbed);
                        break;

                    case 3:
                        Grabber.GrabInventoryItems(treeGrabbed);
                        break;

                    case 4:
                        Grabber.GrabTempItems(treeGrabbed);
                        break;

                    case 5:
                        Grabber.GrabBankItems(treeGrabbed);
                        break;

                    case 6:
                        Grabber.GrabMonsters(treeGrabbed);
                        break;
                }
                treeGrabbed.EndUpdate();
            }
        }

        private void Loaders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                e.Cancel = true;
                Hide();
            }
        }

        private void Loaders_Load(object sender, EventArgs e)
        {
            this.FormClosing += this.Loaders_FormClosing;
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        private void cbLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoad.SelectedIndex == cbLoad.Items.Count - 2)
            {
                btnLoad.Visible = false;
                tableLayoutPanel1.Visible = true;
            }
            else
            {
                btnLoad.Visible = true;
                tableLayoutPanel1.Visible = false;
            }
        }

        private async void btnForceAccept_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn)
            {
                try
                {
                    int questID = int.Parse(txtLoaders.Text);
                    if (Player.Quests.LoadedQuests.Find(q => q.Id == questID) == null)
                    {
                        Player.Quests.GetQuest(questID);
                        await BotManager.Instance.ActiveBotEngine.WaitUntil(() => Player.Quests.LoadedQuests.Find(q => q.Id == questID) != null, () => Player.IsLoggedIn, 6, 500);
                    }
                    await BotManager.Instance.ActiveBotEngine.WaitUntil(() => World.IsActionAvailable(LockActions.AcceptQuest), () => Player.IsLoggedIn, 10, 500);
                    Player.AcceptQuest(questID);
                }
                catch (Exception ex)
                {
                    ErrorReporter.Log("force-accepting a quest", ex);
                }
            }
        }

        #endregion
    }
}