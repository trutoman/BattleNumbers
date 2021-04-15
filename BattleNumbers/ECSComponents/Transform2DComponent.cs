using BattleNumbers.ECS;
using Microsoft.Xna.Framework;

namespace BattleNumbers.ECSComponents
{
    public class Transform2DComponent : IECSComponent
    {
        public bool Limited { get; set; }
        public Vector2 Limits { get; set; }        
        public Transform2DComponent Parent { get; set; }
        public Vector2 TopLeftCornerPosition { get; private set; }

        private Vector2 position;
        public Vector2 Position { 
            get { return this.position; }
            set
            {
                Vector2 rectifiedPosition = value;
                if (Limited)
                {
                    rectifiedPosition = RectifyPosition(value);
                    TopLeftCornerPosition = new Vector2(rectifiedPosition.X - this.ScaleSize.X / 2, rectifiedPosition.Y - this.ScaleSize.Y / 2);
                }
                
                this.position = rectifiedPosition;
            }
        }
        private Vector2 size;
        public Vector2 Size 
        { 
            get { return this.size; }
            set
            {
                Vector2 rectifiedPosition = this.Position;
                this.size = value;
                if (Limited)
                {
                    rectifiedPosition = RectifyPosition(this.Position);
                }
                TopLeftCornerPosition = new Vector2(rectifiedPosition.X - this.ScaleSize.X / 2, rectifiedPosition.Y - this.ScaleSize.Y / 2);              
            } 
        }
        // TODO : Modify scale should modify bound to sccalebounds??
        private Vector2 scale;
        public Vector2 Scale 
        {
            get { return scale; }
            set
            {
                Vector2 rectifiedPosition = this.Position;
                this.scale = value;
                if (Limited)
                {
                    rectifiedPosition = RectifyPosition(this.Position);
                }
                this.TopLeftCornerPosition = new Vector2(rectifiedPosition.X - this.ScaleSize.X / 2, rectifiedPosition.Y - this.ScaleSize.Y / 2);
            } 
        }
        public float Rotation { get; set; }

        public Transform2DComponent()
        {
            Size = Vector2.One;
            Position = Vector2.Zero;
            Scale = Vector2.One;            
            Rotation = 0;

            this.Limits = Vector2.Zero;

            Parent = null;
        }
        public Transform2DComponent(Vector2 position)
        {
            Size = Vector2.One;
            Position = position;
            Scale = Vector2.One;
            Rotation = 0;

            this.Limits = Vector2.Zero;

            Parent = null;
        }
        public Transform2DComponent(Vector2 position, Vector2 limits, Vector2 size)
        {            
            Limits = limits;
            Limited = (Limits != Vector2.Zero);
            Size = size;
            Position = position;
            Scale = Vector2.One;
            Rotation = 0;

            Parent = null;
        }

        public bool HasParent => Parent != null;

        public Vector2 AbsolutePosition => HasParent ? Parent.AbsolutePosition + Position : Position;

        // Centered Bounds
        public Rectangle Bounds => new Rectangle(TopLeftCornerPosition.ToPoint(), Size.ToPoint());        

        public Vector2 ScaleSize => Vector2.Multiply(Size, Scale);

        public Rectangle ScaleBounds => new Rectangle(TopLeftCornerPosition.ToPoint(), ScaleSize.ToPoint());


        private Vector2 RectifyPosition(Vector2 input)
        {
            Vector2 returnValue = Vector2.Zero;

            if (input.X - ScaleSize.X/2 < 0)
            {
                returnValue.X = 0 + ScaleSize.X / 2;
            }
            else if (input.X > Limits.X - ScaleSize.X / 2)
            {
                returnValue.X = Limits.X - ScaleSize.X / 2;
            }
            else
            {
                returnValue.X = input.X;
            }
            if (input.Y - ScaleSize.Y / 2 < 0)
            {
                returnValue.Y = 0 + ScaleSize.Y / 2;
            }
            else if (input.Y > Limits.Y - ScaleSize.Y / 2)
            {
                returnValue.Y = Limits.Y - ScaleSize.Y / 2;
            }
            else
            {
                returnValue.Y = input.Y;
            }
            
            return returnValue;
        }

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
            return $"\n\tposition : {this.Position}\n\ttopLeft : {this.TopLeftCornerPosition}\n\tbounds : {this.Bounds}\n\tscale : {this.scale}";

        }
    }
}
