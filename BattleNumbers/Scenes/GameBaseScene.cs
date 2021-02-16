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

            world = new ECSWorld(this.myGame);                                               
            
            ECSEntity entity1 = world.AddAndGetEntity(new TokenArchetype());
            entity1.GetComponent<RendererComponent>().MainTexture = gameContent.background;

            ECSEntity entity2 = world.AddAndGetEntity(new SpriteArchetype());
            entity2.GetComponent<SpriteComponent>().Data = gameContent.daiManjiData;
            entity2.GetComponent<SpriteComponent>().MainTexture = gameContent.daiManjiSheet;
            entity2.GetComponent<SpriteComponent>().Play(entity2.GetComponent<SpriteComponent>().Data.InitSequence);

            ECSSystem renderedSystem = new RendererSystem(world);
            renderedSystem.UpdateEntityRegistration(entity1);
            renderedSystem.UpdateEntityRegistration(entity2);

            world.AddSystem(renderedSystem);
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
