using System.Collections.Generic;
using System.Xml;
using LiveSplit.UI;
using SharpDX;

namespace LiveSplit.SegmentedBPT
{
    public class SplitsData
    {
        public string SplitsName { get; set; }
        public IList<SelectedSegmentData> SelectedSegments { get; set; }

        public SplitsData()
        {
            SplitsName = "";
            SelectedSegments = new List<SelectedSegmentData>();
        }

        public SplitsData(SplitsData other)
        {
            SplitsName = other.SplitsName;
            SelectedSegments = other.SelectedSegments;
        }

        public static SplitsData FromXml(XmlNode node)
        {
            var element = (XmlElement)node;

            var splitsName = element["SplitsName"].InnerText;
            var selectedSegments = new List<SelectedSegmentData>();

            var selectedSegmentsXML = element["SelectedSegments"];
            if (selectedSegmentsXML != null)
            {
                foreach (var childNode in selectedSegmentsXML.ChildNodes)
                {
                    var selectedSegment = SelectedSegmentData.FromXml((XmlNode)childNode);
                    selectedSegments.Add(selectedSegment);
                }
            }

            return new SplitsData()
            {
                SplitsName = splitsName,
                SelectedSegments = selectedSegments
            };
        }

        public int CreateElement(XmlDocument document, XmlElement parent)
        {
            int hashCode = SettingsHelper.CreateSetting(document, parent, "SplitsName", SplitsName);

            XmlElement selectedSegmentsRoot = null;

            if (document != null)
            {
                selectedSegmentsRoot = document.CreateElement("SelectedSegments");
                parent.AppendChild(selectedSegmentsRoot);
            }

            var count = 1;
            foreach (var selectedSegment in SelectedSegments)
            {
                XmlElement selectedSegmentRoot = null;

                if (document != null)
                {
                    selectedSegmentRoot = document.CreateElement("SelectedSegment");
                    selectedSegmentsRoot.AppendChild(selectedSegmentRoot);
                }

                hashCode ^= selectedSegment.CreateElement(document, selectedSegmentRoot) * count;
                count++;
            }

            return hashCode;
        }

        public SelectedSegmentData GetNextSelectedSegment(int minIndex)
        {
            var result = new SelectedSegmentData();

            foreach (var selectedSegment in SelectedSegments)
            {
                if (minIndex < selectedSegment.Index)
                {
                    result = selectedSegment;
                    break;
                }
            }

            return result;
        }
    }
}
