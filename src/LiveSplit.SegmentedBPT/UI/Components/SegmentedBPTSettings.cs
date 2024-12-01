using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Codaxy.Xlio.Model.Oxml;
using LiveSplit.Model;
using LiveSplit.SegmentedBPT;
using LiveSplit.TimeFormatters;

namespace LiveSplit.UI.Components
{
    public partial class SegmentedBPTSettings : UserControl
    {
        public LiveSplitState CurrentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState == value)
                    return;

                _currentState = value;
                OnCurrentStateChange?.Invoke(this, null);
                OnUpdateRun();
            }
        }
        private LiveSplitState _currentState { get; set; }

        // Event used to alert children the current state might have changed.
        public event EventHandler OnCurrentStateChange;

        public IList<SplitsSettings> SplitsSettingsList { get; set; }
        private IList<SplitsSettings> VisibleSplitsSettingsList { get; set; }
        private SplitsSettings ForCurrentSplits {  get; set; }

        public LayoutMode Mode { get; set; }

        public Color TextColor { get; set; }
        public bool OverrideTextColor { get; set; }
        public Color TimeColor { get; set; }
        public bool OverrideTimeColor { get; set; }
        public TimeAccuracy Accuracy { get; set; }

        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }

        public bool BPTFlashingEnabled { get; set; }
        public bool BPTFlashingContinuous { get; set; }
        public int BPTFlashingSegBPTTime { get; set; }
        public int BPTFlashingBPTTime { get; set; }


        public string GradientString
        {
            get { return BackgroundGradient.ToString(); }
            set { BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value); }
        }

        public bool Display2Rows { get; set; }

        protected bool ShowingAll { get; set; }

        public SegmentedBPTSettings()
        {
            InitializeComponent();

            TextColor = Color.FromArgb(255, 255, 255);
            OverrideTextColor = false;
            TimeColor = Color.FromArgb(255, 255, 255);
            OverrideTimeColor = false;
            Accuracy = TimeAccuracy.Seconds;
            BackgroundColor = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;
            Display2Rows = false;

            chkOverrideTextColor.DataBindings.Add("Checked", this, "OverrideTextColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnTextColor.DataBindings.Add("BackColor", this, "TextColor", false, DataSourceUpdateMode.OnPropertyChanged);
            chkOverrideTimeColor.DataBindings.Add("Checked", this, "OverrideTimeColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnTimeColor.DataBindings.Add("BackColor", this, "TimeColor", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbGradientType.SelectedIndexChanged += cmbGradientType_SelectedIndexChanged;
            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false, DataSourceUpdateMode.OnPropertyChanged);

            rdoSeconds.CheckedChanged += rdoSeconds_CheckedChanged;
            rdoHundredths.CheckedChanged += rdoHundredths_CheckedChanged;

            chkOverrideTextColor.CheckedChanged += chkOverrideTextColor_CheckedChanged;
            chkOverrideTimeColor.CheckedChanged += chkOverrideTimeColor_CheckedChanged;

            checkBoxFlashEnabled.DataBindings.Add("Checked", this, "BPTFlashingEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            checkBoxFlashContinuous.DataBindings.Add("Checked", this, "BPTFlashingContinuous", false, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownSegBPTTime.DataBindings.Add("Value", this, "BPTFlashingSegBPTTime", false, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownBPTTime.DataBindings.Add("Value", this, "BPTFlashingBPTTime", false, DataSourceUpdateMode.OnPropertyChanged);

            SplitsSettingsList = new List<SplitsSettings>();
            VisibleSplitsSettingsList = new List<SplitsSettings>();
            ForCurrentSplits = null;
            ShowingAll = false;

            //OnCurrentStateChange += SegmentedBPTSettings_OnCurrentStateChange;
        }

        public void OnUpdateRun()
        {
            ForCurrentSplits = GetCurrentStateSplitsSettings();
        }

        void chkOverrideTimeColor_CheckedChanged(object sender, EventArgs e)
        {
            label2.Enabled = btnTimeColor.Enabled = chkOverrideTimeColor.Checked;
        }

        void chkOverrideTextColor_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = btnTextColor.Enabled = chkOverrideTextColor.Checked;
        }

        void SegmentedBPTSettings_Load(object sender, EventArgs e)
        {
            chkOverrideTextColor_CheckedChanged(null, null);
            chkOverrideTimeColor_CheckedChanged(null, null);

            rdoSeconds.Checked = Accuracy == TimeAccuracy.Seconds;
            rdoTenths.Checked = Accuracy == TimeAccuracy.Tenths;
            rdoHundredths.Checked = Accuracy == TimeAccuracy.Hundredths;

            if (Mode == LayoutMode.Horizontal)
            {
                chkTwoRows.Enabled = false;
                chkTwoRows.DataBindings.Clear();
                chkTwoRows.Checked = true;
            }
            else
            {
                chkTwoRows.Enabled = true;
                chkTwoRows.DataBindings.Clear();
                chkTwoRows.DataBindings.Add("Checked", this, "Display2Rows", false, DataSourceUpdateMode.OnPropertyChanged);
            }

            ResetLayout();
        }

        void cmbGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColor1.Visible = cmbGradientType.SelectedItem.ToString() != "Plain";
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this, btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        void rdoHundredths_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAccuracy();
        }

        void rdoSeconds_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAccuracy();
        }

        void UpdateAccuracy()
        {
            if (rdoSeconds.Checked)
                Accuracy = TimeAccuracy.Seconds;
            else if (rdoTenths.Checked)
                Accuracy = TimeAccuracy.Tenths;
            else
                Accuracy = TimeAccuracy.Hundredths;
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            TextColor = SettingsHelper.ParseColor(element["TextColor"]);
            OverrideTextColor = SettingsHelper.ParseBool(element["OverrideTextColor"]);
            TimeColor = SettingsHelper.ParseColor(element["TimeColor"]);
            OverrideTimeColor = SettingsHelper.ParseBool(element["OverrideTimeColor"]);
            Accuracy = SettingsHelper.ParseEnum<TimeAccuracy>(element["Accuracy"]);
            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"]);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"]);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"]);
            Display2Rows = SettingsHelper.ParseBool(element["Display2Rows"], false);

            BPTFlashingEnabled = SettingsHelper.ParseBool(element["BPTFlashingEnabled"], true);
            BPTFlashingContinuous = SettingsHelper.ParseBool(element["BPTFlashingContinuous"], false);
            BPTFlashingSegBPTTime = SettingsHelper.ParseInt(element["BPTFlashingSegBPTTime"], 2);
            BPTFlashingBPTTime = SettingsHelper.ParseInt(element["BPTFlashingBPTTime"], 2);

            SplitsSettingsList.Clear();

            var splitsSettingsListElement = element["SplitsSettings"];
            if (splitsSettingsListElement != null)
            {
                foreach (var childNode in splitsSettingsListElement.ChildNodes)
                {
                    var splitSettings = SplitsData.FromXml((XmlNode)childNode);
                    MakeSplitsSettings(splitSettings);
                }
            }

            ForCurrentSplits = GetCurrentStateSplitsSettings();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            var hashCode = SettingsHelper.CreateSetting(document, parent, "Version", "1.4") ^
                SettingsHelper.CreateSetting(document, parent, "TextColor", TextColor) ^
                SettingsHelper.CreateSetting(document, parent, "OverrideTextColor", OverrideTextColor) ^
                SettingsHelper.CreateSetting(document, parent, "TimeColor", TimeColor) ^
                SettingsHelper.CreateSetting(document, parent, "OverrideTimeColor", OverrideTimeColor) ^
                SettingsHelper.CreateSetting(document, parent, "Accuracy", Accuracy) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
                SettingsHelper.CreateSetting(document, parent, "Display2Rows", Display2Rows) ^
                SettingsHelper.CreateSetting(document, parent, "BPTFlashingEnabled", BPTFlashingEnabled) ^
                SettingsHelper.CreateSetting(document, parent, "BPTFlashingContinuous", BPTFlashingContinuous) ^
                SettingsHelper.CreateSetting(document, parent, "BPTFlashingSegBPTTime", BPTFlashingSegBPTTime) ^
                SettingsHelper.CreateSetting(document, parent, "BPTFlashingBPTTime", BPTFlashingBPTTime);

            XmlElement splitsSettingsNode = null;
            if (document != null)
            {
                splitsSettingsNode = document.CreateElement("SplitsSettings");
                parent.AppendChild(splitsSettingsNode);
            }

            var count = 1;
            foreach (var splitsData in SplitsSettingsList.Select(x => x.Data))
            {
                XmlElement splitsSettingNode = null;

                if (document != null)
                {
                    splitsSettingNode = document.CreateElement("SplitsSetting");
                    splitsSettingsNode.AppendChild(splitsSettingNode);
                }

                hashCode ^= splitsData.CreateElement(document, splitsSettingNode) * count;
                count++;
            }

            return hashCode;
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }

        private SplitsSettings GetCurrentStateSplitsSettings()
        {
            foreach (var splitsSettings in SplitsSettingsList)
            {
                if (splitsSettings.IsCurrentlyLoaded)
                    return splitsSettings;
            }
            return null;
        }

        private bool CheckHasConfigForSplits()
        {
            return this.GetCurrentStateSplitsSettings() != null;
        }

        private SplitsSettings MakeSplitsSettings(SplitsData splitsData)
        {
            var control = new SplitsSettings(this) { Data = splitsData };

            // Subscribe to all the events for this control.
            control.OnChange += SplitsSettingsControl_OnChange;
            control.MovedDown += SplitsSettingsControl_MovedDown;
            control.MovedUp += SplitsSettingsControl_MovedUp;
            control.Removed += SplitsSettingsControl_Removed;
            control.SizeChanged += SplitsSettingsControl_SizeChanged;

            // Add to the splits settings list.
            SplitsSettingsList.Add(control);

            return control;
        }

        private void btnAddForCurrent_Click(object sender, EventArgs e)
        {
            // First, check that no other splits data was created for this splits name.
            if (this.CheckHasConfigForSplits())
                return;

            // Create the initial data and the control for configuration.
            var splitsName = Utils.GetStateSplitsName(CurrentState);
            var d = new SplitsData() { SplitsName = splitsName };

            var control = MakeSplitsSettings(d);
            ForCurrentSplits = control;

            // Add the row to the layout.
            UpdateLayoutForRow(control, tblSplitsSettings.RowCount);

            // Disable the "Add" button considering it will not be needed anymore.
            btnAddForCurrent.Enabled = false;
        }

        private void SplitsSettingsControl_SizeChanged(object sender, EventArgs e)
        {
            var senderO = (SplitsSettings)sender;

            if (!VisibleSplitsSettingsList.Contains(senderO))
                return;

            HandleSizeChange();
        }

        private void HandleSizeChange()
        {
            var heightOffset = 0;

            int index = 0;
            foreach (var splitsSetting in VisibleSplitsSettingsList)
            {
                var itemClearance = Utils.ClearanceHeightForTableItem(
                    tblSplitsSettings,
                    splitsSetting
                );

                var previousHeight = Utils.SetHeight(
                    tblSplitsSettings.RowStyles[index],
                    itemClearance
                );

                heightOffset += itemClearance - previousHeight;

                index++;
            }

            Utils.IncreaseHeight(tblSplitsSettings, heightOffset);
            Utils.IncreaseHeight(tblSplitsSettingsLayout.RowStyles[1], heightOffset);
            Utils.IncreaseHeight(tblSplitsSettingsLayout, heightOffset);
            Utils.IncreaseHeight(groupBoxSplitsSettings, heightOffset);
            Utils.IncreaseHeight(tableLayoutPanel2, heightOffset);
            Utils.IncreaseHeight(this, heightOffset);
        }

        private void ResetLayout()
        {
            ClearLayout();

            var hasForCurrentSplits = false;

            VisibleSplitsSettingsList.Clear();

            foreach (var control in SplitsSettingsList)
            {
                if (control.IsCurrentlyLoaded || ShowingAll)
                {
                    VisibleSplitsSettingsList.Add(control);
                    hasForCurrentSplits = hasForCurrentSplits || control.IsCurrentlyLoaded;
                } 
            }

            int index = 0;
            foreach (var control in VisibleSplitsSettingsList)
            {
                UpdateLayoutForRow(control, index);
                control.UpdateButtonStates(
                    ShowingAll,
                    index == 0,
                    index == VisibleSplitsSettingsList.Count - 1
                );
                index++;
            }

            // Reset the button states.
            btnShowAll.Text = ShowingAll ? "Hide All" : "Show All";
            btnAddForCurrent.Enabled = !hasForCurrentSplits;
        }

        private void UpdateLayoutForRow(Control control, int index)
        {
            var height = Utils.ClearanceHeightForTableItem(tblSplitsSettings, control);

            // Add a row with the appropriate height.
            var rowStyle = new RowStyle(SizeType.Absolute, height);

            tblSplitsSettings.RowCount++;

            if (index < tblSplitsSettings.RowStyles.Count)
            {
                tblSplitsSettings.RowStyles.Insert(index, rowStyle);
            }
            else
            {
                tblSplitsSettings.RowStyles.Add(rowStyle);
            }

            // Ensure that the overall layout is big enough for the added content.
            Utils.IncreaseHeight(tblSplitsSettings, height);

            Utils.IncreaseHeight(tblSplitsSettingsLayout, height);
            Utils.IncreaseHeight(tblSplitsSettingsLayout.RowStyles[1], height);

            Utils.IncreaseHeight(groupBoxSplitsSettings, height);
            Utils.IncreaseHeight(tableLayoutPanel2, height);
            Utils.IncreaseHeight(this, height);

            // Add the control to the table where it fits.
            tblSplitsSettings.Controls.Add(control, 0, index);
        }

        private void ClearLayout()
        {
            // Clear out all the visible splits settings from the list.
            VisibleSplitsSettingsList.Clear();

            // Reset the table with all the splitSettings in it.
            tblSplitsSettings.RowCount = 0;
            tblSplitsSettings.RowStyles.Clear();
            tblSplitsSettings.Controls.Clear();

            // We now reset the height of the layout based on the empty list.
            var height = Utils.ResetHeight(tblSplitsSettings);

            Utils.DecreaseHeight(tblSplitsSettingsLayout, height);
            Utils.ResetHeight(tblSplitsSettingsLayout.RowStyles[1]);
            Utils.DecreaseHeight(groupBoxSplitsSettings, height);
            Utils.DecreaseHeight(tableLayoutPanel2, height);
            Utils.DecreaseHeight(this, height);
        }

        private void SplitsSettingsControl_Removed(object sender, EventArgs e)
        {
            var selectedSplits = (SplitsSettings)sender;
            SplitsSettingsList.Remove(selectedSplits);
            if (selectedSplits == ForCurrentSplits)
            {
                ForCurrentSplits = null;
            }
            ResetLayout();
        }

        private void SplitsSettingsControl_MovedUp(object sender, EventArgs e)
        {
            var selectedSplits = (SplitsSettings)sender;

            var index = SplitsSettingsList.IndexOf(selectedSplits);
            if (index == -1 || index == 0)
            {
                return;
            }

            SplitsSettingsList.RemoveAt(index);
            SplitsSettingsList.Insert(index-1, selectedSplits);

            ResetLayout();
        }

        private void SplitsSettingsControl_MovedDown(object sender, EventArgs e)
        {
            var selectedSplits = (SplitsSettings)sender;

            var index = SplitsSettingsList.IndexOf(selectedSplits);
            if (index == -1 || index == SplitsSettingsList.Count - 1)
            {
                return;
            }

            var other = SplitsSettingsList[index+1];
            SplitsSettingsList.RemoveAt(index+1);
            SplitsSettingsList.Insert(index, other);

            ResetLayout();
        }

        private void SplitsSettingsControl_OnChange(object sender, SplitsSettingsChangeEventArgs e)
        {
            // HandleSizeChange();
        }

        public SplitsData GetCurrentData()
        {
            if (ForCurrentSplits == null)
                return new SplitsData();

            return ForCurrentSplits.Data;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowingAll = !ShowingAll;
            ResetLayout();
        }
    }
}
