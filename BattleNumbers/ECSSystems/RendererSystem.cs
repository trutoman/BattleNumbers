using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BattleNumbers.ECSSystems
{
    public class RendererSystem : ECSSystem, IDrawSystem
    {
        private readonly SpriteBatch Batch;

        public RendererSystem(ECSWorld world)
        {
            this.BindWorld(world);
            this.Batch = new SpriteBatch(this.World.game.GraphicsDevice);
            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(RendererComponent) });
            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(SpriteComponent) });
        }

        // This method Update individual entities registered
        protected override void Update(ECSEntity entity, GameTime gametime)
        {
            if (entity.HasComponent<AnimatedSpriteComponent>())
            {
                entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.Update(gametime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (ECSEntity entity in this.Entities)
            {

                if (entity.HasComponent<RendererComponent>())
                {
                    DrawRendererComponent(entity);
                }

                else if (entity.HasComponent<SpriteComponent>())
                {
                    DrawSpriteComponent(entity);
                }

                else if (entity.HasComponent<AnimatedSpriteComponent>())
                {
                    DrawAnimatedSpriteComponent(entity);
                }
            }
        }
        private void DrawAnimatedSpriteComponent(ECSEntity entity)
        {
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            SpriteComponent sprite = entity.GetComponent<AnimatedSpriteComponent>();

            Rectangle sourceRectangle = new Rectangle(
                                (int)object2D.Position.X,
                                (int)object2D.Position.Y,
                                sprite.TextureRegion.Width,
                                sprite.TextureRegion.Height);

            this.Batch.Begin();

            this.Batch.Draw(
                sprite.TextureRegion.Texture,
                object2D.Position,
                sourceRectangle,
                sprite.Color,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.ScaleSize,
                sprite.Effects,
                sprite.Depth);

            this.Batch.End();
        }

        private void DrawSpriteComponent(ECSEntity entity)
        {
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            Rectangle sourceRectangle = new Rectangle(
                                (int)object2D.Position.X,
                                (int)object2D.Position.Y,
                                sprite.TextureRegion.Width,
                                sprite.TextureRegion.Height);

            this.Batch.Begin();

            this.Batch.Draw(
                sprite.TextureRegion.Texture,
                object2D.Position,
                sourceRectangle,
                sprite.Color,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.ScaleSize,
                sprite.Effects,
                sprite.Depth);

            this.Batch.End();
        }

        private void DrawRendererComponent(ECSEntity entity)
        {
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            RendererComponent renderer = entity.GetComponent<RendererComponent>();

            Rectangle sourceRectangle = new Rectangle(
                                (int)object2D.Position.X,
                                (int)object2D.Position.Y,
                                renderer.MainTexture.Width,
                                renderer.MainTexture.Height);

            this.Batch.Begin();

            this.Batch.Draw(
                renderer.MainTexture,
                object2D.Position,
                sourceRectangle,
                renderer.Color,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.ScaleSize,
                renderer.Effects,
                renderer.Depth);

            this.Batch.End();
        }
    }
}

