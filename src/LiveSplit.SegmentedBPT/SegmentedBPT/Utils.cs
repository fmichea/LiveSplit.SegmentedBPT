using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveSplit.Model;

namespace LiveSplit.SegmentedBPT
{
    internal class Utils
    {
        public static int ClearanceHeightForTableItem(Control table, Control item)
        {
            // FIXME: This works but leaves a bit of whitespace on it, but
            //   not having this cuts off some parts of the item.
            int clearance = 0;

            clearance += item.Height;
            clearance += item.Margin.Top + item.Margin.Bottom;
            clearance += item.Padding.Top + item.Padding.Bottom;
            clearance += table.Margin.Top + table.Margin.Bottom;
            clearance += table.Padding.Top + table.Padding.Bottom;
            return clearance;
        }

        public static int SetHeight(RowStyle style, int height)
        {
            if (style == null)
                return 0;

            var previousValue = style.Height;
            style.Height = height;
            return (int)previousValue;
        }

        public static int SetHeight(Control control, int height)
        {
            if (control == null)
                return 0;

            var previousValue = control.Height;
            control.Height = height;
            return previousValue;
        }

        public static int ResetHeight(RowStyle style)
        {
            return SetHeight(style, 0);
        }

        public static int ResetHeight(Control control)
        {
            return SetHeight(control, 0);
        }
        
        public static int IncreaseHeight(RowStyle style, int height)
        {
            if (style == null)
                return 0;

            var newHeight = style.Height + height;
            if (newHeight < 0 ) newHeight = 0;
            style.Height = newHeight;
            return (int) style.Height;
        }

        public static int IncreaseHeight(Control control, int height)
        {
            if (control == null)
                return 0;

            var newHeight = control.Height + height;
            if (newHeight < 0) newHeight = 0;
            control.Height = newHeight;
            return control.Height;
        }

        public static int DecreaseHeight(RowStyle style, int height)
        {
            return IncreaseHeight(style, -height);
        }

        public static int DecreaseHeight(Control control, int height)
        {
            return IncreaseHeight(control, -height);
        }

        public static string GetStateSplitsName(LiveSplitState state)
        {
            return Path.GetFileNameWithoutExtension(state?.Run.FilePath);
        }

        public static bool IsCurrentSplitsFile(LiveSplitState state, string splitsName)
        {
            return GetStateSplitsName(state) == splitsName;
        }
    }
}
