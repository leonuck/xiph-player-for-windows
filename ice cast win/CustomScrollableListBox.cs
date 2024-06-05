using System;
using System.Drawing;
using System.Windows.Forms;

namespace ice_cast_win
{
    public class CustomScrollableListBox : UserControl
    {
        private Panel panel = new Panel();
        private VScrollBar vScrollBar = new VScrollBar();
        private ListBox listBox = new ListBox();

        public CustomScrollableListBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panel.Dock = DockStyle.Fill;
            this.panel.BackColor = Color.FromArgb(30, 30, 30);
            this.panel.Controls.Add(this.listBox);
            this.panel.Controls.Add(this.vScrollBar);

            this.vScrollBar.Dock = DockStyle.Right;
            this.vScrollBar.Width = 20;
            this.vScrollBar.BackColor = Color.FromArgb(45, 45, 48);
            this.vScrollBar.ForeColor = Color.White;

            this.listBox.Dock = DockStyle.Fill;
            this.listBox.BackColor = Color.FromArgb(30, 30, 30);
            this.listBox.ForeColor = Color.White;
            this.listBox.BorderStyle = BorderStyle.None;
            this.listBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBox.DrawItem += new DrawItemEventHandler(ListBox_DrawItem);

            this.Controls.Add(this.panel);
            this.SizeChanged += new EventHandler(CustomScrollableListBox_SizeChanged);
            this.vScrollBar.Scroll += new ScrollEventHandler(VScrollBar_Scroll);
        }

        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            e.Graphics.FillRectangle(new SolidBrush(isSelected ? Color.FromArgb(0, 122, 204) : Color.FromArgb(30, 30, 30)), e.Bounds);

            string text = ((ListBox)sender).Items[e.Index].ToString();
            e.Graphics.DrawString(text, e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        private void CustomScrollableListBox_SizeChanged(object? sender, EventArgs e)
        {
            AdjustScrollBar();
        }

        private void VScrollBar_Scroll(object? sender, ScrollEventArgs e)
        {
            this.listBox.TopIndex = e.NewValue;
        }

        private void AdjustScrollBar()
        {
            int itemsHeight = this.listBox.ItemHeight * this.listBox.Items.Count;
            bool isVisible = itemsHeight > this.listBox.ClientSize.Height;

            this.vScrollBar.Visible = isVisible;

            if (isVisible)
            {
                this.vScrollBar.Minimum = 0;
                this.vScrollBar.Maximum = this.listBox.Items.Count - 1;
                this.vScrollBar.LargeChange = this.listBox.ClientSize.Height / this.listBox.ItemHeight;
                this.vScrollBar.SmallChange = 1;
            }
        }

        public ListBox.ObjectCollection Items => this.listBox.Items;

        public int SelectedIndex
        {
            get => this.listBox.SelectedIndex;
            set => this.listBox.SelectedIndex = value;
        }

        public object SelectedItem
        {
            get => this.listBox.SelectedItem;
            set => this.listBox.SelectedItem = value;
        }

        public event EventHandler SelectedIndexChanged
        {
            add => this.listBox.SelectedIndexChanged += value;
            remove => this.listBox.SelectedIndexChanged -= value;
        }
    }
}