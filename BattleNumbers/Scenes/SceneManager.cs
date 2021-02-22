using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleNumbers.Scenes
{
    public interface ISceneManager
    {
        void LoadScene(IScene scene);
        IScene GetActiveScene();
    }

    public class SceneManager : DrawableGameComponent, ISceneManager
    {
        private IScene _activeScene;
        private IScene _prevScene;
        private BattleNumbers Game;

        // Fixed proportions for all game
        private int virtualWidth;
        private int virtualHeight;
        private GraphicsDeviceManager graphicsDeviceManager;
        private bool updateMatrix = true;
        
        private Matrix scale = Matrix.Identity;
        public Matrix Scale
        {
            get
            {
                if (this.updateMatrix)
                {
                    CreateScaleMatrix();
                    this.updateMatrix = false;
                }
                return scale;
            }
        }

        // http://www.infinitespace-studios.co.uk/general/handling-multiple-screen-resolutions-in-monogame-for-android-part-1/
        public Viewport Viewport
        {
            get { return new Viewport(0, 0, this.virtualWidth, this.virtualHeight); }
        }

        protected void CreateScaleMatrix()
        {
            this.scale = Matrix.CreateScale(
                (float)GraphicsDevice.Viewport.Width / this.virtualWidth,
                (float)GraphicsDevice.Viewport.Width / this.virtualWidth, 1f);
        }

        public Matrix InputScale
        {
            get { return Matrix.Invert(Scale); }
        }

        public Vector2 InputTranslate
        {
            get { return new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y); }
        }

        protected void FullViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = this.Game.graphics.PreferredBackBufferWidth;
            vp.Height = this.Game.graphics.PreferredBackBufferHeight;
            GraphicsDevice.Viewport = vp;
        }

        protected float GetVirtualAspectRatio()
        {
            return (float)this.virtualWidth / (float)this.virtualHeight;
        }

        protected void ResetViewport()
        {
            float targetAspectRatio = GetVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio     
            int width = this.Game.graphics.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;
            if (height > this.Game.graphics.PreferredBackBufferHeight) 
            {
                height = this.Game.graphics.PreferredBackBufferHeight;
                // PillarBox 
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }
            // set up the new viewport centered in the backbuffer 
            Viewport viewport = new Viewport();
            viewport.X = (this.Game.graphics.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (this.Game.graphics.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;
            if (changed)
            {
                this.updateMatrix = true;
            }
            this.Game.graphics.GraphicsDevice.Viewport = viewport;
        }

        protected void BeginDraw()
        {
            // Start by reseting viewport 
            FullViewport();
            // Clear to Black 
            GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio 
            ResetViewport();
            // and clear that    
            // This way we are gonna have black bars if aspect ratio requires it and     
            // the clear color on the rest 
            GraphicsDevice.Clear(Color.Black);
        }


        // http://www.infinitespace-studios.co.uk/general/handling-multiple-screen-resolutions-in-monogame-for-android-part-1/

        public SceneManager(BattleNumbers game, int virtualWidth, int virtualHeight) : base(game)
        {
            this.Game = game;
            this.virtualWidth = virtualWidth;
            this.virtualHeight = virtualHeight;

            // http://www.infinitespace-studios.co.uk/general/handling-multiple-screen-resolutions-in-monogame-for-android-part-1/
            // set the Virtual environment up
            this.graphicsDeviceManager = (GraphicsDeviceManager)game.Services.GetService(typeof(IGraphicsDeviceManager));
            // we must set EnabledGestures before we can query for them, but

            // TODO: Android
            // we don't assume the game wants to read them.
            //TouchPanel.EnabledGestures = GestureType.None;
            // http://www.infinitespace-studios.co.uk/general/handling-multiple-screen-resolutions-in-monogame-for-android-part-1/

            _activeScene = null;
            _prevScene = null;
        }

        public void LoadScene(IScene scene)
        {
            if (_activeScene != null)
            {
                _prevScene = _activeScene;
                _prevScene.Dispose();
                _prevScene.UnloadContent();
            }

            _activeScene = scene;

            _activeScene.LoadContent();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _activeScene.Dispose();
            _activeScene.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            BeginDraw();
            _activeScene.Draw(gameTime);
            base.Draw(gameTime);
        }

        public IScene GetActiveScene() => _activeScene;
    }
}