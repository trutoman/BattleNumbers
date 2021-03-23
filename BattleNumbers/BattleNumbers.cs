using BattleNumbers.LogService;
using BattleNumbers.Scene;
using BattleNumbers.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleNumbers
{
    public class BattleNumbers : Game
    {
        public int VirtualWidth = 1280;
        public int VirtualHeight = 720;

        public GraphicsDeviceManager graphics;                
        public SceneManager SceneManager { get; set; }
        public LogService.LogService logService;

        public BattleNumbers() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.graphics.PreferMultiSampling = false;
            this.graphics.PreferredBackBufferWidth = 1600;
            this.graphics.PreferredBackBufferHeight = 900;

            //this.graphics.IsFullScreen = true;

            this.graphics.ApplyChanges();
            IsMouseVisible = true;

            this.SceneManager = new SceneManager(this, VirtualWidth, VirtualHeight);
            Components.Add(SceneManager);

            // Creating Drawable Log service and drawable component
            Components.Add(logService = new LogService.LogService(this));
            Services.AddService(typeof(ILogService), logService);

            base.Initialize();
        }

        protected override void LoadContent()
        {                       
            this.SceneManager.LoadScene(new GameBaseScene(this));
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
