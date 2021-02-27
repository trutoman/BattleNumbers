﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents.Sprite
{
    public class SpriteSheetAnimation
    {
        public const float DefaultFrameDuration = 0.2f;

        public string Name { get; }
        public TextureRegion2D[] Frames { get; }

        public float FrameDuration { get; set; }
        public SpriteEffect SpriteEffect { get; set; }
        public bool IsLooping { get; set; }
        public bool IsReversed { get; set; }

        public double CurrentTime { get; internal set; }

        public float FramesPerSecond
        {
            get => 1.0f / FrameDuration;
            set => FrameDuration = value / 1.0f;
        }

        public float AnimationDuration => Frames.Length * FrameDuration;


        public bool IsComplete => CurrentTime >= AnimationDuration;
        public bool IsPaused { get; private set; }
        public bool IsPlaying => !IsPaused && !IsComplete;

        public int PreviousFrameIndex { get; private set; }
        public int CurrentFrameIndex { get; private set; }

        public bool FrameHasChanged { get; private set; }
        public TextureRegion2D CurrentFrame => Frames[CurrentFrameIndex];

        public event Action OnCompleted;

        public SpriteSheetAnimation(string name, TextureRegion2D[] frames, float frameDuration, SpriteEffect spriteEffect,
            bool isLooping, bool isReversed)
        {
            Name = name;
            Frames = frames;
            FrameDuration = frameDuration;
            SpriteEffect = spriteEffect;
            IsLooping = isLooping;
            IsReversed = isReversed;

            CurrentFrameIndex = IsReversed ? Frames.Length - 1 : 0;
            PreviousFrameIndex = CurrentFrameIndex;
            FrameHasChanged = true;
        }

        public void Play()
        {
            IsPaused = false;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Stop()
        {
            Pause();
            Rewind();

            CurrentFrameIndex = IsReversed ? Frames.Length - 1 : 0;
        }

        public void Rewind()
        {
            CurrentTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsPlaying) return;

            // Update CurrentTIme
            var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            CurrentTime += deltaTime;

            // Animation Completed
            if (IsComplete)
            {
                OnCompleted?.Invoke();

                if (IsLooping)
                    CurrentTime -= AnimationDuration;
            }

            // Only One Frame
            if (Frames.Length == 1)
            {
                CurrentFrameIndex = 0;
            }

            // Get CurrentFrameIndex
            int frameIndex = (int)(CurrentTime / FrameDuration);
            int length = Frames.Length;

            if (IsLooping)
            {
                if (IsReversed)
                {
                    frameIndex = frameIndex % length;
                    frameIndex = length - frameIndex - 1;
                }
                else
                    frameIndex = frameIndex % length;
            }
            else
            {
                frameIndex = IsReversed ? Math.Max(length - frameIndex - 1, 0) : Math.Min(length - 1, frameIndex);
            }

            CurrentFrameIndex = frameIndex;

            if (CurrentFrameIndex != PreviousFrameIndex)
            {
                this.FrameHasChanged = true;
                PreviousFrameIndex = CurrentFrameIndex;
            }
            else
            {
                this.FrameHasChanged = false;
            }

        }
    }
}
