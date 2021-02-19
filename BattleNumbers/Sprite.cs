using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteData;
using System;

namespace BattleNumbers
{
    public class Sprite : IDisposable, IUpdateable, IDrawable
    {
        private Texture2D texture;
        private SpriteBatch batch;
        private Sequence currentSequence;
        private Frame currentFrame;
        private int currentIndex = 0;
        private double accumulatedTime = 0f;
        private const int milisecPerSec = 1000;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        private bool _Visible;
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        private int _DrawOrder;
        public int DrawOrder
        {
            get { return _DrawOrder; }
            set { _DrawOrder = value; }
        }

        private int _UpdateOrder;
        public int UpdateOrder
        {
            get { return _UpdateOrder; }
            set { _UpdateOrder = value; }
        }

        private SpriteData.SpriteData _Data;
        public SpriteData.SpriteData Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private Vector2 _CurrentLocation;
        public Vector2 CurrentLocation
        {
            get { return _CurrentLocation; }
            set { _CurrentLocation = value; }
        }

        private bool _isPaused;
        public bool isPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        public Sprite(Texture2D texture, SpriteData.SpriteData data, SpriteBatch batch)
        {
            this.texture = texture;
            this.CurrentLocation = new Vector2(0, 0);
            this.Data = data;
            this.batch = batch;
            this.accumulatedTime = 0f;
            this.DrawOrder = 0;
            this.Enabled = true;
            this.Visible = true;
            this.currentIndex = 0;
            this.isPaused = false;
            //this.currentSequence = GetSequence(this.Data.InitSequence);            
            //this.currentFrame = this.currentSequence.Frames[this.currentIndex];
        }

        //private Sequence GetSequence(string sequenceName)
        //{
        //    foreach (Sequence currentSeq in this.Data.Sequences.SequenceList)
        //    {
        //        if (currentSeq.Name == sequenceName)
        //        {
        //            return currentSeq;
        //        }
        //    }
        //    return new Sequence();
        //}

        public void Pause(bool value)
        {
            this.isPaused = value;
        }

        public void FramesPerSecond(int speed)
        {
            float frameDuration = (1f / (float)speed) * (float)Sprite.milisecPerSec;
            //this.FramesPerSecond = frameDuration;
        }

        public void Update(GameTime gameTime)
        {
            //if (this.Enabled)
            //{
            //    if (!this.isPaused)
            //    {
            //        this.accumulatedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            //        if (this.accumulatedTime >= this.currentFrame.Duration)
            //        {
            //            this.accumulatedTime = 0;
            //            this.currentIndex++;
            //            if (this.currentIndex > this.currentSequence.Frames.Count - 1)
            //            {
            //                this.currentIndex = 0;
            //            }
            //            this.currentFrame = this.currentSequence.Frames[this.currentIndex];
            //        }
            //    }
            //}
        }
        public void Draw(GameTime gameTime)
        {
            Rectangle sourceRectangle = new Rectangle(
                this.currentFrame.Rect.X,
                this.currentFrame.Rect.Y,
                this.currentFrame.Rect.Width,
                this.currentFrame.Rect.Height);

            Rectangle destinationRectangle = new Rectangle(
                (int)this.CurrentLocation.X,
                (int)this.CurrentLocation.X,
                this.currentFrame.Rect.Width,
                this.currentFrame.Rect.Height);

            this.batch.Begin();

            this.batch.Draw(this.texture, destinationRectangle, sourceRectangle, Color.White);

            this.batch.End();
        }

        public void Draw(Color color, float rotation = 0f, float scale = 1.0f, SpriteEffects effects = SpriteEffects.None)
        {
            Rectangle sourceRectangle = new Rectangle(
                this.currentFrame.Rect.X,
                this.currentFrame.Rect.Y,
                this.currentFrame.Rect.Width,
                this.currentFrame.Rect.Height);

            this.batch.Begin();

            batch.Draw(
                texture,
                this.CurrentLocation,
                sourceRectangle,
                Color.White,
                rotation,
                this.CurrentLocation,
                scale,
                effects,
                this.DrawOrder);

            this.batch.End();
        }
        public void Dispose()
        {
            this.texture.Dispose();
        }
    }
}
