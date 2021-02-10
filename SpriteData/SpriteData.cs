using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteData
{
    [Serializable()]
    public class FrameRect
    {
        private int _X;
        public int X
        {
            get; set;
        }

        private int _Y;
        public int Y
        {
            get; set;
        }

        private int _Width;
        public int Width
        {
            get; set;
        }

        private int _Height;
        public int Height
        {
            get; set;
        }

        public FrameRect()
        {
            this.X = 0;
            this.Y = 0;
            this.Width = 0;
            this.Height = 0;
        }
        public override string ToString()
        {
            string returnValue = $"( {this.X.ToString()},  {this.Y.ToString()}, {this.Width.ToString()}, {this.Height.ToString()} )";
            return returnValue;
        }
    }

    [Serializable()]
    public class Frame
    {
        private float _Duration;
        public float Duration
        {
            get; set;
        }

        private int _Ordinal;
        public int Ordinal
        {
            get; set;
        }

        private FrameRect _Rect;
        public FrameRect Rect
        {
            get; set;
        }

        public Frame()
        {
            this.Duration = 0f;
            this.Ordinal = 0;
            this.Rect = new FrameRect();
        }
        public override string ToString()
        {
            string returnValue = $"( {this.Duration.ToString()}, {this.Ordinal.ToString()}, { this.Rect.ToString()} )";
            return returnValue;
        }
    }

    [Serializable()]
    public class Sequence
    {
        private string _Name;
        public string Name
        {
            get; set;
        }

        private int _Count;
        public int Count
        {
            get; set;
        }

        private Frame _Frame;
        public Frame Frame
        {
            get; set;
        }

        public Sequence()
        {
            this.Name = string.Empty;
            this.Count = 0;
            this.Frame = new Frame();
        }
        public override string ToString()
        {
            string returnValue = $"( {this.Name}, {this.Count.ToString()}, {this.Frame.ToString()} )";
            return returnValue;
        }
    }

    [Serializable()]
    public class Sequences
    {
        private List<Sequence> _SequenceList;
        public List<Sequence> SequenceList
        {
            get; set;
        }

        public Sequences()
        {
            this.SequenceList = new List<Sequence>();
        }

        public override string ToString()
        {
            return this.SequenceList.ToString();
        }
    }

    [Serializable()]
    class SpriteData
    {
        private string _FileName;
        public string FileName
        {
            get; set;
        }

        private Sequences _Sequences;
        public Sequences Sequences
        {
            get; set;
        }

        public SpriteData()
        {
            this.FileName = string.Empty;
            this.Sequences = new Sequences();
        }
        public override string ToString()
        {
            string returnValue = $"( {this.FileName}, {this.Sequences.ToString()} )";
            return returnValue;
        }
    }
}
