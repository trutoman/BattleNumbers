using BattleNumbers.ECSSystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleNumbers.ECS
{
    public class ECSWorld
    {
        public BattleNumbers game { get; set; }
        private Dictionary<int, ECSEntity> entities;
        private Dictionary<Type, ECSSystem> systems;
        private List<int> toDelete;
        private int currentId = 0;

        public ECSWorld(BattleNumbers game)
        {
            this.game = game;
            this.entities = new Dictionary<int, ECSEntity>();
            this.systems = new Dictionary<Type, ECSSystem>();
            this.toDelete = new List<int>();
        }

        public ECSEntity AddAndGetEntity()
        {
            ECSEntity entity = new ECSEntity(currentId++);
            entities[entity.Id] = entity;
            return entity;
        }

        public ECSEntity AddAndGetEntity(ECSArchetype archetype, params object[] args)
        {
            ECSEntity entity = new ECSEntity(currentId++);
            archetype.CreateEntity(entity, args);            
            entities[entity.Id] = entity;
            return entity;
        }

        public void DeleteEntity(int id)
        {
            toDelete.Add(id);
        }

        public ECSEntity GetEntityById(int id)
        {
            return entities[id];
        }

        public bool EntityExists(int id)
        {
            return entities.ContainsKey(id);
        }

        public void AddSystem(ECSSystem system)
        {
            systems[system.GetType()] = system;
            system.BindWorld(this);
        }

        public T GetSystem<T>() where T : ECSSystem
        {
            return (T)systems[typeof(T)];
        }

        public void Update(GameTime gameTime)
        {
            foreach (ECSSystem system in systems.Values)
            {
                system.UpdateAll(gameTime);
            }
            Flush();
        }

        public void Draw(GameTime gameTime)
        {
            IDrawSystem drawableObject;

            foreach (ECSSystem system in systems.Values)
            {
                if (system is IDrawSystem)
                {
                    drawableObject = (IDrawSystem)system;
                    drawableObject.Draw(gameTime);
                }
            }
            Flush();
        }

        private void Flush()
        {
            foreach (int id in toDelete)
            {
                if (!EntityExists(id)) //safeguard against deleting twice
                    continue;

                foreach (ECSSystem system in systems.Values)
                {
                    system.DeleteEntity(id);
                }

                entities.Remove(id);
            }
            toDelete.Clear();
        }

        private void UpdateEntityRegistration(ECSEntity entity)
        {
            foreach (ECSSystem system in systems.Values)
            {
                system.UpdateEntityRegistration(entity);
            }
        }

        public void AddComponentToEntity(ECSEntity entity, IECSComponent component)
        {
            entity.AttachComponent(component);
            UpdateEntityRegistration(entity);
        }

        public void RemoveComponentFromEntity<T>(ECSEntity entity) where T : IECSComponent
        {
            entity.DettachComponent<T>();
            UpdateEntityRegistration(entity);
        }
    }
}
