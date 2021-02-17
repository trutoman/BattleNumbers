using BattleNumbers.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents.Sprite
{
    public class AnimatedSpriteComponent : SpriteComponent, IECSComponent
    {
        private readonly SpriteSheet _spriteSheet;
        private SpriteSheetAnimation _currentAnimation;

        public AnimatedSpriteComponent(SpriteSheet spriteSheet)
            : base(spriteSheet[0])
        {
            _spriteSheet = spriteSheet;
        }

        public SpriteSheetAnimation Play(string name, Action onCompleted = null)
        {
            _currentAnimation = _spriteSheet.CreateAnimation(name);

            if (onCompleted != null)
                _currentAnimation.OnCompleted += onCompleted;

            return _currentAnimation;
        }

        public void Pause()
        {
            _currentAnimation?.Pause();
        }

        public void Stop()
        {
            _currentAnimation?.Stop();
        }

        public void Rewind()
        {
            _currentAnimation?.Rewind();
        }

        internal SpriteSheetAnimation CurrentAnimation => _currentAnimation;

        internal void SetTextureRegion(TextureRegion2D textureRegion) => TextureRegion = textureRegion;
    }
}
