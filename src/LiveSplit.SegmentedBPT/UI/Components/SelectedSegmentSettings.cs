using System;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.SegmentedBPT;

namespace LiveSplit.UI.Components
{
    public partial class SelectedSegmentSettings : UserControl
    {
        public SelectedSegmentData Data { get; set; }

        protected SplitsSettings ParentSettings { get; set; }

        public event EventHandler OnRemove;
        public event EventHandler<SelectedSegmentSettingsChangeEventArgs> OnChange;

        public SelectedSegmentSettings(SplitsSettings parent)
        {
            InitializeComponent();

            ParentSettings = parent;
            ParentSettings.OnCurrentStateChange += parent_OnCurrentStateChange;
        }

        private void parent_OnCurrentStateChange(object sender, EventArgs e)
        {
            ReloadForState();
        }

        private void SelectedSegmentSettings_Load(object sender, EventArgs e)
        {
            ReloadForState();
        }

        private void ReloadForState()
        {
            var title = $"Split #{Data.Index}";

            if (ParentSettings.IsCurrentlyLoaded)
            {
                var safeIdx = Data.GetSafeIdx(ParentSettings.CurrentState);
                var splitName = ParentSettings.CurrentState.Run[safeIdx].Name;

                // NOTE: First case should never happen but will help debug if it does.
                title = safeIdx != Data.Index
                    ? $"Split #{safeIdx} (!={Data.Index}): {splitName}"
                    : $"Split #{safeIdx}: {splitName}";
            }

            groupBoxMain.Text = title;
            txtAlias.Text = Data.Alias;
        }

        public void SelectControl()
        {
            txtAlias.Select();
        }

        private void txtAlias_TextChanged(object sender, EventArgs e)
        {
            var newAlias = txtAlias.Text;

            if (Data.Alias == newAlias)
                return;

            var prevData = Data;
            Data = new SelectedSegmentData(Data) { Alias = newAlias };
            OnChange?.Invoke(this, new SelectedSegmentSettingsChangeEventArgs(prevData, Data));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemove?.Invoke(this, null);
        }
    }

    public class SelectedSegmentSettingsChangeEventArgs : EventArgs
    {
        public SelectedSegmentSettingsChangeEventArgs(SelectedSegmentData prevData, SelectedSegmentData newData)
        {
            PrevData = prevData;
            NewData = newData;
        }

        public SelectedSegmentData PrevData { get; protected set; }
        public SelectedSegmentData NewData { get; protected set; }
    }
}
