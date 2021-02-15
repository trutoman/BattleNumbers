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
        private ECSWorld world;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
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
            this.isPaused = false;
        }

        public void LoadContent()
        {
            GameContent gameContent = new GameContent(this.myGame.Content);

            this.world = new ECSWorld(this.myGame);

            ECSEntity entity1 = world.AddAndGetEntity();
            entity1.AddComponent(new PositionComponent(0,0));
            entity1.AddComponent(new RendererComponent(gameContent.background));

            ECSEntity entity2 = world.AddAndGetEntity();
            entity2.AddComponent(new PositionComponent(100, 200));
            entity2.AddComponent(new SpriteComponent(gameContent.daiManjiSheet, gameContent.daiManjiData));

            ECSSystem renderedSystem = new RendererSystem(world);

            world.AddSystem(renderedSystem);

            world.GetSystem<RendererSystem>().UpdateEntityRegistration(entity1);
            world.GetSystem<RendererSystem>().UpdateEntityRegistration(entity2);

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
            if (this.Enabled)
            {
                world.Draw(gameTime);
            }
        }
        public void Dispose()
        {

        }
    }
}
