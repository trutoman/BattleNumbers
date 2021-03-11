using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static BattleNumbers.ECSComponents.Interaction2DComponent;

namespace BattleNumbers.ECSSystems
{
    public class InteractionSystem : ECSSystem
    {
        private MouseState CurrentMouseState;
        private MouseState PreviousMouseState;
        private bool OnlyOneDragedElement;
        private bool OneDraged = false;

        // Rozdelit na mouse interaction a keyboard interaction system
        public InteractionSystem(ECSWorld world, bool OnlyOneDragedElement)
        {
            this.BindWorld(world);
            this.OnlyOneDragedElement = OnlyOneDragedElement;
        }

        public void Begin()
        {
            // TODO: Android
            //Gestures.Clear();
            //while (TouchPanel.IsGestureAvailable)
            //{
            //    GestureSample g = TouchPanel.ReadGesture();
            //    Vector2 p1 = Vector2.Transform(g.Position - ScreenManager.InputTranslate, ScreenManager.InputScale);
            //    Vector2 p2 = Vector2.Transform(g.Position2 - ScreenManager.InputTranslate, ScreenManager.InputScale);
            //    Vector2 p3 = Vector2.Transform(g.Delta - ScreenManager.InputTranslate, ScreenManager.InputScale);
            //    Vector2 p4 = Vector2.Transform(g.Delta2 - ScreenManager.InputTranslate, ScreenManager.InputScale);
            //    g = new GestureSample(g.GestureType, g.Timestamp, p1, p2, p3, p4);
            //    Gestures.Add(g);
            //}

            CurrentMouseState = Mouse.GetState();
            Vector2 _mousePosition = new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
            Vector2 p = _mousePosition - this.World.Game.SceneManager.InputTranslate;
            p = Vector2.Transform(p, this.World.Game.SceneManager.InputScale);
            CurrentMouseState = new MouseState(
                (int)p.X,
                (int)p.Y,
                CurrentMouseState.ScrollWheelValue,
                CurrentMouseState.LeftButton,
                CurrentMouseState.MiddleButton,
                CurrentMouseState.RightButton,
                CurrentMouseState.XButton1,
                CurrentMouseState.XButton2);
        }

        public void End()
        {
            PreviousMouseState = CurrentMouseState;
        }

        public override void Update(GameTime gameTime)
        {
            Begin();
            
            foreach (ECSEntity entity in Entities)
            {
                Process(entity);
            }

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
            bool onReleaseNotHovered = CheckReleaseNotHovered(transform, interaction);
            bool onClick = CheckClick(transform, interaction);
            bool onDragStart = CheckDragStart(transform, interaction);
            bool onDragOver = CheckDragOver(transform, interaction);
            bool onDrop = CheckDrop(transform, interaction);
            bool onMove = CheckMove(transform, interaction);

            // Trigger Events
            if (onHover) interaction.OnHover(BuildMouseEvent(entity.Id));
            if (onOver) interaction.OnOver(BuildMouseEvent(entity.Id));
            if (onPress) interaction.OnPress(BuildMouseEvent(entity.Id));
            if (onRelease) interaction.OnRelease(BuildMouseEvent(entity.Id));
            if (onReleaseNotHovered) interaction.OnReleaseNotHovered(BuildMouseEvent(entity.Id));
            if (onClick) interaction.OnClick(BuildMouseEvent(entity.Id));
            if (onDragStart) interaction.OnDragStart(BuildDragEvent(entity.Id));
            if (onDragOver) interaction.OnDragOver(BuildDragEvent(entity.Id));
            if (onDrop) interaction.OnDrop(BuildDragEvent(entity.Id));
            if (onMove) interaction.OnMove(BuildMouseEvent(entity.Id));
        }

        // Mouse handlers

        private bool CheckHover(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (!interaction.IsHovered && IsEntityHovered(transform, CurrentMouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckOver(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsHovered && !IsEntityHovered(transform, CurrentMouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckPress(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (!interaction.IsPressed && IsEntityPressed(transform, CurrentMouseState) && IsMouseReleased(PreviousMouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckRelease(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsPressed && !IsEntityPressed(transform, CurrentMouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckReleaseNotHovered(Transform2DComponent transform, Interaction2DComponent interaction)
        {           
            if (IsMousePressed(PreviousMouseState) && !IsMousePressed(CurrentMouseState) && interaction.IsPressed)
            {
                return true;
            }
            return false;
        }

        private bool CheckClick(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsPressed && IsEntityHovered(transform, CurrentMouseState) && IsMouseReleased(CurrentMouseState))
            {
                return true;
            }
            return false;
        }

        private bool CheckMove(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsPressed && CurrentMouseState != PreviousMouseState)
            {
                return true;
            }
            return false;
        }

        // Drag handlers

        private bool CheckDragStart(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (!interaction.IsDraged && IsEntityPressed(transform, CurrentMouseState)
                && IsMouseReleased(PreviousMouseState))
            {
                if (this.OnlyOneDragedElement)
                {
                    this.OneDraged = true;
                }
                return true;
            }

            return false;
        }


        public bool CheckDragOver(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsDraged && !IsEntityPressed(transform, CurrentMouseState) &&
                IsMousePressed(CurrentMouseState))
            {
                return true;
            }

            return false;
        }

        private bool CheckDrop(Transform2DComponent transform, Interaction2DComponent interaction)
        {
            if (interaction.IsDraged && IsMouseReleased(CurrentMouseState))
            {
                if (this.OnlyOneDragedElement)
                {
                    this.OneDraged = false;
                }
                return true;
            }

            return false;
        }

        private MouseEventArgs BuildMouseEvent(int entityId)
        {
            return new MouseEventArgs()
            {
                EntityId = entityId,
                MouseState = CurrentMouseState
            };
        }

        private DragEventArgs BuildDragEvent(int entityId)
        {
            return new DragEventArgs()
            {
                EntityId = entityId,
                MouseState = CurrentMouseState
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
            var bounds = new Rectangle(transform.TopLeftCornerPosition.ToPoint(), transform.ScaleSize.ToPoint());

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
