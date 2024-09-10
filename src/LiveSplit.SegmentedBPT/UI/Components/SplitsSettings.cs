using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.SegmentedBPT;

namespace LiveSplit.UI.Components
{
    public partial class SplitsSettings : UserControl
    {
        public LiveSplitState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; OnCurrentStateChange?.Invoke(this, null); }
        }

        protected LiveSplitState _currentState { get; set ; }

        public bool IsCurrentlyLoaded
        {
            get { return Utils.IsCurrentSplitsFile(CurrentState, Data?.SplitsName); }
        }

        public SplitsData Data { get; set; }

        // Event used to alert children the current state might have changed.
        public event EventHandler OnCurrentStateChange;

        // Events used by the parent to move the item up and down the list.
        public event EventHandler MovedUp;
        public event EventHandler MovedDown;
        public event EventHandler Removed;

        // Event used by the parent to handle changes to the item.
        public event EventHandler<SplitsSettingsChangeEventArgs> OnChange;

        // Private attributes for this class.
        protected List<SelectedSegmentSettings> SelectedSegmentSettingsList;
        protected Label InfoLabel { get; set; }

        protected static int SelectedSegmentsRowIdx = 1;

        protected SegmentedBPTSettings ParentSettings {  get; set; }

        public SplitsSettings(SegmentedBPTSettings parentSettings)
        {
            InitializeComponent();

            CurrentState = parentSettings.CurrentState;

            ParentSettings = parentSettings;
            ParentSettings.OnCurrentStateChange += SegmentedBPTSettings_OnCurrentStateChange;

            SelectedSegmentSettingsList = new List<SelectedSegmentSettings>();
            InfoLabel = labelSelectedSegmentsInfo;
        }

        private void SegmentedBPTSettings_OnCurrentStateChange(object sender, EventArgs e)
        {
            CurrentState = ParentSettings.CurrentState;

            ResetSelectedSegmentSettings();
            ResetDropdownMenu();
        }

        void selectedSegment_OnChange(object sender, SelectedSegmentSettingsChangeEventArgs e)
        {
            var selectedSegment = (SelectedSegmentSettings)sender;

            var selectedSegmentsData = Data.SelectedSegments.
                Select(x => x == e.PrevData ? e.NewData : x).
                ToList();

            var prevData = Data;
            Data = new SplitsData(Data) { SelectedSegments = selectedSegmentsData };

            OnChange?.Invoke(this, new SplitsSettingsChangeEventArgs(prevData, Data));
        }

        void selectedSegment_Removed(object sender, EventArgs e)
        {
            var selectedSegment = (SelectedSegmentSettings)sender;
            var index = SelectedSegmentSettingsList.IndexOf(selectedSegment);

            var removedSelectedSegmentData = selectedSegment.Data;
            var selectedSegmentsData = Data.SelectedSegments.
                Where(x => x != removedSelectedSegmentData).
                ToList();

            var prevData = Data;
            Data = new SplitsData(Data) { SelectedSegments = selectedSegmentsData };

            SelectedSegmentSettingsList.Remove(selectedSegment);

            ResetSelectedSegmentSettings();
            ResetDropdownMenu();

            OnChange?.Invoke(this, new SplitsSettingsChangeEventArgs(prevData, Data));
        }

        private SelectedSegmentSettings MakeSelectedSegmentSettings(SelectedSegmentData data)
        {
            var result = new SelectedSegmentSettings(this) { Data = data };

            // Add to the selected segments in the right position.
            SelectedSegmentSettingsList.Add(result);
            SelectedSegmentSettingsList = SelectedSegmentSettingsList.
                OrderBy(x => x.Data.Index).
                ToList();

            // Subscribe to the relevant events.
            result.OnChange += selectedSegment_OnChange;
            result.OnRemove += selectedSegment_Removed;

            return result;
        }

        private void ResetSelectedSegmentSettings()
        {
            ClearLayout();

            if (Data.SelectedSegments.Count == 0)
            {
                // Add the information label to the very first row of the table.
                UpdateLayoutForRow(InfoLabel, 0);
            }
            else
            {
                var index = 0;
                foreach (var data in Data.SelectedSegments)
                {
                    var control = MakeSelectedSegmentSettings(data);
                    UpdateLayoutForRow(control, index);
                    index++;
                }
            }
        }

        private void ResetDropdownMenu()
        {
            var selectedIndexes = Data.SelectedSegments.Select(x => x.Index).ToList();

            var selectedItem = (SplitsSettingsComboBoxItem)dropDownSplits.SelectedItem;
            var selectedItemIndex = selectedItem?.Index ?? -1;

            dropDownSplits.Items.Clear();
            dropDownSplits.Items.Add(new SplitsSettingsComboBoxItem(-1, 0, "----- Select Split -----"));
            dropDownSplits.Enabled = IsCurrentlyLoaded;

            if (!IsCurrentlyLoaded)
            {
                dropDownSplits.SelectedIndex = 0;
                return;
            }

            var index = -1;
            var finalSelectedIndex = 0;
            var indexWidth = (int)Math.Ceiling(Math.Log10(CurrentState.Run.Count));


            foreach (var segment in CurrentState.Run)
            {
                index++;

                if (index == 0 || selectedIndexes.Contains(index))
                    continue;

                var item = new SplitsSettingsComboBoxItem(
                    index,
                    indexWidth,
                    segment.Name
                );

                if (index == selectedItemIndex)
                    finalSelectedIndex = dropDownSplits.Items.Count;

                dropDownSplits.Items.Add(item);
            }

            dropDownSplits.SelectedIndex = finalSelectedIndex;
        }

        private void ClearLayout()
        {
            // Remove all the controls from the table for selected splits.
            tblSelectedSplits.RowCount = 0;
            tblSelectedSplits.RowStyles.Clear();
            tblSelectedSplits.Controls.Clear();

            // Clear the selected segments settings.
            SelectedSegmentSettingsList.Clear();

            // Reduce selected segments row to 0 size.
            int height = Utils.ResetHeight(tblSelectedSplits);
            
            Utils.DecreaseHeight(tblMainLayout, height);
            Utils.ResetHeight(tblMainLayout.RowStyles[SelectedSegmentsRowIdx]);
            Utils.DecreaseHeight(groupBox1, height);
            Utils.DecreaseHeight(this, height);
        }

        private void UpdateLayoutForRow(Control control, int index)
        {
            var height = Utils.ClearanceHeightForTableItem(tblSelectedSplits, control);

            // Add a row with the appropriate height.
            tblSelectedSplits.RowCount++;
            tblSelectedSplits.RowStyles.Insert(index, new RowStyle(SizeType.Absolute, height));

            // Ensure that the overall layout is big enough for the added content.
            Utils.IncreaseHeight(tblSelectedSplits, height);

            // Ensure that the row for the selected segments is big enough for its contents.
            Utils.IncreaseHeight(tblMainLayout, height);
            Utils.IncreaseHeight(tblMainLayout.RowStyles[SelectedSegmentsRowIdx], height);

            // Expand the overall layout.
            Utils.IncreaseHeight(groupBox1, height);
            Utils.IncreaseHeight(this, height);

            // Add row to layout.
            tblSelectedSplits.Controls.Add(control, 0, index);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MovedDown?.Invoke(this, null);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MovedUp?.Invoke(this, null);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Removed?.Invoke(this, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedItem = (SplitsSettingsComboBoxItem)dropDownSplits.SelectedItem;
            var selectedSegmentData = new SelectedSegmentData() { Index = selectedItem.Index };

            var selectedSegmentsData = new List<SelectedSegmentData>(Data.SelectedSegments);
            selectedSegmentsData.Add(selectedSegmentData);
            selectedSegmentsData = selectedSegmentsData.OrderBy(x => x.Index).ToList();

            var prevData = Data;
            Data = new SplitsData(Data) { SelectedSegments = selectedSegmentsData };

            var selectedSegment = MakeSelectedSegmentSettings(selectedSegmentData);

            ResetSelectedSegmentSettings();
            ResetDropdownMenu();

            OnChange?.Invoke(this, new SplitsSettingsChangeEventArgs(prevData, Data));
        }

        private void SplitsSettings_Load(object sender, EventArgs e)
        {
            txtName.Text = Data != null ? Data?.SplitsName : "[unknown]";
            groupBox1.Text = txtName.Text;

            ResetSelectedSegmentSettings();
            ResetDropdownMenu();
        }

        private void dropDownSplits_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = dropDownSplits.SelectedIndex != 0;
        }

        public void UpdateButtonStates(bool showingAll, bool isFirst, bool isLast)
        {
            btnMoveDown.Visible = showingAll;
            btnMoveDown.Enabled = !isLast;

            btnMoveUp.Visible = showingAll;
            btnMoveUp.Enabled = !isFirst;
        }
    }

    public class SplitsSettingsChangeEventArgs : EventArgs
    {
        public SplitsSettingsChangeEventArgs(SplitsData prevData, SplitsData newData)
        {
            PrevData = prevData;
            NewData = newData;
        }

        public SplitsData PrevData { get; protected set; }
        public SplitsData NewData { get; protected set; }
    }

    public class SplitsSettingsComboBoxItem
    {
        public int Index { get; protected set; }

        public int IndexWidth { get; protected set; }

        public string SegmentName { get; protected set; }

        public SplitsSettingsComboBoxItem(int index, int indexWidth, string segmentName)
        {
            Index = index;
            IndexWidth = indexWidth;
            SegmentName = segmentName;
        }

        public override string ToString()
        {
            if (Index == -1)
                return SegmentName;

            return string.Format(
                "#{0}: {1}", 
                Index.ToString().PadLeft(IndexWidth, '0'),
                SegmentName
            );
        }
    }
}
