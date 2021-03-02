using BattleNumbers.ECSSystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleNumbers.ECS
{
    public class ECSWorld
    {
        public BattleNumbers Game { get; set; }
        private Dictionary<int, ECSEntity> Entities;
        private Dictionary<Type, ECSSystem> Systems;
        private List<int> toDelete;
        private int currentId = 0;

        public ECSWorld(BattleNumbers game)
        {
            this.Game = game;
            this.Entities = new Dictionary<int, ECSEntity>();
            this.Systems = new Dictionary<Type, ECSSystem>();
            this.toDelete = new List<int>();
        }

        public ECSEntity CreateEntity()
        {
            ECSEntity entity = new ECSEntity(currentId++);
            Entities[entity.Id] = entity;
            return entity;
        }

        public ECSEntity CreateEntity(ECSArchetype archetype, params object[] args)
        {
            ECSEntity entity = new ECSEntity(currentId++);
            archetype.CreateEntity(entity, args);            
            Entities[entity.Id] = entity;
            return entity;
        }

        public void DeleteEntity(int id)
        {
            toDelete.Add(id);
        }

        public ECSEntity GetEntityById(int id)
        {
            return Entities[id];
        }

        public bool EntityExists(int id)
        {
            return Entities.ContainsKey(id);
        }

        public void AddSystem(ECSSystem system)
        {
            Systems[system.GetType()] = system;
            system.BindWorld(this);
        }

        public T GetSystem<T>() where T : ECSSystem
        {
            return (T)Systems[typeof(T)];
        }

        public void Update(GameTime gameTime)
        {
            foreach (ECSSystem system in Systems.Values)
            {
                system.UpdateAll(gameTime);
            }
            Flush();
        }

        public void Draw(GameTime gameTime)
        {
            IDrawSystem drawableObject;

            foreach (ECSSystem system in Systems.Values)
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

                foreach (ECSSystem system in Systems.Values)
                {
                    system.DeleteEntity(id);
                }

                Entities.Remove(id);
            }
            toDelete.Clear();
        }

        private void UpdateEntityRegistration(ECSEntity entity)
        {
            foreach (ECSSystem system in Systems.Values)
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
