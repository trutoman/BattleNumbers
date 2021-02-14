using BattleNumbers.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteData;

namespace BattleNumbers.ECSComponents
{
    class SpriteComponent : ECSComponent
    {
        private Texture2D MainTexture { get; set; }
        private SpriteData.SpriteData Data { get; set; }       
        private SpriteData.Sequence CurrentSequence { get; set; }
        private int CurrentFrame { get; set; }
        public int accumulatedTime { get; set; }
        public int FramesPerSecond { get; set; }
        public bool IsVisible { get; set; }
        public bool IsPaused { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }
        public float Depth { get; set; }
        public float Alpha { get; set; }

        public void Play(string sequence)
        {
            this.CurrentSequence = GetSequence(sequence);
            this.CurrentFrame = 0;
            this.IsPaused = false;
        }

        public void Pause()
        {
            this.IsPaused = true;
        }

        public void Stop()
        {
            this.Pause();
            this.Rewind();
        }

        public void Rewind()
        {
            this.CurrentFrame = 0;
        }

        private Sequence GetSequence(string sequenceName)
        {
            foreach (Sequence currentSeq in this.Data.Sequences.SequenceList)
            {
                if (currentSeq.Name == sequenceName)
                {
                    return currentSeq;
                }
            }
            return new Sequence();
        }

        public SpriteComponent(Texture2D texture, SpriteData.SpriteData data)
        {
            this.MainTexture = texture;
            this.Data = data;
            this.CurrentSequence = GetSequence(this.Data.InitSequence);
            this.FramesPerSecond = this.CurrentSequence.FramesPerSecond;
            this.accumulatedTime = 0;
            this.CurrentFrame = 0;
            this.IsVisible = true;
            this.IsPaused = false;
            this.Color = Color.White;
            this.Effects = SpriteEffects.None;
            this.Depth = 0f;
            this.Alpha = 1.0f;
        }
    }
}
