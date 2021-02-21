using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using static BattleNumbers.ECSComponents.Interaction2DComponent;

namespace BattleNumbers.ECSSystems
{
    public class InteractionSystem : ECSSystem
    {
        private MouseState current_mouseState;
        private MouseState prev_mouseState;

        // Rozdelit na mouse interaction a keyboard interaction system
        public InteractionSystem(ECSWorld world)
        {
            this.BindWorld(world);
        }

        public void Begin()
        {
            current_mouseState = Mouse.GetState();
        }

        public void End()
        {
            prev_mouseState = current_mouseState;
        }

        protected override void Update(ECSEntity entity, GameTime gameTime)
        {
            Begin();

            Process(entity);

            End();
        }

        public void Process(ECSEntity entity)
        {
            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            Interaction2DComponent interaction = entity.GetComponent<Interaction2DComponent>();

            // Check for Events
            bool onHover = CheckHover(transform, interaction);
            bool onOver = CheckOver(transform, interaction);
            bool onPress = CheckPress(transform, interaction);
            bool onRelease = CheckRelease(transform, interaction);
            bool onClick = CheckClick(transform, interaction);
            bool onDragStart = CheckDragStart(transform, interaction);
            bool onDragOver = CheckDragOver(transform, interaction);
            bool onDrop = CheckDrop(transform, interaction);

            // Trigger Events
            if (onHover) interaction.OnHover(BuildMouseEvent(entity.Id));
            if (onOver) interaction.OnOver(BuildMouseEvent(entity.Id));
            if (onPress) interaction.OnPress(BuildMouseEvent(entity.Id));
            if (onRelease) interaction.OnRelease(BuildMouseEvent(entity.Id));
            if (onClick) interaction.OnClick(BuildMouseEvent(entity.Id));
            if (onDragStart) interaction.OnDragStart(BuildDragEvent(entity.Id));
            if (onDragOver) interaction.OnDragOver(BuildDragEvent(entity.Id));
            if (onDrop) interaction.OnDrop(BuildDragEvent(entity.Id));
        }

        // Mouse handlers

        private bool CheckHover(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (!interaction.IsHovered && IsEntityHovered(transform, current_mouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckOver(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsHovered && !IsEntityHovered(transform, current_mouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckPress(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (!interaction.IsPressed && IsEntityPressed(transform, current_mouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckRelease(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsPressed && !IsEntityPressed(transform, current_mouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckClick(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsPressed && IsEntityHovered(transform, current_mouseState) && IsMouseReleased(current_mouseState))
            {
                return true;
            }
            return false;
        }

        // Drag handlers

        private bool CheckDragStart(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (!interaction.IsDraged && IsEntityPressed(transform, current_mouseState)
                && IsMouseReleased(prev_mouseState))
            {
                return true;
            }

            return false;
        }


        public bool CheckDragOver(Transform2DComponent transform, Interaction2DComponent interacton)
        {
            if (interacton.IsDraged && !IsEntityPressed(transform, current_mouseState) &&
                IsMousePressed(current_mouseState))
            {
                return true;
            }

            return false;
        }



        private bool CheckDrop(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsDraged && IsMouseReleased(current_mouseState))
            {
                return true;
            }

            return false;
        }

        private MouseEventArgs BuildMouseEvent(int entityId)
        {
            return new MouseEventArgs()
            {
                EntityId = entityId,
                MouseState = current_mouseState
            };
        }

        private DragEventArgs BuildDragEvent(int entityId)
        {
            return new DragEventArgs()
            {
                EntityId = entityId,
                MouseState = current_mouseState
            };
        }

        // Mouse Helpers

        public static bool IsMousePressed(MouseState mouseState)
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsMouseReleased(MouseState mouseState)
        {
            return mouseState.LeftButton == ButtonState.Released;
        }

        public static bool IsEntityHovered(Transform2DComponent transform, MouseState mouseState)
        {
            var bounds = new Rectangle(transform.AbsolutePosition.ToPoint(), transform.ScaleSize.ToPoint());


            return mouseState.Position.X >= bounds.X &&
                   mouseState.Position.X <= bounds.X + bounds.Width &&
                   mouseState.Position.Y >= bounds.Y &&
                   mouseState.Position.Y <= bounds.Y + bounds.Height;
        }

        public static bool IsEntityPressed(Transform2DComponent transform, MouseState mouseState)
        {
            return IsEntityHovered(transform, mouseState) && IsMousePressed(mouseState);
        }
    }
}
