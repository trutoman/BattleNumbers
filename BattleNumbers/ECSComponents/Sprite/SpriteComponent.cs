using BattleNumbers.ECS;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteData;

namespace BattleNumbers.ECSComponents
{
    public class SpriteComponent : IECSComponent
    {
        public TextureRegion2D TextureRegion { get; protected set; }

        public bool IsVisible { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }
        public float Depth { get; set; }
        public float Alpha { get; set; }

        public SpriteComponent(TextureRegion2D textureRegion)
        {
            TextureRegion = textureRegion;
            IsVisible = true;
            Color = Color.White;
            Effects = SpriteEffects.None;
            Depth = 0f;
            Alpha = 1.0f;
        }
    }
}
