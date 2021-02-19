using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
        public string Name;

        public FrameRect Rect;

        public Frame()
        {
            this.Name = string.Empty;
            this.Rect = new FrameRect();
        }
        public override string ToString()
        {
            string returnValue = $"( {this.Name} - {this.Rect.ToString()} )";
            return returnValue;
        }
    }

    [Serializable()]
    public class Sequence
    {
        public string SequenceName;

        public int FromFrame;

        public int ToFrame;

        private bool _IsLooping;
        public bool IsLooping
        {
            get; set;
        }

        private bool _IsReversed;
        public bool IsReversed
        {
            get; set;
        }

        private float _FrameDuration;
        public float FrameDuration
        {
            get; set;
        }

        private string _SpriteEffect;
        public string SpriteEffect
        {
            get; set;
        }
        public override string ToString()
        {
            string returnValue = $"( {this.SequenceName},(from: {this.FromFrame}, to: {this.ToFrame}), isLooping: {this.IsLooping}, IsReversed: {this.IsReversed} )";
            return returnValue;
        }
    }

    [Serializable()]
    public class SpriteData
    {
        private string _SpriteSheetName;
        public string SpriteSheetName
        {
            get; set;
        }

        private List<Frame> _Frames;
        public List<Frame> Frames
        {
            get; set;
        }

        private List<Sequence> _Sequences;
        public List<Sequence> Sequences
        {
            get; set;
        }

        public Dictionary<string, Rectangle> GetRegions()
        {
            int frameIndex = 0;
            Dictionary<string, Rectangle> regions = new Dictionary<string, Rectangle>();

            foreach (Frame frame in this.Frames)
            {
                Rectangle rect = new Rectangle(frame.Rect.X, frame.Rect.Y, frame.Rect.Width, frame.Rect.Height);
                regions.Add(this.SpriteSheetName + "_" + frameIndex.ToString(), rect);
                frameIndex++;
            }

            return regions;
        }

        public SpriteData()
        {
            this.SpriteSheetName = string.Empty;
            this.Frames = new List<Frame>();
            this.Sequences = new List<Sequence>();
        }

        public override string ToString()
        {
            string returnValue = $"( {this.SpriteSheetName}, {this.Frames.ToString()}, {this.Sequences.ToString()} )";
            return returnValue;
        }
    }
}
