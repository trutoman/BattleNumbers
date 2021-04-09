using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using BattleNumbers.LogService;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BattleNumbers.ECSSystems
{
    public class RendererSystem : ECSSystem, IDrawSystem, IUpdateSystem
    {
        private readonly SpriteBatch Batch;
        LogService.LogService Log;

        public RendererSystem(ECSWorld world, LogService.LogService log)
        {
            this.BindWorld(world);
            this.Batch = new SpriteBatch(this.World.Game.GraphicsDevice);

            Log = log;

            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(RendererComponent) });
            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(SpriteComponent) });
            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(AnimatedSpriteComponent) });
            AddRequiredComponents(new List<Type>() { typeof(TextRenderComponent) });
        }

        // This method Update individual entities registered
        public override void Update(GameTime gametime)
        {
            foreach (ECSEntity entity in Entities)
            {
                UpdateEntityRegistration(entity);
                if (entity.HasComponent<AnimatedSpriteComponent>())
                {
                    AnimatedSpriteComponent animatedSprite = entity.GetComponent<AnimatedSpriteComponent>();

                    if (animatedSprite.CurrentAnimation != null && !animatedSprite.CurrentAnimation.IsComplete)
                    {
                        animatedSprite.CurrentAnimation.Update(gametime);
                        animatedSprite.SetTextureRegion(animatedSprite.CurrentAnimation.CurrentFrame);
                    }
                    // Sprite bounds is variable so everytime we updated sprite we update also transform2d components bounds
                    // transform2D component bounds is the rectangle we will use to draw sprite at Draw method.

                    if (animatedSprite.CurrentAnimation.FrameHasChanged)
                    {
                        Transform2DComponent Object2D = entity.GetComponent<Transform2DComponent>();

                        Object2D.Size = new Vector2(
                            entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Width,
                            entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Height);

                        string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        Debug.Print($"{currentMethodName} finally {Object2D}");
                    }
                }
                else if (entity.HasComponent<RendererComponent>())
                {

                }
                else if (entity.HasComponent<SpriteComponent>())
                {
                    // Sprite bounds is variable so everytime we updated sprite we update also transform2d components bounds
                    // transform2D component bounds is the rectangle we will use to draw sprite at Draw method.
                    Transform2DComponent Object2D = entity.GetComponent<Transform2DComponent>();
                    Object2D.Size = new Vector2(
                        entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Width,
                        entity.GetComponent<AnimatedSpriteComponent>().CurrentAnimation.CurrentFrame.Height);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            //this.Batch.Begin();
            this.Batch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, this.World.Game.SceneManager.Scale);
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
                    if (entity.HasComponent<TokenTypeComponent>())
                    {
                        DrawTokenType(entity);
                    }
                    else
                    {
                        DrawAnimatedSpriteComponent(entity);
                    }
                }

                else if (entity.HasComponent<TextRenderComponent>())
                {
                    if (entity.Id == Log.entityId)
                    {
                        if (Log.active)
                        {
                            DrawTextComponent(entity);
                        }
                    }
                    else
                    {
                        DrawTextComponent(entity);
                    }
                }
            }
            this.Batch.End();
        }
        private void DrawTextComponent(ECSEntity entity)
        {
            TextRenderComponent text = entity.GetComponent<TextRenderComponent>();

            Vector2 drawPosition = new Vector2(0, 0);

            this.Batch.DrawString(
                text.Font,
                Log.text,
                drawPosition,
                text.Color,
                0,
                origin: Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
        }

        private void DrawAnimatedSpriteComponent(ECSEntity entity)
        {
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            AnimatedSpriteComponent sprite = entity.GetComponent<AnimatedSpriteComponent>();

            var region = sprite.TextureRegion;

            Debug.Print($"{currentMethodName} - {object2D.ToString()}");

            this.Batch.Draw(
                region.Texture,
                object2D.TopLeftCornerPosition,
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
            Vector2 drawPosition = new Vector2(object2D.Position.X - region.Width / 2, object2D.Position.Y - region.Height / 2);

            this.Batch.Draw(
                region.Texture,
                object2D.Position,
                region.Bounds,
                sprite.Color,
                object2D.Rotation,
                origin: object2D.Position,
                object2D.ScaleSize,
                sprite.Effects,
                sprite.Depth);
        }

        private void DrawTokenType(ECSEntity entity)
        {
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            TokenTypeComponent token = entity.GetComponent<TokenTypeComponent>();
            AnimatedSpriteComponent sprite = entity.GetComponent<AnimatedSpriteComponent>();

            var region = sprite.TextureRegion;

            Debug.Print($"{currentMethodName} - {object2D.ToString()}");
            Debug.Print($"{currentMethodName} - DEPTH: {token.Depth}");

            this.Batch.Draw(
                region.Texture,
                object2D.TopLeftCornerPosition,
                region.Bounds,
                sprite.Color,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.Scale,
                sprite.Effects,
                token.Depth);

            this.Batch.DrawString(
                token.Font,
                token.Image,
                object2D.Position,
                Color.Black,
                object2D.Rotation,
                origin: Vector2.Zero,
                object2D.Scale,
                sprite.Effects,
                token.Depth + 0.01f);
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

