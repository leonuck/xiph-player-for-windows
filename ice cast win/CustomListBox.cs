using System;
using System.Drawing;
using System.Windows.Forms;

namespace ice_cast_win
{
    public class CustomListBox : ListBox
    {
        public CustomListBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? Brushes.Blue : Brushes.Black;
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), e.Bounds);
            e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Desenha a barra de rolagem personalizada
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.FromArgb(63, 63, 70), ButtonBorderStyle.Solid);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0xF;

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                var vScroll = this.GetScrollInfo();
                if (vScroll.nMin != vScroll.nMax)
                {
                    var scrollBarBounds = new Rectangle(this.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, this.ClientRectangle.Height);
                    using (var brush = new SolidBrush(Color.FromArgb(63, 63, 70)))
                    {
                        Graphics.FromHwnd(this.Handle).FillRectangle(brush, scrollBarBounds);
                    }
                }
            }
        }

        private ScrollInfo GetScrollInfo()
        {
            const int SB_VERT = 1;
            const int SIF_RANGE = 0x1;
            const int SIF_POS = 0x4;

            ScrollInfo si = new ScrollInfo();
            si.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(si);
            si.fMask = SIF_RANGE | SIF_POS;
            GetScrollInfo(this.Handle, SB_VERT, ref si);

            return si;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct ScrollInfo
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetScrollInfo(IntPtr hWnd, int n, ref ScrollInfo lpScrollInfo);
    }
}