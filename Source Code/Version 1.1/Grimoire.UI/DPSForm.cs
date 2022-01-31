using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Game;
using Grimoire;
using Grimoire.Game.Data;
using DarkUI.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grimoire.UI
{
    public partial class DPSForm : DarkForm
    {
        public DPSForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        public Dictionary<string, int> Damage = new Dictionary<string, int>();

        public Dictionary<string, int> DamagePerSecond = new Dictionary<string, int>();

        private void DPSForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DamagePerSecond.Count == 0) return;
            int[] dps = DamagePerSecond.Values.ToArray();
            darkLabel1.Text = int.Parse(string.Concat(dps)).ToString();
            chart1.Series.Clear();
            foreach (var kv in DamagePerSecond)
            {
                chart1.Series.Add(kv.Key);
                chart1.Series[kv.Key].Points.Add(kv.Value);
                chart1.Series[kv.Key].ChartType = SeriesChartType.Column;
            }
            DamagePerSecond.Clear();
        }

        private void DPSForm_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
        }
    }
}
