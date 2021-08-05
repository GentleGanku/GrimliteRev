using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class CosmeticForm : DarkForm
    {
        public static CosmeticForm Instance
        {
            get;
        } = new CosmeticForm();

        public CosmeticForm()
        {
            InitializeComponent();
            lbItems.SelectionMode = SelectionMode.MultiExtended;
        }

        private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Player.IsLoggedIn)
                return;
            cbPlayer.Items.Clear();
            World.RefreshDictionary();
            cbPlayer.Items.AddRange(World.Players.ToArray());
            cbPlayer.SelectedItem = cbPlayer.Items.Count > 0 ? cbPlayer.Items[0] : null;
        }

        private void btnGrabCosm_Click(object sender, EventArgs e)
        {
            if (cbPlayer.SelectedIndex > -1)
                try
                {
                    try
                    {
                        if (lbItems.Items[lbItems.Items.Count - 1].ToString() != " ")
                            lbItems.Items.Add(" ");
                    }
                    catch { }
                    lbItems.Items.AddRange(CosmeticEquipment.Get(((PlayerInfo)cbPlayer.SelectedItem).EntID).ToArray());
                    lbItems.Items.Add(" ");
                }
                catch
                {

                }
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            lbItems.Items.Cast<CosmeticEquipment>().ForEach(x => x.Equip());
        }

        private void btnEquipSelected_Click(object sender, EventArgs e)
        {
            try
            {
                lbItems.SelectedItems.Cast<CosmeticEquipment>().ForEach(x => x.Equip());
            }
            catch
            {

            }
        }

        private void lnkGrabTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lbItems.Items.Clear();
            lbItems.Items.AddRange(CosmeticEquipment.Get(Flash.Instance.GetGameObject<int>("world.myAvatar.target.uid")).ToArray());
        }

        private void CosmeticForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn)
                return;
            cbPlayer.Items.Clear();
            World.RefreshDictionary();
            cbPlayer.Items.AddRange(World.Players.ToArray());
            cbPlayer.SelectedItem = cbPlayer.Items.Count > 0 ? cbPlayer.Items[0] : null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbItems.Items.Clear();
            TextBox[] textboxes = new TextBox[14]
            {
                txtHelm1,
                txtHelm2,
                txtArmor1,
                txtArmor2,
                txtClass1,
                txtClass2,
                txtWeapon1,
                txtWeapon2,
                txtPet1,
                txtPet2,
                txtCape1,
                txtCape2,
                txtOff1,
                txtOff2
            };
            foreach (TextBox tb in textboxes)
                tb.Text = "";
        }

        private void linkItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cbPlayer.SelectedIndex > -1)
                try
                {
                    string lbl = ((LinkLabel)sender).Text.Split(' ')[1].Replace("Wep", "Weapon");
                    object[] array = CosmeticEquipment.Get(((PlayerInfo)cbPlayer.SelectedItem).EntID).ToArray();
                    for (int num = 0; array.Length > num; num++)
                        if (array[num].ToString().StartsWith(lbl))
                        {
                            lbItems.Items.Add(array[num]);
                            string[] i = array[num].ToString().Replace($"{lbl}: ", "").Split(';');
                            if (lbl == "Cape" && txtCape1.Text != (txtCape1.Text.NullIfEmpty() ?? i[0]))
                            {
                                txtCape1.Text = i[0];
                                txtCape2.Text = i[1];
                            }
                            else if (lbl == "Class" && txtClass1.Text != (txtClass1.Text.NullIfEmpty() ?? i[0]))
                            {
                                txtClass1.Text = i[0];
                                txtClass2.Text = i[1];
                            }
                            else if (lbl == "Pet" && txtPet2.Text != (txtPet1.Text.NullIfEmpty() ?? i[0]))
                            {
                                txtPet1.Text = i[0];
                                txtPet2.Text = i[1];
                            }
                            else if (lbl == "Weapon" && txtWeapon1.Text != (txtWeapon1.Text.NullIfEmpty() ?? i[0]))
                            {
                                txtWeapon1.Text = i[0];
                                txtWeapon2.Text = i[1];
                            }
                            else if (lbl == "Helm" && txtHelm1.Text != (txtHelm1.Text.NullIfEmpty() ?? i[0]))
                            {
                                txtHelm1.Text = i[0];
                                txtHelm2.Text = i[1];
                            }
                            else if (lbl == "Armor" && txtArmor1.Text != (txtArmor1.Text.NullIfEmpty() ?? i[0]))
                            {
                                txtArmor1.Text = i[0];
                                txtArmor2.Text = i[1];
                            }
                        }
                }catch{}
        }

        private void lbItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lbItems.SelectedItem != null)
                {
                    int selectedIndex = lbItems.SelectedIndex;
                    if (selectedIndex > -1)
                    {
                        for (int x = lbItems.SelectedIndices.Count - 1; x >= 0; x--)
                        {
                            int idx = lbItems.SelectedIndices[x];
                            lbItems.Items.RemoveAt(idx);
                        }
                        lbItems.EndUpdate();
                    }
                }
            }
            if (e.KeyCode == Keys.Enter && lbItems.SelectedIndex > -1)
            {
                var selectedItems = lbItems.SelectedItems;
                for(int num = 0; selectedItems.Count > num; num++)
                {
                    string lbl = selectedItems[num].ToString().Split(':')[0];
                    if (selectedItems[num].ToString() == " ")
                        continue;
                    var i = selectedItems[num].ToString().Replace($"{lbl}: ", "").Split(';');
                    switch (lbl)
                    {
                        case "Cape":
                            txtCape1.Text = i[0];
                            txtCape2.Text = i[1];
                            break;

                        case "Pet":
                            txtPet1.Text = i[0];
                            txtPet2.Text = i[1];
                            break;

                        case "Class":
                            txtClass1.Text = i[0];
                            txtClass2.Text = i[1];
                            break;

                        case "Helm":
                            txtHelm1.Text = i[0];
                            txtHelm2.Text = i[1];
                            break;

                        case "Armor":
                            txtArmor1.Text = i[0];
                            txtArmor2.Text = i[1];
                            break;

                        default:
                            txtWeapon1.Text = i[0];
                            txtWeapon2.Text = i[1];
                            break;
                    }
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            string txt = s.Name.Replace("btn", "").Replace("Set", "");
            string file;
            string link;
            switch (txt)
            {
                case "Cape":
                    txt = "ba";
                    file = txtCape1.Text;
                    link = txtCape2.Text;
                    break;

                case "Class":
                    txt = "ar";
                    file = txtClass1.Text;
                    link = txtClass2.Text;
                    break;

                case "Pet":
                    txt = "pe";
                    file = txtPet1.Text;
                    link = txtPet2.Text;
                    break;

                case "Helm":
                    txt = "he";
                    file = txtHelm1.Text;
                    link = txtHelm2.Text;
                    break;

                case "Armor":
                    txt = "co";
                    file = txtArmor1.Text;
                    link = txtArmor2.Text;
                    break;

                case "Offhand":
                    txt = "Off";
                    file = txtOff1.Text;
                    link = txtOff2.Text;
                    break;

                default:
                    txt = "Weapon";
                    file = txtWeapon1.Text;
                    link = txtWeapon2.Text;
                    break;
            }
            dynamic equip = new ExpandoObject();
            equip.sFile = file;
            equip.sLink = link.ReplaceLink();
            equip.sType = txt;
            //equip.sMeta = Meta;
            Flash.Call("SetEquip", new object[2] { txt , equip });
        }

        private void btnSaveSet_Click(object sender, EventArgs e)
        {
            string[] _data = new string[21]
            {
               "Helmet:",
               $"he file:{txtHelm1.Text ?? "None"}",
               $"he link:{txtHelm2.Text ?? "None"}",
               "\r\nArmor:",
               $"co file:{txtArmor1.Text ?? "None"}",
               $"co link:{txtArmor2.Text ?? "None"}",
               "\r\nClass:",
               $"ar file:{txtClass1.Text ?? "None"}",
               $"ar link:{txtClass2.Text ?? "None"}\r\n",
               "\r\nWeapon:",
               $"Weapon file:{txtWeapon1.Text ?? "None"}",
               $"Weapon link:{txtWeapon2.Text ?? "None"}\r\n",
               "\r\nPet:",
               $"pe file:{txtPet1.Text ?? "None"}",
               $"pe link:{txtPet2.Text ?? "None"}",
               "\r\nCape:",
               $"ba file:{txtCape1.Text ?? "None"}",
               $"ba link:{txtCape2.Text ?? "None"}",
               "\r\nOff:",
               $"off file:{txtOff1.Text ?? "None"}",
               $"off link:{txtOff2.Text ?? "None"}"
            };
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = Application.StartupPath + "\\Sets",
                Filter = "Grimoire sets|*.gset"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllLines(saveFileDialog.FileName, _data);
        }

        private void btnLoadSet_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Application.StartupPath + "\\Sets",
                Filter = "Grimoire sets|*.gset"
            };
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] s = File.ReadAllLines(openFileDialog.FileName).Where(empty => empty.Trim() != string.Empty).ToArray();
                try
                {
                    txtHelm1.Text = s[1].Split(':')[1];
                    txtHelm2.Text = s[2].Split(':')[1];
                    txtArmor1.Text = s[4].Split(':')[1];
                    txtArmor2.Text = s[5].Split(':')[1];
                    txtClass1.Text = s[7].Split(':')[1];
                    txtClass2.Text = s[8].Split(':')[1];
                    txtWeapon1.Text = s[10].Split(':')[1];
                    txtWeapon2.Text = s[11].Split(':')[1];
                    txtPet1.Text = s[13].Split(':')[1];
                    txtPet2.Text = s[14].Split(':')[1];
                    txtCape1.Text = s[16].Split(':')[1];
                    txtCape2.Text = s[17].Split(':')[1];
                    txtOff1.Text = s[19].Split(':')[1];
                    txtOff2.Text = s[20].Split(':')[1];
                }
                catch { }
            }
        }

        private void lbItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbItems.SelectedItem.ToString() == " ")
                return;
            string lbl = lbItems.SelectedItem.ToString().Split(':')[0];
            string[] i = lbItems.SelectedItem.ToString().Replace($"{lbl}: ", "").Split(';');
            switch(lbl)
            {
                case "Cape":
                    txtCape1.Text = i[0];
                    txtCape2.Text = i[1];
                    break;

                case "Pet":
                    txtPet1.Text = i[0];
                    txtPet2.Text = i[1];
                    break;

                case "Class":
                    txtClass1.Text = i[0];
                    txtClass2.Text = i[1];
                    break;

                case "Helm":
                    txtHelm1.Text = i[0];
                    txtHelm2.Text = i[1];
                    break;

                case "Armor":
                    txtArmor1.Text = i[0];
                    txtArmor2.Text = i[1];
                    break;

                default:
                    txtWeapon1.Text = i[0];
                    txtWeapon2.Text = i[1];
                    break;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = lbItems.SelectedIndices;
                for(int x = selectedItems.Count - 1; x >= 0; x--)
                {
                    int idx = selectedItems[x];
                    lbItems.Items.RemoveAt(idx);
                }
            }
            catch
            {

            }
        }
    }
}