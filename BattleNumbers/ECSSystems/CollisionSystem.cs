using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.LogService;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSSystems
{
    public class CollisionSystem : ECSSystem, IUpdateSystem
    {

        public class ColliderPair
        {
            public CollisionComponent collider { get; set; }
            public Transform2DComponent body { get; set; }

            public ColliderPair()
            {
                collider = new CollisionComponent();
                body = new Transform2DComponent();
            }
        }

        List<ColliderPair> CollisionActors;

        public CollisionSystem(ECSWorld world)
        {
            this.BindWorld(world);
            AddRequiredComponents(new List<Type>() { typeof(Transform2DComponent), typeof(CollisionComponent) });
            CollisionActors = new List<ColliderPair>();
        }

        private void CheckCollision(ColliderPair main, ColliderPair other)
        {
            Rectangle mainRect = main.body.ScaleBounds;
            Rectangle otherRect = other.body.ScaleBounds;

            if (mainRect.Intersects(otherRect))
            {
                LogService.LogService log = (LogService.LogService)this.World.Game.Services.GetService(typeof(ILogService));
                log.Log("Collision");
                Rectangle result = Rectangle.Intersect(mainRect, otherRect);
            }
        }

        private void CheckCollisions(ColliderPair element, List<ColliderPair> others)
        {
            foreach (ColliderPair otherPair in others)
            {
                CheckCollision(element, otherPair);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // First bucle will create actual world of collision element
            foreach (ECSEntity entity in Entities)
            {
                UpdateEntityRegistration(entity);

                if (entity.HasComponent<CollisionComponent>() && entity.HasComponent<Transform2DComponent>())
                {
                    CollisionComponent collider = entity.GetComponent<CollisionComponent>();
                    Transform2DComponent body = entity.GetComponent<Transform2DComponent>();
                    ColliderPair actor = new ColliderPair();
                    actor.collider = collider;
                    actor.body = body;
                    CollisionActors.Add(actor);
                }
            }

            for (int i = CollisionActors.Count - 1; i >= 1; i--)
            {
                List<ColliderPair> subList = CollisionActors.GetRange(0, CollisionActors.Count - 1);
                CheckCollisions(CollisionActors[i], subList);
                CollisionActors.RemoveAt(i);
            }

            CollisionActors = new List<ColliderPair>();
        }
    }
}
