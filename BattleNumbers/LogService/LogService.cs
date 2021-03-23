using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.LogService
{
    public class LogService : DrawableGameComponent, ILogService
    {
        BattleNumbers game;
        SpriteBatch spriteBatch;
        string text;
        SpriteFont font;        

        public LogService(BattleNumbers game) : base(game)
        {
            this.game = game;
            this.text = string.Empty;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            font = game.Content.Load<SpriteFont>("fonts/AliensAmongUs");            
        }

        public void setSpriteBatch(SpriteBatch batch)
        {
            spriteBatch = batch;
        }

        public override void Initialize()
        { }

        protected override void LoadContent()
        { }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public void Log(string text)
        {
            this.text = text;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, this.game.SceneManager.Scale);
            this.spriteBatch.DrawString(
                font,
                text,
                new Vector2(0,0),
                Color.White,
                0,
                origin: Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
