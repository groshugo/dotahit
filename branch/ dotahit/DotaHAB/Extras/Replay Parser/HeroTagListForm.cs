using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotaHIT.Core.Resources;
using DotaHIT.Core;
using DotaHIT.DatabaseModel.Format;

namespace DotaHIT.Extras
{
    public partial class HeroTagListForm : Form
    {
        HabPropertiesCollection hpcHeroTagsContainer;
        HabProperties hpsHeroTags;
        List<string> heroes;

        public HeroTagListForm()
        {
            InitializeComponent();

            heroes = new List<string>(DHLOOKUP.dcHeroesTaverns.Keys.Count);
            foreach (string key in DHLOOKUP.dcHeroesTaverns.Keys)
                heroes.Add(key);

            heroes.Sort(new HeroComparer());
            
            /*hpcHeroTagsContainer = new HabPropertiesCollection();
            hpcHeroTagsContainer.ReadFromFile(ReplayBrowserForm.ReplayExportCfgFileName);

            if (!hpcHeroTagsContainer.TryGetValue("HeroTags", out hpsHeroTags))
            {
                hpsHeroTags = new HabProperties(heroes.Count);
                hpcHeroTagsContainer.AddUnchecked("HeroTags", hpsHeroTags);
            }

            heroTagGridView.RowCount = heroes.Count;      */      
        }

        private void heroTagGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            HabProperties hpsHero = DHLOOKUP.hpcUnitProfiles[heroes[e.RowIndex]];

            switch (e.ColumnIndex)
            {
                case 0: // image
                    e.Value = DHRC.GetImage(hpsHero.GetStringValue("Art"));
                    break;

                case 1: // tag
                    e.Value = hpsHeroTags.GetStringValue(hpsHero.name);
                    break;

                case 2: // name
                    e.Value = hpsHero.GetStringValue("Name");
                    break;                
            }
        }

        private void heroTagGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string heroName = heroes[e.RowIndex];
                hpsHeroTags[heroName] = e.Value;
            }
        }

        private void HeroTagListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hpcHeroTagsContainer.SaveToFile(ReplayBrowserForm.ReplayExportCfgFileName);
        }

        public DialogResult ShowDialog(HabPropertiesCollection hpcCfg)
        {
            this.hpcHeroTagsContainer = hpcCfg;

            if (!hpcHeroTagsContainer.TryGetValue("HeroTags", out hpsHeroTags))
            {
                hpsHeroTags = new HabProperties(heroes.Count);
                hpcHeroTagsContainer.AddUnchecked("HeroTags", hpsHeroTags);
            }

            heroTagGridView.RowCount = heroes.Count; 

            return base.ShowDialog();
        }
    }

    class HeroComparer : IComparer<string>
    {
        int IComparer<string>.Compare(string a, string b)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(DHLOOKUP.hpcUnitProfiles[a].GetStringValue("Name"), DHLOOKUP.hpcUnitProfiles[b].GetStringValue("Name"));
        }
    }
}