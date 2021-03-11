using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using BattleNumbers.ECSEntities;
using BattleNumbers.ECSSystems;
using BattleNumbers.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
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

            // Create world with systems
            world = new ECSWorld(this.myGame);
            ECSSystem renderedSystem = new RendererSystem(world);
            ECSSystem interactionSystem = new InteractionSystem(world, OnlyOneDragedElement:true);
            world.AddSystem(renderedSystem);
            world.AddSystem(interactionSystem);

            // Create a entity factory
            EntityFactory entityFactory = new EntityFactory();
            entityFactory.LoadContent(world, gameContent);

            // Create and register entities
            //ECSEntity entity = entityFactory.CreateRenderEntity(new Point(0, 0), gameContent.background);
            //ECSEntity entity2 = entityFactory.CreateAnimatedSpriteEntity(new Point(100, 100), gameContent.daiManjiSheet, gameContent.daiManjiData);

            ECSEntity entity3 = entityFactory.CreateTokenEntity(
                new TokenTypeComponent(77, gameContent.baseFont),
                new Vector2(100, 100),
                new Vector2(world.Game.VirtualWidth, world.Game.VirtualHeight),
                gameContent.daiManjiSheet,
                gameContent.daiManjiData);

            ECSEntity entity4 = entityFactory.CreateTokenEntity(
                new TokenTypeComponent(66, gameContent.baseFont),
                new Vector2(300, 300),
                new Vector2(world.Game.VirtualWidth, world.Game.VirtualHeight),
                gameContent.tokenSheet,
                gameContent.tokenData);

            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Debug.Print($"{currentMethodName} finally {entity3.GetComponent<Transform2DComponent>()}");

            //renderedSystem.UpdateEntityRegistration(entity);
            //renderedSystem.UpdateEntityRegistration(entity2);
            renderedSystem.UpdateEntityRegistration(entity3);
            interactionSystem.UpdateEntityRegistration(entity3);

            renderedSystem.UpdateEntityRegistration(entity4);
            interactionSystem.UpdateEntityRegistration(entity4);
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
                    Debug.Print("---- UPDATE ------------------------------------");
                    world.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (this.Enabled)
            {
                Debug.Print("---- DRAW   -------------------------------------");
                world.Draw(gameTime);
            }
        }
        public void Dispose()
        {

        }
    }
}
