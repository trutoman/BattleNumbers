using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleNumbers
{
    class DaiManji
    {
        public float X { get; set; } // x position on screen
        public float Y { get; set; } // y position on screen
        public float Width { get; set; } // width of daiManji
        public float Height { get; set; } // height of daiManji
        public float ScreenWidth { get; set; } // width of game screen

        private Texture2D daiManji { get; set; }  // cached image of the paddle
        private SpriteBatch spriteBatch;  // allows us to write on backbuffer when we need to draw self

        public DaiManji(float x, float y, float screenWidth, SpriteBatch spriteBatch, GameContent gameContent)
        {
            X = x;
            Y = y;
            this.daiManji = gameContent.daiManji;
            this.Width = this.daiManji.Width;
            this.Height = this.daiManji.Height;
            this.spriteBatch = spriteBatch;
            ScreenWidth = screenWidth;
        }

        public void Draw()
        {
            spriteBatch.Draw(
                daiManji, 
                new Vector2(X, Y),
                new Rectangle(2, 2, 163, 116), 
                Color.White,
                0, 
                new Vector2(0, 0), 
                1.0f, 
                SpriteEffects.None, 
                0);
        }

        public void MoveLeft()
        {
            X = X - 5;
            if (X < 1)
            {
                X = 1;
            }
        }
        public void MoveRight()
        {
            X = X + 5;
            if ((X + Width) > ScreenWidth)
            {
                X = ScreenWidth - Width;
            }
        }

        public void MoveTo(float x)
        {
            if (x >= 0)
            {
                if (x < ScreenWidth - Width)
                {
                    X = x;
                }
                else
                {
                    X = ScreenWidth - Width;
                }
            }
            else
            {
                if (x < 0)
                {
                    X = 0;
                }
            }
        }
    }
}
