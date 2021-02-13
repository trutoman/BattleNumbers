using BattleNumbers.Scene;
using BattleNumbers.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleNumbers
{
    public class BattleNumbers : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameContent gameContent;
        private SceneManager sceneManager;

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

            this.sceneManager = new SceneManager(this);
        }

        protected override void Initialize()
        {
            Components.Add(sceneManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            if (screenWidth >= this.graphics.PreferredBackBufferWidth)
            {
                screenWidth = this.graphics.PreferredBackBufferWidth;
            }
            if (screenHeight >= this.graphics.PreferredBackBufferHeight)
            {
                screenHeight = this.graphics.PreferredBackBufferHeight;
            }
            graphics.ApplyChanges();

            this.sceneManager.LoadScene(new GameBaseScene(this));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public SpriteBatch GetSpriteBatch() => this.spriteBatch;
        public GameContent GetGameContent() => this.gameContent;
    }
}
