using System;
using System.Collections.Generic;

namespace SpriteData
{
    [Serializable()]
    public class FrameRect
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

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

        public FrameRect Rect;

        public Frame()
        {
            this.Duration = 0f;
            this.Rect = new FrameRect();
        }
        public override string ToString()
        {
            string returnValue = $"( {this.Duration.ToString()}, { this.Rect.ToString()} )";
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

        private List<Frame> _Frames;
        public List<Frame> Frames
        {
            get; set;
        }

        public Sequence()
        {
            this.Name = string.Empty;
            this.Frames = new List<Frame>();
        }
        public override string ToString()
        {
            string returnValue = $"( {this.Name}, {this.Frames.ToString()} )";
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
            this.SequenceList = new List<Sequence>
            {
                new Sequence()
            };
        }

        public override string ToString()
        {
            return this.SequenceList.ToString();
        }
    }

    [Serializable()]
    public class SpriteData
    {
        private string _FileName;
        public string FileName
        {
            get; set;
        }

        private string _InitSequence;
        public string InitSequence
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
