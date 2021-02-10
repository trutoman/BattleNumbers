using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace BattleNumbers
{
    public class BattleNumbers : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameContent gameContent;
        private DaiManji daiManji;
        private int screenWidth = 0;
        private int screenHeight = 0;

        public BattleNumbers()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferMultiSampling = false;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            //this._graphics.IsFullScreen = true;            
            this.graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //set game to 502x700 or screen max if smaller
            if (screenWidth >= this.graphics.PreferredBackBufferWidth)
            {
                screenWidth = this.graphics.PreferredBackBufferWidth;
            }
            if (screenHeight >= this.graphics.PreferredBackBufferHeight)
            {
                screenHeight = this.graphics.PreferredBackBufferHeight;
            }
            graphics.ApplyChanges();

            //create game objects
            int daiManjiInitialX = 0;
            //we'll center the paddle on the screen to start
            int daiManjiInitialY = 0;  //paddle will be 100 pixels from the bottom of the screen
            daiManji = new DaiManji(daiManjiInitialX, daiManjiInitialY, screenWidth, spriteBatch, gameContent);  // create the game paddle
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here                      
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            daiManji.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
