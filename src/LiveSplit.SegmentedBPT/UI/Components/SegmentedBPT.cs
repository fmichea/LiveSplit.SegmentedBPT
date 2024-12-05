using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.SegmentedBPT;
using LiveSplit.TimeFormatters;

namespace LiveSplit.UI.Components
{
    public class SegmentedBPT : IComponent
    {
        public SegmentedBPTSettings Settings { get; set; }
        protected InfoTimeComponent InternalComponent { get; set; }
        private SplitTimeFormatter Formatter { get; set; }

        public float PaddingTop => InternalComponent.PaddingTop;
        public float PaddingLeft => InternalComponent.PaddingLeft;
        public float PaddingBottom => InternalComponent.PaddingBottom;
        public float PaddingRight => InternalComponent.PaddingRight;

        public float VerticalHeight => InternalComponent.VerticalHeight;
        public float MinimumWidth => InternalComponent.MinimumWidth;
        public float HorizontalWidth => InternalComponent.HorizontalWidth;
        public float MinimumHeight => InternalComponent.MinimumHeight;

        public IDictionary<string, Action> ContextMenuControls => null;

        public string ComponentName => "Segmented BPT";

        protected LiveSplitState CurrentState { get; set; }
        protected IRun PreviousRun { get; set; }

        public SegmentedBPT(LiveSplitState state)
        {
            Settings = new SegmentedBPTSettings()
            {
                CurrentState = state
            };

            Formatter = new SplitTimeFormatter(Settings.Accuracy);
            InternalComponent = new InfoTimeComponent(null, null, Formatter);
        }

        private void PrepareDraw(LiveSplitState state)
        {
            // If state has changed, we need to propagate to the settings objects,
            // because they provide information based on the loaded splits.
            if (state != CurrentState)
            {
                if (CurrentState != null)
                {
                    CurrentState.OnSplit -= State_OnSplit;
                    CurrentState.OnStart -= State_OnSplit;
                }


                Settings.CurrentState = state;
                CurrentState = state;

                if (CurrentState != null)
                {
                    CurrentState.OnSplit += State_OnSplit;
                    CurrentState.OnStart += State_OnSplit;
                }
            }

            if (PreviousRun != state.Run)
            {

                Settings.OnUpdateRun();
                PreviousRun = state.Run;
            }

            // Copy the relevant settings for display to the internal component.
            InternalComponent.DisplayTwoRows = Settings.Display2Rows;

            InternalComponent.NameLabel.HasShadow
                = InternalComponent.ValueLabel.HasShadow
                = state.LayoutSettings.DropShadows;

            Formatter.Accuracy = Settings.Accuracy;

            InternalComponent.NameLabel.ForeColor = Settings.OverrideTextColor ? Settings.TextColor : state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor = Settings.OverrideTimeColor ? Settings.TimeColor : state.LayoutSettings.TextColor;
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            LastSplitTime = CurrentState.CurrentTime.RealTime;
        }

        private void DrawBackground(Graphics g, LiveSplitState state, float width, float height)
        {
            if (Settings.BackgroundColor.A > 0
                || Settings.BackgroundGradient != GradientType.Plain
                && Settings.BackgroundColor2.A > 0)
            {
                var gradientBrush = new LinearGradientBrush(
                            new PointF(0, 0),
                            Settings.BackgroundGradient == GradientType.Horizontal
                            ? new PointF(width, 0)
                            : new PointF(0, height),
                            Settings.BackgroundColor,
                            Settings.BackgroundGradient == GradientType.Plain
                            ? Settings.BackgroundColor
                            : Settings.BackgroundColor2);
                g.FillRectangle(gradientBrush, 0, 0, width, height);
            }
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            DrawBackground(g, state, width, VerticalHeight);
            PrepareDraw(state);
            InternalComponent.DrawVertical(g, state, width, clipRegion);
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            DrawBackground(g, state, HorizontalWidth, height);
            PrepareDraw(state);
            InternalComponent.DrawHorizontal(g, state, height, clipRegion);
        }

        private TimeSpan? LastSplitTime = null;

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            const string comparison = BestSegmentsComparisonGenerator.ComparisonName;

            var selectedSegment = new SelectedSegmentData();

            var currentTime = state.CurrentTime.RealTime;
            var totalCycleLength = Settings.BPTFlashingSegBPTTime + Settings.BPTFlashingBPTTime;

            double sinceLastSplit = -1;
            if (Settings.BPTFlashingEnabled && currentTime != null && LastSplitTime != null)
            {
                sinceLastSplit = currentTime.Value.Subtract(LastSplitTime.Value).TotalSeconds;
                if (Settings.BPTFlashingMode == BPTFlashingModeEnum.ContinuousFlashing)
                {
                    sinceLastSplit %= totalCycleLength;
                }
            }


            bool shouldShowDefault = (
                sinceLastSplit == -1 ||
                (
                    sinceLastSplit < Settings.BPTFlashingSegBPTTime
                    || totalCycleLength < sinceLastSplit
                )
            );

            if (
                (shouldShowDefault && Settings.BPTFlashingMode != BPTFlashingModeEnum.DefaultBPT) ||
                (!shouldShowDefault && Settings.BPTFlashingMode == BPTFlashingModeEnum.DefaultBPT)
            ) {
                selectedSegment = Settings.
                    GetCurrentData().
                    GetNextSelectedSegment(state.CurrentSplitIndex);
            }

            var isLast = selectedSegment.IsLast();

            var nextSplit = state.Run[state.Run.Count - 1];
            var splitName = "";

            if (!isLast)
            {
                nextSplit = state.Run[selectedSegment.Index - 1];
                splitName = state.Run[selectedSegment.Index].Name;
            }

            var titles = _generateTexts(splitName, selectedSegment);

            InternalComponent.InformationName = InternalComponent.LongestString = titles[0];
            InternalComponent.AlternateNameText = titles.Skip(1).ToArray();

            switch (state.CurrentPhase)
            {
                case TimerPhase.Running:
                case TimerPhase.Paused:
                {
                    TimeSpan? delta = LiveSplitStateHelper.GetLastDelta(
                        state,
                        state.CurrentSplitIndex,
                        comparison,
                        state.CurrentTimingMethod) ?? TimeSpan.Zero;

                    var liveDelta = state.CurrentTime[state.CurrentTimingMethod] - state.CurrentSplit.Comparisons[comparison][state.CurrentTimingMethod];
                    if (liveDelta > delta)
                        delta = liveDelta;

                    InternalComponent.TimeValue = delta + nextSplit.Comparisons[comparison][state.CurrentTimingMethod];
                    break;
                }

                case TimerPhase.Ended:
                    InternalComponent.TimeValue = state.Run.Last().SplitTime[state.CurrentTimingMethod];
                    break;

                case TimerPhase.NotRunning:
                default:
                    InternalComponent.TimeValue = nextSplit.Comparisons[comparison][state.CurrentTimingMethod];
                    break;
            }

            InternalComponent.Update(invalidator, state, width, height, mode);
        }

        protected string[] _generateTexts(string splitName, SelectedSegmentData selectedSegment)
        {
            if (selectedSegment.IsLast())
            {
                return new[]
                {
                    "Best Possible Time",
                    "Best Poss. Time",
                    "BPT",
                };
            }

            if (selectedSegment.Alias != "")
            {
                if (selectedSegment.FullAlias)
                    return new[]{ selectedSegment.Alias };

                return _generateTextsFromName(selectedSegment.Alias);
            }

            return _generateTextsFromName(splitName);
        }

        protected string[] _generateTextsFromName(string splitName)
        {
            return new[]
            {
                $"Best Possible Time to {splitName}",
                $"Best Poss. Time to {splitName}",
                $"BPT to {splitName}",
            };
        }

        public void Dispose()
        {
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }
    }
}
