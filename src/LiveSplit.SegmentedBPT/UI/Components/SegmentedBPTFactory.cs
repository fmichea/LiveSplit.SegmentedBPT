using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(SegmentedBPTFactory))]

namespace LiveSplit.UI.Components
{
    public class SegmentedBPTFactory : IComponentFactory
    {
        public string ComponentName => "Segmented BPT";

        public string Description => "Displays the predicted best possible time for an upcoming split.";

        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new SegmentedBPT(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "http://livesplit.org/update/Components/noupdates.xml";

        public string UpdateURL => "http://livesplit.org/update/";

        public Version Version => Version.Parse("0.0.1");
    }
}
