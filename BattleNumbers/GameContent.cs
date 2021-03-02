using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleNumbers
{
    public class GameContent
    {
        public Texture2D daiManjiSheet { get; set; }
        public Texture2D tokenSheet { get; set; }
        public Texture2D background { get; set; }
        public SpriteData.SpriteData daiManjiData { get; set; }
        public SpriteData.SpriteData tokenData { get; set; }
        public SoundEffect clickSound { get; set; }
        public SpriteFont baseFont { get; set; }

        public GameContent(ContentManager Content)
        {
            //load images
            background = Content.Load<Texture2D>("images/external");

            //load sprites
            daiManjiSheet = Content.Load<Texture2D>("sprites/DaiManji");
            daiManjiData = Content.Load<SpriteData.SpriteData>("sprites/DaiManji_Data");

            tokenSheet = Content.Load<Texture2D>("sprites/TokenSprite");
            tokenData = Content.Load<SpriteData.SpriteData>("sprites/TokenSprite_Data");

            //load sounds
            clickSound = Content.Load<SoundEffect>("sounds/click1");

            //load fonts
            baseFont = Content.Load<SpriteFont>("fonts/AliensAmongUs");
        }
    }
}
