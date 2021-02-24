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

        public ECSEntity CreateRenderEntity(Rectangle bounds, Texture2D texture)
        {
            ECSEntity entity = World.CreateEntity(new RenderArchetype(), texture);

            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            transform.Bounds = bounds;

            return entity;
        }

        public ECSEntity CreateAnimatedSpriteEntity(Point origin, Texture2D sheet, SpriteData.SpriteData sheetData)
        {
            ECSEntity entity = World.CreateEntity(new AnimatedSpriteArchetype(), sheet, sheetData);

            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            AnimatedSpriteComponent animation = entity.GetComponent<AnimatedSpriteComponent>();

            animation.Play(sheetData.InitSequence);

            Rectangle rect = new Rectangle(
                origin.X,
                origin.Y,
                animation.CurrentAnimation.CurrentFrame.Width,
                animation.CurrentAnimation.CurrentFrame.Height);

            transform.Bounds = rect;

            return entity;
        }

        public ECSEntity CreateTokenEntity(Point origin, Texture2D sheet, SpriteData.SpriteData sheetData)
        {

            ECSEntity entity = World.CreateEntity(new TokenArchetype(), sheet, sheetData, origin);

            TokenTypeComponent token = entity.GetComponent<TokenTypeComponent>();
            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();
            AnimatedSpriteComponent animation = entity.GetComponent<AnimatedSpriteComponent>();

            animation.Play(sheetData.InitSequence);

            Rectangle rect = new Rectangle(
                origin.X, 
                origin.Y, 
                animation.CurrentAnimation.CurrentFrame.Width, 
                animation.CurrentAnimation.CurrentFrame.Height);

            transform.Bounds = rect;

            interaction.Press += TokenEntityPressHandler;
            // Using Release event in place of dragover because of : 
            // due to time or performance settings sometimes fast movement when dragged an object            
            // produces a IsDragged = false 
            interaction.Release += TokenEntityReleaseHandler;
            interaction.Move += TokenEntityMoveHandler;
            interaction.DragStart += TokenEntityDragStartHandler;

            return entity;
        }

        private void TokenEntityMoveHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();

            Debug.Print($"PREVV {currentMethodName} finally {object2D}");
            Debug.Print($"PREVV {currentMethodName} relative {interaction.RelativePressedPoint}");
            Debug.Print($"PREVV {currentMethodName} mouse {e.MouseState.Position}");

            object2D.Position = new Vector2(e.MouseState.Position.X - interaction.RelativePressedPoint.X, e.MouseState.Position.Y - interaction.RelativePressedPoint.Y);
            object2D.Bounds = new Rectangle((int)object2D.Position.X, (int)object2D.Position.Y, (int)object2D.Size.X, (int)object2D.Size.Y);

            Debug.Print($"{currentMethodName} finally {object2D}");
        }

        private void TokenEntityPressHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();

            object2D.Scale = new Vector2(1.15f, 1.15f);
            object2D.Bounds = new Rectangle((int)object2D.Position.X, (int)object2D.Position.Y, (int)object2D.Size.X, (int)object2D.Size.Y);

            Debug.Print($"{currentMethodName} finally {object2D}");
        }

        private void TokenEntityReleaseHandler(object sender, MouseEventArgs e)
        {
            int id = e.EntityId;
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ECSEntity entity = this.World.GetEntityById(id);
            Transform2DComponent object2D = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();

            Debug.Print($"{currentMethodName} release MOUZ {e.MouseState.Position}");
            Debug.Print($"{currentMethodName} release RelatiVE {interaction.RelativePressedPoint}");

            object2D.Position = new Vector2(e.MouseState.Position.X - interaction.RelativePressedPoint.X, e.MouseState.Position.Y - interaction.RelativePressedPoint.Y);            
            object2D.Scale = new Vector2(1f, 1f);
            object2D.Bounds = new Rectangle((int)object2D.Position.X, (int)object2D.Position.Y, (int)object2D.Size.X, (int)object2D.Size.Y);

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
        }

    }
}
