using BattleNumbers.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class CollisionComponent : IECSComponent
    {

        public CollisionComponent()
        { }

        public CollisionComponent(ColliderType type)
        {
            this.type = type;
        }

        public enum ColliderType
        {
            attractor,
            rigid,            
        }

        public class CollisionElement
        {
            Point collisionPoint;
            ColliderType type;
            float overlappedArea;
        }

        public ColliderType type { get; set; }

        List<CollisionElement> actualCollisionList;
        List<CollisionElement> previousCollisionList;

        public event EventHandler<CollisionElement> EnterCollision;
        public event EventHandler<CollisionElement> MaintainCollision;
        public event EventHandler<CollisionElement> ExitCollision;

        internal void OnEnterCollision(CollisionElement e)
        {            
            EnterCollision?.Invoke(this, e);
        }

        internal void OnMaintainCollision(CollisionElement e)
        {
            MaintainCollision?.Invoke(this, e);
        }

        internal void OnExitCollision(CollisionElement e)
        {
            ExitCollision?.Invoke(this, e);
        }
    }
}
