using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleNumbers.ECSSystems
{
    public class RendererSystem : ECSSystem
    {
        private readonly BattleNumbers Game;
        private readonly SpriteBatch Batch;

        public RendererSystem(World world,BattleNumbers game)
        {
            this.Game = game;
            AddRequiredComponent<PositionComponent>();
            AddRequiredComponent<RendererComponent>();
            this.BindManager(world);
        }

        // This method Update individual entities registered
        protected override void Update(ECSEntity entity, float deltaTime)
        { }

        public void Draw(GameTime gameTime)
        {
            foreach (ECSEntity entity in this.Entities)
            {
                PositionComponent position = entity.GetComponent<PositionComponent>();
                RendererComponent renderer = entity.GetComponent<RendererComponent>();

                var texture = renderer.MainTexture;

                Rectangle sourceRectangle = new Rectangle(
                position.X,
                position.Y,
                renderer.MainTexture.Width,
                renderer.MainTexture.Height);

                this.Batch.Begin();

                this.Batch.Draw(texture, new Vector2(position.X, position.Y), sourceRectangle, renderer.Color,
                    0, Vector2.Zero, 1f, renderer.Effects, renderer.Depth);

                this.Batch.End();

            }            
        }
    }
}
