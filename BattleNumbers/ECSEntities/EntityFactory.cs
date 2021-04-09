using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BattleNumbers.ECSEntities
{
    public class EntityFactory
    {
        protected ECSWorld World;
        protected GameContent Content;

        public void LoadContent(ECSWorld world, GameContent gameContent)
        {
            this.World = world;
            this.Content = gameContent;
        }

        public ECSEntity CreateTextEntity(SpriteFont font)
        {
            ECSEntity entity = World.CreateEntity(new TextRenderArchetype(), font);

            return entity;
        }

        public ECSEntity CreateRenderEntity(Point position, Texture2D texture)
        {
            ECSEntity entity = World.CreateEntity(new RenderArchetype(), texture);

            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            transform.Position = new Vector2(position.X, position.Y);

            return entity;
        }



        public ECSEntity CreateAnimatedSpriteEntity(Point position, Texture2D sheet, SpriteData.SpriteData sheetData)
        {
            ECSEntity entity = World.CreateEntity(new AnimatedSpriteArchetype(), sheet, sheetData);

            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            AnimatedSpriteComponent animation = entity.GetComponent<AnimatedSpriteComponent>();

            animation.Play(sheetData.InitSequence);

            transform.Size = new Vector2(animation.CurrentAnimation.CurrentFrame.Width, animation.CurrentAnimation.CurrentFrame.Height);
            transform.Position = new Vector2(position.X, position.Y);            

            return entity;
        }

        public ECSEntity CreateTokenEntity(TokenTypeComponent token, Vector2 position, Vector2 limits, Texture2D sheet, SpriteData.SpriteData sheetData)
        {
            ECSEntity entity = World.CreateEntity(new TokenArchetype(), sheet, sheetData, position, limits, token);
            
            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();
            AnimatedSpriteComponent animation = entity.GetComponent<AnimatedSpriteComponent>();
            CollisionComponent collision = entity.GetComponent<CollisionComponent>();
            
            // Using Release event in place of dragover because of : 
            // due to time or performance settings sometimes fast movement when dragged an object            
            // produces a IsDragged = false 
            interaction.ReleaseNotHovered += TokenEntityReleaseNotHoveredHandler;
            interaction.Press += TokenEntityPressHandler;
            interaction.Move += TokenEntityMoveHandler;
            interaction.DragStart += TokenEntityDragStartHandler;

            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Debug.Print($"{currentMethodName} finally {transform}");

            return entity;
        }

        private void TokenEntityMoveHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();

            object2D.Position = new Vector2(e.MouseState.Position.X - interaction.RelativePressedPoint.X, e.MouseState.Position.Y - interaction.RelativePressedPoint.Y);            

            Debug.Print($"{currentMethodName} finally {object2D}");
        }

        private void TokenEntityPressHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            TokenTypeComponent token = entity.GetComponent<TokenTypeComponent>();

            object2D.Scale = new Vector2(1.15f, 1.15f);

            // Set depth to token pressed
            token.Depth = 0.90f;

            Debug.Print($"{currentMethodName} finally {object2D}");
        }

        private void TokenEntityReleaseNotHoveredHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();
            TokenTypeComponent token = entity.GetComponent<TokenTypeComponent>();

            object2D.Position = new Vector2(e.MouseState.Position.X - interaction.RelativePressedPoint.X, e.MouseState.Position.Y - interaction.RelativePressedPoint.Y);            
            object2D.Scale = new Vector2(1f, 1f);

            // Set depth to token released
            token.Depth = 0.5f;

            Debug.Print($"{currentMethodName} finally {object2D}");
        }

        private void TokenEntityDragStartHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();


            interaction.RelativePressedPoint = new Point(e.MouseState.Position.X - (int)object2D.Position.X, e.MouseState.Position.Y - (int)object2D.Position.Y);

            Debug.Print($"{currentMethodName} finally {object2D}");
        }

    }
}
