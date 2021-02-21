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
            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(AnimatedSpriteComponent) });
        }

        // This method Update individual entities registered
        protected override void Update(ECSEntity entity, GameTime gametime)
        {
            if (entity.HasComponent<AnimatedSpriteComponent>())
            {
                AnimatedSpriteComponent animatedSprite = entity.GetComponent<AnimatedSpriteComponent>();

                if (animatedSprite.CurrentAnimation != null && !animatedSprite.CurrentAnimation.IsComplete)
                {
                    animatedSprite.CurrentAnimation.Update(gametime);
                    animatedSprite.SetTextureRegion(animatedSprite.CurrentAnimation.CurrentFrame);
                }
                // Sprite bounds ius variable so everytime we updated sprite we update also transform2d components bounds
                // transform2D component bounds is the rectangle we will use to draw sprite at Draw method.
                Transform2DComponent Object2D = entity.GetComponent<Transform2DComponent>();
                Object2D.Size = new Vector2(entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Width,
                    entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Height);

            }
            else if (entity.HasComponent<SpriteComponent>())
            {
                // Sprite bounds ius variable so everytime we updated sprite we update also transform2d components bounds
                // transform2D component bounds is the rectangle we will use to draw sprite at Draw method.
                Transform2DComponent Object2D = entity.GetComponent<Transform2DComponent>();
                Object2D.Size = new Vector2(entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Width,
                    entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Height);
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.Batch.Begin();
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
            this.Batch.End();
        }
        private void DrawAnimatedSpriteComponent(ECSEntity entity)
        {
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            AnimatedSpriteComponent sprite = entity.GetComponent<AnimatedSpriteComponent>();

            var region = sprite.TextureRegion;

            this.Batch.Draw(
                region.Texture,
                object2D.Position,
                region.Bounds,
                sprite.Color,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.Scale,
                sprite.Effects,
                sprite.Depth);
        }

        private void DrawSpriteComponent(ECSEntity entity)
        {
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            var region = sprite.TextureRegion;

            this.Batch.Draw(
                region.Texture,
                object2D.Position,
                region.Bounds,
                sprite.Color,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.ScaleSize,
                sprite.Effects,
                sprite.Depth);
        }

        private void DrawRendererComponent(ECSEntity entity)
        {
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            RendererComponent renderer = entity.GetComponent<RendererComponent>();

            Rectangle sourceRectangle = new Rectangle(
                                0,
                                0,
                                renderer.MainTexture.Width,
                                renderer.MainTexture.Height);

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
        }
    }
}

