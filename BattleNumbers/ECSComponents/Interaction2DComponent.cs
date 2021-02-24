using BattleNumbers.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class MouseEventArgs : EventArgs
    {
        public MouseState MouseState { get; set; }
        public int EntityId { get; set; }
    }

    public class DragEventArgs : MouseEventArgs
    {
    }

    public class Interaction2DComponent : IECSComponent
    {
        // Properties
        public bool IsHovered { get; private set; }
        public bool IsPressed { get; private set; }
        public Point RelativePressedPoint { get; set; }
        public Point Origin { get; set; }
        public bool IsDraged { get; private set; }

        // Mouse Events
        public event EventHandler<MouseEventArgs> Hover;
        public event EventHandler<MouseEventArgs> Over;
        public event EventHandler<MouseEventArgs> Press;
        public event EventHandler<MouseEventArgs> Release;
        public event EventHandler<MouseEventArgs> Click;
        public event EventHandler<MouseEventArgs> Move;

        // Drag Events
        public event EventHandler<DragEventArgs> DragStart;
        public event EventHandler<DragEventArgs> DragOver;
        public event EventHandler<DragEventArgs> Drop;

        // Trigger Mouse Events
        internal void OnHover(MouseEventArgs e)
        {
            IsHovered = true;
            Hover?.Invoke(this, e);
        }
        internal void OnOver(MouseEventArgs e)
        {
            IsHovered = false;
            Over?.Invoke(this, e);
        }

        internal void OnPress(MouseEventArgs e)
        {            
            IsPressed = true;            
            Press?.Invoke(this, e);
        }

        internal void OnRelease(MouseEventArgs e)
        {
            IsPressed = false;
            Release?.Invoke(this, e);
        }
        internal void OnClick(MouseEventArgs e)
        {
            IsPressed = false;
            Click?.Invoke(this, e);
        }

        internal void OnMove(MouseEventArgs e)
        {
            IsPressed = true;
            Move?.Invoke(this, e);
        }

        // Trigger Drag Events
        internal void OnDragStart(DragEventArgs e)
        {
            IsDraged = true;            
            DragStart?.Invoke(this, e);
        }

        internal void OnDragOver(DragEventArgs e)
        {
            IsDraged = false;
            // This is executed after OnRelease event so we dont modify RelativePressed before execute OnRelease handler
            // this.RelativePressedPoint = Point.Zero;
            DragOver?.Invoke(this, e);
        }

        internal void OnDrop(DragEventArgs e)
        {
            IsDraged = false;
            Drop?.Invoke(this, e);
        }
    }
}
