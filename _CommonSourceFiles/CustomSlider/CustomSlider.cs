using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsan
{
    public partial class CustomSlider : UserControl
    {
        public CustomSlider()
        {
            InitializeComponent();
            this.KeyUp += this_KeyUp;
            this.Resize += this_Resize;
            
            this.MouseEnter += this_MouseEnter;
            this.MouseMove += this_MouseMove;
            this.MouseWheel += this_MouseWheel;
            this.MouseDown += this_MouseDown;
            
            // theese can use same as above, 
            // because the mouse position don't matter
            pnlBar.MouseEnter += this_MouseEnter;
            pnlBar.MouseWheel += this_MouseWheel;
            pnlBar.Click += pnlBar_Click;
            
            // theese needs seperate handlers
            // because the mouse position is inside pnlBar
            pnlBar.MouseMove += pnlBar_MouseMove;
            pnlBar.MouseDown += pnlBar_MouseDown;
            pnlBar.MouseUp += pnlBar_MouseUp;
        }

        GraphicsPath GrPath
        {
            get
            {
                int amount = 10;
                GraphicsPath grPath = new GraphicsPath();
                grPath.AddArc(0,0, amount, amount, 180, 90);      
                grPath.AddArc(this.Width-amount, 0, amount, amount, 270, 90);
                grPath.AddArc(this.Width-amount, this.Height-amount, amount, amount, 0, 90);
                grPath.AddArc(0, this.Height-amount, amount, amount, 90, 90);
                //grPath.AddEllipse(this.ClientRectangle);
                return grPath;
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // set the region property to the desired path like this
            this.Region = new System.Drawing.Region(GrPath);
    
            // other drawing goes here
            //e.Graphics.FillEllipse(new SolidBrush(ForeColor), ClientRectangle);
        }
        

        public delegate void SliderMovedEventHandler(object tag, int value);
        public event SliderMovedEventHandler SliderMoved = null;

        public delegate void ValueChangedEventHandler(object tag, int value);
        public event ValueChangedEventHandler ValueChanged = null;

        public Action<string> DebugMessageDelegate;

        private void DebugMessage(string message)
        {
            if (DebugMessageDelegate != null)
                DebugMessageDelegate(message);
        }

        private int _valueMax = 100;
        private int _valueMin = 0;
        private int _value;

        private int _valueSteps = 15;

        public int ValueSteps
        {
            get { return _valueSteps; }

            set { _valueSteps = value; }
        }

        public int ValueStepSize
        {
            get { return ValueRange / _valueSteps; }
        }

        public int ValueRange
        {
            get { return (_valueMax - _valueMin); }
        }

        [Description("The max value"), Category("Data")]
        public int ValueMax
        {
            get { return _valueMax; }
            set
            {
                _valueMax = value;
                Value = _value;
            }
        }
        [Description("the min value"), Category("Data")]
        public int ValueMin
        {
            get { return _valueMin; }
            set
            {
                _valueMin = value;
                Value = _value;
            }
        }

        [Description("Current value"), Category("Data")]
        public int Value
        {
            get
            {
                
                return _value;
            }
            set
            {
                _value = value;
                if (_value < _valueMin) _value = _valueMin;
                else if (_value > _valueMax) _value = _valueMax;
                SetBarFromValue();
                if (ValueChanged != null) ValueChanged(this.Tag, _value);
            }
        }

        [Description("color of slider"), Category("Data")]
        public Color SliderBarColor
        {
            get { return pnlBar.BackColor; }
            set { pnlBar.BackColor = value; }
        }

        private void SetBarFromValue()
        {
            SetBarByPosition(this.Height - ((_value - _valueMin) * this.Height) / ValueRange);
        }

        private void SetBarByPosition(int rootPos)
        {
            if (rootPos < 0) rootPos = 0;
            else if (rootPos >= this.Height) rootPos = this.Height;

            pnlBar.Height = this.Height - rootPos;
            pnlBar.Top = rootPos;
        }


        private void SliderUserMove(int rootPos)
        {
            SetBarByPosition(rootPos);
            // calculate value from slider GUI position
            _value = _valueMin + (ValueRange * (this.Height - pnlBar.Top)) / this.Height;

            if (ValueChanged != null) ValueChanged(this.Tag, _value);
            SliderMoved(this.Tag, _value);
        }

        private void pnlBar_Click(object sender, EventArgs e)
        {
            //((Control)this).OnClick(e);
        }
        private void pnlBar_MouseMove(object sender, MouseEventArgs e) {
            this.OnMouseMove(e.GetNewWithOffset(pnlBar.Location));
        }
        private void pnlBar_MouseDown(object sender, MouseEventArgs e) {
            this.OnMouseDown(e.GetNewWithOffset(pnlBar.Location));
        }
        private void pnlBar_MouseUp(object sender, MouseEventArgs e) {
            this.OnMouseUp(e.GetNewWithOffset(pnlBar.Location));
        }

        private void this_MouseDown(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Button != MouseButtons.Left) return;

            SliderUserMove(e.Y);
        }

        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Button != MouseButtons.Left) return;

            SliderUserMove(e.Y);
        }

        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Delta == 0) return;

            MouseScroll_ChangeValue(e.Delta);
            SliderMoved(this.Tag, Value);
        }

        private void this_Resize(object sender, EventArgs e)
        {
            SetBarFromValue();
        }

        private void MouseScroll_ChangeValue(int delta)
        {
            if (delta < 0) delta = -1;
            else delta = 1;
            Value = _value + delta * ValueStepSize;
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only

            if (e.KeyCode == Keys.Up)
                Value = _value + ValueStepSize;
            else if (e.KeyCode == Keys.Down)
                Value = _value - ValueStepSize;
        }

        private void this_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
        
        
    }
    public static class MouseEventExt
    {
        public static MouseEventArgs GetNewWithOffset(this MouseEventArgs e, int xOffset, int yOffset)
        {
            return new MouseEventArgs(e.Button, e.Clicks, e.X + xOffset, e.Y + yOffset, e.Delta);
        }
        public static MouseEventArgs GetNewWithOffset(this MouseEventArgs e, Point offset)
        {
            return new MouseEventArgs(e.Button, e.Clicks, e.X + offset.X, e.Y + offset.Y, e.Delta);
        }
    }
}
