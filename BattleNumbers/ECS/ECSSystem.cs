using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleNumbers.ECS
{
    public abstract class ECSSystem
    {
        private HashSet<int> registeredEntityIds;
        private List<Type> requiredComponents;
        protected World Manager;

        protected List<ECSEntity> Entities
        {
            get
            {
                IEnumerable<ECSEntity> result = from id in registeredEntityIds
                                             where Manager.EntityExists(id)
                                             select Manager.GetEntityById(id);

                return result.ToList();
            }
        }

        protected ECSSystem()
        {
            registeredEntityIds = new HashSet<int>();
            requiredComponents = new List<Type>();
        }

        public void UpdateEntityRegistration(ECSEntity entity)
        {
            bool matches = Matches(entity);
            if (registeredEntityIds.Contains(entity.Id))
            {
                if (!matches)
                {
                    registeredEntityIds.Remove(entity.Id);
                }
            }
            else
            {
                if (matches)
                {
                    registeredEntityIds.Add(entity.Id);
                }
            }
        }

        private bool Matches(ECSEntity entity)
        {
            foreach (Type required in requiredComponents)
            {
                if (!entity.HasComponent(required))
                    return false;
            }
            return true;
        }

        protected void AddRequiredComponent<T>() where T : ECSComponent
        {
            requiredComponents.Add(typeof(T));
        }

        public virtual void UpdateAll(float deltaTime)
        {
            foreach (ECSEntity entity in Entities)
            {
                UpdateEntityRegistration(entity);
                Update(entity, deltaTime);
            }
        }

        protected abstract void Update(ECSEntity entity, float deltaTime);

        public virtual void DeleteEntity(int id)
        {
            if (registeredEntityIds.Contains(id))
            {
                registeredEntityIds.Remove(id);
            }
        }

        public void BindManager(World manager)
        {
            Manager = manager;
        }

    }
}
