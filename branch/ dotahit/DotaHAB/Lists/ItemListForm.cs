using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotaHIT.DatabaseModel.Data;
using DotaHIT.DatabaseModel.DataTypes;
using DotaHIT.Core;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;
using DotaHIT.Jass.Native.Types;
using DotaHIT.Jass;
using DotaHIT.Core.Resources;
using DotaHIT.Jass.Types;

namespace DotaHIT
{
    public partial class ItemListForm : FloatingListForm
    {
        internal enum CombineMode
        {
            Normal = 0,
            Default = Normal,
            Fast = 1
        }
        internal CombineMode combineMode = CombineMode.Default;

        internal object switchButton = null;

        internal RecordCollection Items = new RecordCollection();      

        public delegate void ItemActivateEvent(object sender, IRecord item);
        public event ItemActivateEvent ItemActivate = null;
        
        ToolStripButton emptyButton = new ToolStripButton();
        Dictionary<string, string> MorphingItems = null;
        Dictionary<string, List<List<string>>> ComplexItems = null;
        Dictionary<string, int> ComplexItemCosts = null;
        Dictionary<string, List<HabProperties>> sameLookingComplexItems = null;

        public ItemListForm()
        {            
            leftSide = false;            

            InitializeComponent();

            base_itemsLV = this.itemsLV;
            base_listMinMaxTimer = this.listMinMaxTimer;

            this.baseInit();           

            CaptionButton = this.captionB;

            fullHeight = this.Height;
            minHeight = captionB.Height;
            width = this.Width;
        }

        public void Init()
        {
            this.Items = new RecordCollection();
        }

        public void Reset()
        {
            itemsLV.Clear();
            shopsTS.Items.Clear();
            captionB.Text = "Items";
        }

        internal void PrepareList()
        {
            shopsTS.Items.Clear();

            foreach (unit shop in DHLOOKUP.shops)
            {
                ToolStripButton tsb = new ToolStripButton();
                tsb.BackColor = Color.Black;//Color.Red;//Color.LightGray;
                tsb.AutoSize = false;
                tsb.Size = shopsTS.ImageScalingSize;
                tsb.Margin = Padding.Empty;
                tsb.DisplayStyle = ToolStripItemDisplayStyle.Image;                
                tsb.Image = (Image)shop.iconImage;
                tsb.TextImageRelation = TextImageRelation.Overlay;
                tsb.Tag = shop;
                tsb.ToolTipText = shop.ID;
                tsb.Padding = new Padding(2);

                tsb.MouseEnter += new EventHandler(tsb_MouseEnter);
                tsb.MouseDown += new MouseEventHandler(tsb_MouseDown);
                tsb.MouseLeave += new EventHandler(tsb_MouseLeave);

                shopsTS.Items.Add(tsb);
            }

            if (MorphingItems != null) MorphingItems = null;
            SetCombineMode((CombineMode)DHCFG.Items["Items"].GetIntValue("CombineMode"));
        }

        void tsb_MouseLeave(object sender, EventArgs e)
        {
            if (switchButton != sender)
                (sender as ToolStripItem).BackColor = Color.Black;
        }

        void tsb_MouseEnter(object sender, EventArgs e)
        {
            if (!shopsTS.Focused)
                shopsTS.Focus();
            if (switchButton != sender)
                (sender as ToolStripItem).BackColor = Color.Gray;
        }

        void tsb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetListState(sender as ToolStripButton);
            else            
                SetListState(emptyButton);            
        }

        internal void SetListState(ToolStripButton switchButton)
        {
            if (this.switchButton == switchButton)
                return;

            this.switchButton = switchButton;

            foreach (ToolStripButton b in shopsTS.Items)
            {
                if (switchButton == b)
                    //b.Padding = new Padding(2);
                    b.BackColor = Color.White;
                else
                    b.BackColor = Color.Black;
                    //b.Padding = Padding.Empty;                    
            }
          
            SetListState(switchButton.Tag as unit);
        }

        internal void SetListState(int index)
        {
            SetListState(shopsTS.Items[index] as ToolStripButton);
        }

        internal void SetListState(unit shop)
        {           
            InitListByShop(shop);
        }           

        internal void InitListByShop(unit shop)
        {
            HabPropertiesCollection hpcListItems = new HabPropertiesCollection();

            if (shop == null)
                InitList(DHLOOKUP.shops);            
            else
            {
                DBSTRINGCOLLECTION itemList;
                HabPropertiesCollection hpcItemProfiles;                

                if (DHHELPER.IsNewVersionItemShop(shop))
                {
                    itemList = shop.sellunits;
                    hpcItemProfiles = DHLOOKUP.hpcUnitProfiles;
                }
                else
                {
                    itemList = shop.sellitems;
                    hpcItemProfiles = DHLOOKUP.hpcItemProfiles;
                }

                foreach (string itemID in itemList)
                {
                    HabProperties hps = hpcItemProfiles[itemID];
                    hpcListItems.Add(hps);
                }

                InitList(hpcListItems);
            }
        }

        internal void InitList(List<unit> shops)
        {
            this.Width = this.width + 4;

            itemsLV.BeginUpdate();

            // init list

            itemsLV.Items.Clear();
            itemsLV.Groups.Clear();

            foreach (unit s in shops)
            {
                ListViewGroup Group = new ListViewGroup(s.codeID, s.ID);
                Group.Tag = s;

                itemsLV.Groups.Add(Group);

                DBSTRINGCOLLECTION itemList;
                HabPropertiesCollection hpcItemProfiles;

                if (DHHELPER.IsNewVersionItemShop(s))
                {
                    itemList = s.sellunits;
                    hpcItemProfiles = DHLOOKUP.hpcUnitProfiles;
                }
                else
                {
                    itemList = s.sellitems;
                    hpcItemProfiles = DHLOOKUP.hpcItemProfiles;
                }


                foreach (string itemID in itemList)
                {
                    HabProperties hpsItem = hpcItemProfiles[itemID];

                    string iconName = hpsItem.GetStringValue("Art");
                    if (String.IsNullOrEmpty(iconName)) continue;

                    ListViewItem lvi_Item = new ListViewItem();

                    lvi_Item.ImageKey = iconName;
                    lvi_Item.Tag = hpsItem;
                    lvi_Item.Group = Group;

                    itemsLV.Items.Add(lvi_Item);
                }
            }           

            itemsLV.EndUpdate();

            DisplayCaption();
        }
        internal void InitList(HabPropertiesCollection hpcListItems)
        {
            this.Width = this.width;

            itemsLV.BeginUpdate();

            // init list

            itemsLV.Items.Clear();
            itemsLV.Groups.Clear();

            foreach (HabProperties hpsItem in hpcListItems.Values)
            {
                string iconName = hpsItem.GetStringValue("Art");
                if (String.IsNullOrEmpty(iconName)) continue;

                ListViewItem lvi_Item = new ListViewItem();

                lvi_Item.ImageKey = iconName;
                lvi_Item.Tag = hpsItem;

                itemsLV.Items.Add(lvi_Item);
            }

            itemsLV.EndUpdate();

            DisplayCaption();
        }        

        private void itemsLV_ItemActivate(object sender, EventArgs e)
        {
            foreach (int index in itemsLV.SelectedIndices)
            {
                ListViewItem lvItem = itemsLV.Items[index];
                HabProperties hpsItem = lvItem.Tag as HabProperties;

                unit shop = (switchButton as ToolStripButton).Tag as unit;
                if (shop == null) shop = lvItem.Group.Tag as unit;

                OnItemActivate(SellItem(shop,hpsItem));
                break;
            }
        }        

        internal item SellItem(unit shop, HabProperties hpsItem)
        {
            if (hpsItem == null || Current.unit == null) return null;

            // the map script checks if buying unit 
            // is located near the shop, so here we set
            // the hero's location to the selected shop's location
            // & remove other units from shop checking
            foreach (unit unit in Current.player.units.Values)
                if (unit == Current.unit)
                    unit.set_location(shop.get_location());
                else
                    unit.set_location(new location(0,0));

            switch (combineMode)
            {
                case CombineMode.Fast:
                    return SellItemFast(shop, hpsItem);

                default:
                    return SellItemNormal(shop, hpsItem);
            }
        }

        internal item SellItemNormal(unit shop, HabProperties hpsItem)
        {
            string ID = hpsItem.name;
            IRecord item = Items.GetByUnit("codeID", ID);

            bool isNewVersionItem = DHHELPER.IsNewVersionItem(ID);

            if (!(item is item) && !(item is unit))
            {
                item = isNewVersionItem ? (IRecord)new unit(hpsItem.name) : (IRecord)new item(hpsItem.name);
                Items.Add(item);
            }

            item = item.Clone();          

            if (isNewVersionItem)
            {
                (item as unit).DoSummon = true;
                (item as unit).set_owningPlayer(Current.unit.get_owningPlayer()); //!               

                shop.OnSell(item as unit); // sell item as unit
            }
            else
                shop.OnSellItem(item as item, Current.unit);
            
            // pay the gold for this item

            int goldCost = isNewVersionItem ? (item as unit).goldCost : (item as item).goldCost;
            Current.player.Gold = Current.player.Gold - goldCost;            

            return item as item;
        }

        internal item SellItemNormal(unit shop, HabProperties hpsItem, int goldCost)
        {
            string ID = hpsItem.name;
            item item = Items.GetByUnit("codeID", ID) as item;

            if (item == null)
            {
                item = new item(hpsItem.name);
                Items.Add(item);
            }

            item = item.Clone() as item;
            
            shop.OnSellItem(item, Current.unit);

            // pay the gold for this item

            Current.player.Gold = Current.player.Gold - goldCost;            

            return item;
        }

        internal item SellItemFast(unit shop, HabProperties hpsItem)
        {
            List<HabProperties> hpsList = FindSameLookingComplexItem(hpsItem);            

            if (hpsList.Count == 0) return SellItemNormal(shop, hpsItem);
            else
            {
                int goldCost = GetGoldCost(hpsList[0]);
                return SellItemNormal(shop, hpsList[0], goldCost);
            }
        }

        private void OnItemActivate(IRecord e)
        {            
            if (ItemActivate != null)
                ItemActivate(null,e);
        }

        private void ItemListForm_Load(object sender, EventArgs e)
        {
            //this.SetDesktopLocation(Screen.PrimaryScreen.WorkingArea.Width,
                 //                    Screen.PrimaryScreen.WorkingArea.Height); 
        }

        private void ItemListForm_Shown(object sender, EventArgs e)
        {
            //this.Visible = false;            
        }       

        private void itemsLV_MouseMove(object sender, MouseEventArgs e)
        {
            Point cp = itemsLV.PointToClient(MousePosition);
            ListViewItem lvItem = itemsLV.GetItemAt(cp.X, cp.Y);

            if (lvItem == null)
            {
                CloseToolTip();
                return;
            }            

            if (toolTipItem != lvItem.Tag)
            {
                CloseToolTip();
                
                toolTipItem = lvItem.Tag;
                toolTipTimer.Start();
            }

            if (toolTip != null)
                toolTip.DisplayAtCursor(MousePosition);
        }

        private void itemsLV_MouseLeave(object sender, EventArgs e)
        {
            CloseToolTip();           
        }

        private void toolTipTimer_Tick(object sender, EventArgs e)
        {
            toolTipTimer.Stop();
            toolTip = new ItemToolTipForm(this);

            HabProperties hpsItem = toolTipItem as HabProperties;

            switch (combineMode)
            {
                case CombineMode.Fast:
                    List<HabProperties> hpsList = FindSameLookingComplexItem(hpsItem);
                    if (hpsList.Count == 0)
                        toolTip.showItemToolTip(hpsItem, false);
                    else
                        toolTip.showComplexItemToolTip(hpsItem, GetGoldCost(hpsList[0]));
                    break;

                default:
                    toolTip.showItemToolTip(hpsItem, false);
                    break;
            }            
        }

        private void itemsLV_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.Graphics.DrawImage((Image)DHRC.GetImage(e.Item.ImageKey),
                                e.Bounds.X, e.Bounds.Y, 48, 48);
            /*HabProperties hpsX = e.Item.Tag as HabProperties;
            e.Item.Text = RecordSlotComparer.get_slot(hpsX.GetValue("Buttonpos") + "")+"";            
            e.DrawText();*/
        }

        private void all_MouseDown(object sender, MouseEventArgs e)
        {
            SetListState((unit)null);
        }

        public void CollectItemCombiningData()
        {
            if (MorphingItems != null) return;

            sameLookingComplexItems = null;

            Current.player.AcceptMessages = false;

            Dictionary<string, widget> dcItems = new Dictionary<string, widget>();

            foreach (unit shop in DHLOOKUP.shops)
            {
                if (DHHELPER.IsNewVersionItemShop(shop))
                {
                    foreach (string unitID in shop.sellunits)
                        if (!dcItems.ContainsKey(unitID))
                        {
                            unit u = new unit(unitID);
                            u.DoSummon = true;
                            u.set_owningPlayer(Current.player);

                            dcItems.Add(unitID, u);
                        }
                }
                else
                    foreach (string itemID in shop.sellitems)
                        if (!dcItems.ContainsKey(itemID))
                            dcItems.Add(itemID, new item(itemID));
            }

            unit test_unit = new unit();
            test_unit.codeID = "test";
            test_unit.Inventory.init(0,10);            

            test_unit.set_owningPlayer(Current.player);

            DHJassExecutor.CaughtReferences.Clear();
            DHJassExecutor.CatchArrayReference = true;

            DBINVENTORY inventory = test_unit.Inventory;

            List<widget> itemList = new List<widget>(dcItems.Values);
            MorphingItems = new Dictionary<string, string>(itemList.Count);

            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            splashScreen.ShowText("Collecting item combining data...");

            for(int i=0; i< itemList.Count; i++)
            {
                widget item = itemList[i];

                if (DHHELPER.IsNewVersionItem(item.codeID))
                {
                    test_unit.OnSell(item as unit);
                    Thread.Sleep(2); // to pass control to item handling script thread
                }
                else
                {
                    test_unit.OnSellItem(item as item, test_unit);
                    inventory.put_item(item as item);
                }

                item result = inventory[0].Item;                
                if (result != null)
                {   if (result.codeID != item.codeID)
                        MorphingItems.Add(result.codeID, item.codeID);
                    inventory[0].drop_item();
                }

                splashScreen.ShowProgress((double)i, (double)itemList.Count);
            }

            Current.player.remove_unit(test_unit);
            test_unit.destroy();

            List<string> arrays = DHJassExecutor.CaughtReferences;

            if (arrays.Count == 6)
            {
                Dictionary<int,DHJassValue> dcA = (DHJassExecutor.Globals[arrays[0]] as DHJassArray).Array;
                Dictionary<int, DHJassValue> dcB = (DHJassExecutor.Globals[arrays[1]] as DHJassArray).Array;
                Dictionary<int,DHJassValue> dcC = (DHJassExecutor.Globals[arrays[2]] as DHJassArray).Array;
                Dictionary<int,DHJassValue> dcD = (DHJassExecutor.Globals[arrays[3]] as DHJassArray).Array;                

                Dictionary<int,DHJassValue> dcCombined = (DHJassExecutor.Globals[arrays[4]] as DHJassArray).Array;

                ComplexItems = new Dictionary<string, List<List<string>>>(dcA.Count);                
                
                int value;                
                foreach (int key in dcA.Keys)
                {
                    List<string> components = new List<string>(4);

                    value = dcA[key].IntValue;
                    if (value != 0) components.Add(DHJassInt.int2id(value));

                    value = dcB[key].IntValue;
                    if (value != 0) components.Add(DHJassInt.int2id(value));

                    value = dcC[key].IntValue;
                    if (value != 0) components.Add(DHJassInt.int2id(value));

                    value = dcD[key].IntValue;
                    if (value != 0) components.Add(DHJassInt.int2id(value));

                    value = dcCombined[key].IntValue;
                    if (value != 0)
                    {
                        string strValue = DHJassInt.int2id(value);

                        List<List<string>> componentsList;
                        if (!ComplexItems.TryGetValue(strValue, out componentsList))
                        {
                            componentsList = new List<List<string>>(1);
                            ComplexItems.Add(strValue, componentsList);
                        }

                        componentsList.Add(components);
                    }
                }
            }            

            splashScreen.Close();

            Current.player.AcceptMessages = true;
        }

        internal void DisplayCaption()
        {
            string caption = "Items (" + itemsLV.Items.Count + ")";

            switch (combineMode)
            {
                case CombineMode.Fast:
                    caption += " Fast";
                    break;
            }

            captionB.Text = caption;
        }

        protected override void captionButton_MouseDown(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Right)
            {
                if (DHLOOKUP.hpcItemProfiles != null && DHLOOKUP.hpcItemProfiles.Count != 0)
                {
                    captionButton.BackColor = Color.Gray;

                    combineMode++;                    
                    if ((int)combineMode > (int)CombineMode.Fast)
                        combineMode = CombineMode.Default;

                    SetCombineMode(combineMode);
                    WriteConfig();
                }
                else
                    captionButton.BackColor = Color.Red;
            }

            base.captionButton_MouseDown(sender, e);
        }
        protected override void captionButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) captionButton.BackColor = Color.Black;
            base.captionButton_MouseUp(sender, e);
        }

        internal void SetCombineMode(CombineMode combineMode)
        {
            this.combineMode = combineMode;

            switch (combineMode)
            {
                case CombineMode.Fast:
                    CollectItemCombiningData();
                    if (ComplexItems == null)
                    {
                        MessageBox.Show("Fast Item Combining mode is not supported in this version,\n due to different item combining script in the map", UIStrings.Warning);
                        this.combineMode = CombineMode.Default;
                        captionB.BackColor = Color.Black;
                    }
                    break;
            }

            DisplayCaption();            
        }

        internal List<HabProperties> FindSameLookingComplexItem(HabProperties hpsItem)
        {
            if (sameLookingComplexItems == null)            
                sameLookingComplexItems = new Dictionary<string, List<HabProperties>>(ComplexItems.Count);

            string itemArt = hpsItem.GetStringValue("art");

            List<HabProperties> hpsList;
            if (sameLookingComplexItems.TryGetValue(itemArt, out hpsList))            
                return hpsList;                     
            else
                hpsList = new List<HabProperties>();            

            foreach (string codeID in ComplexItems.Keys)
            {
                HabProperties hpsCItem = DHLOOKUP.hpcItemProfiles[codeID];                
                if (hpsCItem != null && hpsCItem.GetStringValue("art") == itemArt
                    && IsItemNameSimilar(hpsItem, hpsCItem))
                    hpsList.Add(hpsCItem);
            }

            sameLookingComplexItems.Add(itemArt, hpsList);
            
            return hpsList;
        }
        internal bool IsItemNameSimilar(HabProperties hpsItemA, HabProperties hpsItemB)
        {
            string[] name_partsA = hpsItemA.GetStringValue("Name").Split(' ');
            string[] name_partsB = hpsItemB.GetStringValue("Name").Split(' ');

            int matches = 0;
            for (int i = 0; i < name_partsA.Length; i++)
                for(int j=0; j < name_partsB.Length; j++)
                    if (name_partsA[i] == name_partsB[j]) matches++;

            return (matches > 0);
        }
        internal int GetGoldCost(HabProperties hpsItem)
        {
            if (ComplexItemCosts == null)            
                ComplexItemCosts = new Dictionary<string, int>(ComplexItems.Count);

            int result = 0;
            if (ComplexItemCosts.TryGetValue(hpsItem.name, out result))
                return result;

            List<List<string>> componentsList;
            if (!ComplexItems.TryGetValue(hpsItem.name, out componentsList))            
                return DHLOOKUP.hpcItemData[hpsItem.name].GetIntValue("goldcost");        

            List<string> components = componentsList[0];            

            foreach (string codeID in components)
            {
                string originalCodeID;
                if (!MorphingItems.TryGetValue(codeID, out originalCodeID))
                    originalCodeID = codeID;

                HabProperties hpsComponent = DHLOOKUP.hpcItemData[originalCodeID];
                result += hpsComponent.GetIntValue("GoldCost");
            }

            ComplexItemCosts.Add(hpsItem.name, result);

            return result;
        }
        internal int GetGoldCost(item item)
        {
            string originalCodeID;

            if (ComplexItems == null)
            {                
                if (MorphingItems.TryGetValue(item.codeID, out originalCodeID))
                {
                    if (DHHELPER.IsNewVersionItem(originalCodeID))
                        return DHMpqDatabase.UnitSlkDatabase["UnitBalance"][originalCodeID].GetIntValue("GoldCost");
                    else
                        return DHLOOKUP.hpcItemData[originalCodeID].GetIntValue("GoldCost");
                }
                else
                    return item.goldCost;
            }

            if (ComplexItemCosts == null)
                ComplexItemCosts = new Dictionary<string, int>(ComplexItems.Count);            

            int result = 0;
            if (ComplexItemCosts.TryGetValue(item.codeID, out result))
                return result;
            
            List<List<string>> componentsList;
            if (!ComplexItems.TryGetValue(item.codeID, out componentsList))
            {
                if (MorphingItems.TryGetValue(item.codeID, out originalCodeID))
                    return DHLOOKUP.hpcItemData[originalCodeID].GetIntValue("GoldCost");
                else
                    return item.goldCost;
            }

            List<string> components = componentsList[0];

            foreach (string codeID in components)
            {                
                if (!MorphingItems.TryGetValue(codeID, out originalCodeID))
                    originalCodeID = codeID;

                HabProperties hpsComponent = DHLOOKUP.hpcItemData[originalCodeID];
                result += hpsComponent.GetIntValue("GoldCost");
            }

            ComplexItemCosts.Add(item.codeID, result);

            return result;
        }        

        public override void WriteConfig()
        {
            DHCFG.Items["Items"]["CombineMode"] = (int)combineMode;
        }
    }
}