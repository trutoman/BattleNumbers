using BattleNumbers.ECS;
using Microsoft.Xna.Framework;

namespace BattleNumbers.ECSComponents
{
    public class Transform2DComponent : IECSComponent
    {
        public Transform2DComponent Parent { get; set; }
        public Vector2 TopLeftCornerPosition { get; private set; }
        public Vector2 CenteredOrigin { get; private set; }

        private Vector2 position;
        public Vector2 Position { 
            get { return this.position; }
            set
            {
                TopLeftCornerPosition = new Vector2(value.X - this.Size.X / 2, value.Y - this.Size.Y / 2);
                CenteredOrigin = value - TopLeftCornerPosition;
                this.position = value;
            }
        }
        private Vector2 size;
        public Vector2 Size 
        { 
            get { return this.size; }
            set
            {
                TopLeftCornerPosition = new Vector2(this.Position.X - value.X / 2, this.Position.Y - value.Y / 2);
                CenteredOrigin = this.Position - TopLeftCornerPosition;
                this.size = value;
            } 
        }
        // TODO : Modify scale should modify bound to sccalebounds??
        private Vector2 scale;
        public Vector2 Scale 
        {
            get { return scale; }
            set
            { 
                Vector2 newSize = Vector2.Multiply(Size, Scale);
                this.TopLeftCornerPosition = new Vector2(this.Position.X - newSize.X / 2, this.Position.Y - newSize.Y / 2);
                this.CenteredOrigin = this.Position - TopLeftCornerPosition;
                this.scale = value;
            } 
        }
        public float Rotation { get; set; }

        public Transform2DComponent()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Size = Vector2.One;
            Rotation = 0;

            Parent = null;
        }

        public bool HasParent => Parent != null;

        public Vector2 AbsolutePosition => HasParent ? Parent.AbsolutePosition + Position : Position;

        // Centered Bounds
        public Rectangle Bounds => new Rectangle(TopLeftCornerPosition.ToPoint(), Size.ToPoint());        

        public Vector2 ScaleSize => Vector2.Multiply(Size, Scale);

        public Rectangle ScaleBounds => new Rectangle(TopLeftCornerPosition.ToPoint(), ScaleSize.ToPoint());

        public void ScaleBy(float scaleX, float scaleY)
        {
            float _scaleX = Scale.X + scaleX;
            float _scaleY = Scale.Y + scaleY;
            Scale = new Vector2(_scaleX, _scaleY);
        }

        public void RotateBy(float amountInDegrees)
        {
            if (amountInDegrees != 0)
            {
                float rotation = (Rotation + amountInDegrees) % 360;
                Rotation = rotation;
            }
        }

        public static Vector2 Up => new Vector2(0, -1);
        public static Vector2 Down => new Vector2(0, 1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);

        public override string ToString()
        {
            return $"position : {this.Position}, topLeft : {this.TopLeftCornerPosition}, bounds : {this.Bounds}, origin : {this.CenteredOrigin}, scale : {this.scale}";

        }
    }
}
