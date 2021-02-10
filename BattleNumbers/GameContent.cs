using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BattleNumbers
{
    class GameContent
    {
        public Texture2D daiManji { get; set; }
        public SpriteData.SpriteData daiManjiData { get; set; }
        public SoundEffect clickSound { get; set; }
        public SpriteFont baseFont { get; set; }

        public GameContent(ContentManager Content)
        {
            //load images
            daiManji = Content.Load<Texture2D>("sprites/DaiManji");
            daiManjiData = Content.Load<SpriteData.SpriteData>("sprites/DaiManji_Data");

            //load sounds
            clickSound = Content.Load<SoundEffect>("sounds/click1");

            //load fonts
            baseFont = Content.Load<SpriteFont>("fonts/AliensAmongUs");
        }
    }
}
