using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSSystems;
using BattleNumbers.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleNumbers.Scene
{
    public class GameBaseScene : IScene
    {
        private BattleNumbers myGame;
        private World world;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        private bool _Visible;
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        private int _DrawOrder;
        public int DrawOrder
        {
            get { return _DrawOrder; }
            set { _DrawOrder = value; }
        }

        private int _UpdateOrder;
        public int UpdateOrder
        {
            get { return _UpdateOrder; }
            set { _UpdateOrder = value; }
        }

        private SpriteData.SpriteData _Data;
        public SpriteData.SpriteData Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private bool _isPaused;
        public bool isPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        public GameBaseScene(BattleNumbers game)
        {
            this.myGame = game;
            this.Enabled = true;
            this.Visible = true;
            this.isPaused = false;
        }

        public void LoadContent()
        {
            GameContent gameContent = new GameContent(this.myGame.Content);

            this.world = new World(this.myGame);

            ECSEntity entity1 = world.AddAndGetEntity();
            entity1.AddComponent(new PositionComponent(0,0));
            entity1.AddComponent(new RendererComponent(gameContent.background));

            ECSEntity entity2 = world.AddAndGetEntity();
            entity2.AddComponent(new PositionComponent(100, 200));
            entity2.AddComponent(new SpriteComponent(gameContent.daiManjiSheet, gameContent.daiManjiData));

            ECSSystem renderedSystem = new RendererSystem(world, this.myGame);

            renderedSystem.UpdateEntityRegistration(entity1);
            renderedSystem.UpdateEntityRegistration(entity2);

            world.AddSystem(new RendererSystem(world, this.myGame));


        }

        public void UnloadContent()
        {

        }

        public void Pause(bool value)
        {
            this.isPaused = value;
        }

        public void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                if (!this.isPaused)
                {
                    world.Update(gameTime.ElapsedGameTime.Milliseconds);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            //actor1.Draw(Color.White);
        }

        public void Dispose()
        {

        }
    }
}
