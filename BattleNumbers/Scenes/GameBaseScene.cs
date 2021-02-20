using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using BattleNumbers.ECSEntities;
using BattleNumbers.ECSSystems;
using BattleNumbers.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

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

            world = new ECSWorld(this.myGame);

            ECSSystem renderedSystem = new RendererSystem(world);
            world.AddSystem(renderedSystem);

            EntityFactory entityFactory = new EntityFactory();
            entityFactory.LoadContent(world, gameContent);

            ECSEntity entity = entityFactory.CreateRenderEntity(new Rectangle(0, 0, 1280, 720), gameContent.background);
            ECSEntity entity2 = entityFactory.CreateAnimatedSpriteEntity(new Rectangle(0, 0, 1280, 720), gameContent.daiManjiSheet, gameContent.daiManjiData);
            renderedSystem.UpdateEntityRegistration(entity);
            renderedSystem.UpdateEntityRegistration(entity2);
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
                    world.Update(gameTime);
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
