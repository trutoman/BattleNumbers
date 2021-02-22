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
        private SceneManager sceneManager;

        public BattleNumbers()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.graphics.PreferMultiSampling = false;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;

            //this._graphics.IsFullScreen = true;            
            this.graphics.ApplyChanges();
            IsMouseVisible = true;

            this.sceneManager = new SceneManager(this);
            Components.Add(sceneManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {                       
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
    }
}
