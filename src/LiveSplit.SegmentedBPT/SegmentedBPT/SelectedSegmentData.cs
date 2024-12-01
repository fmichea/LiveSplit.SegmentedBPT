using System;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LiveSplit.SegmentedBPT
{
    public class SelectedSegmentData
    {
        public int Index { get; set; }
        public string Alias { get; set; }
        public bool FullAlias {  get; set; }

        public SelectedSegmentData()
        {
            Index = -1;
            Alias = "";
            FullAlias = false;
        }

        public SelectedSegmentData(SelectedSegmentData other)
        {
            Index = other.Index;
            Alias = other.Alias;
            FullAlias = other.FullAlias;
        }

        public static SelectedSegmentData FromXml(XmlNode node)
        {
            var element = (XmlElement)node;

            var index = int.Parse(element["Index"].InnerText);
            var alias = element["Alias"].InnerText;
            var fullAlias = SettingsHelper.ParseBool(element["FullAlias"], false);
            return new SelectedSegmentData() { Index = index, Alias = alias, FullAlias = fullAlias };
        }

        public int CreateElement(XmlDocument document, XmlElement parent)
        {
            return
                SettingsHelper.CreateSetting(document, parent, "Index", Index) ^
                SettingsHelper.CreateSetting(document, parent, "Alias", Alias) ^
                SettingsHelper.CreateSetting(document, parent, "FullAlias", FullAlias);
        }

        public int GetSafeIdx(LiveSplitState state)
        {
            var safeIdx = Index;

            safeIdx = Math.Min(safeIdx, state.Run.Count - 1);
            safeIdx = Math.Max(safeIdx, 0);

            return safeIdx;
        }

        public bool IsLast()
        {
            return Index == -1;
        }
    }
}
