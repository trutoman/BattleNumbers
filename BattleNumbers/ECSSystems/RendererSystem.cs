using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using BattleNumbers.ECSEntities;
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
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.Batch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, this.World.Game.SceneManager.Scale);

            foreach (ECSEntity entity in this.Entities)
            {
                if (entity.Archetype == typeof(TokenImageArchetype))
                {
                    DrawTokenImageArchetype(entity);
                }
                else if (entity.Archetype == typeof(TokenSpriteArchetype))
                {
                    DrawTokenSpriteArchetype(entity);
                }
                else if (entity.Archetype == typeof(TextRenderArchetype))
                {
                    DrawTextRenderArchetype(entity);
                }
            }
            this.Batch.End();
        }
        private void DrawTextRenderArchetype(ECSEntity entity)
        {
            TextRenderComponent text = entity.GetComponent<TextRenderComponent>();
            Transform2DComponent transform2D = entity.GetComponent<Transform2DComponent>();

            this.Batch.DrawString(
                text.Font,
                Log.text,
                transform2D.Position,
                text.Color,
                transform2D.Rotation,
                origin: Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
        }

        private void DrawTokenImageArchetype(ECSEntity entity)
        {
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            TokenTypeComponent token = entity.GetComponent<TokenTypeComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

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
                sprite.Depth);

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

        private void DrawTokenSpriteArchetype(ECSEntity entity)
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
    }
}

