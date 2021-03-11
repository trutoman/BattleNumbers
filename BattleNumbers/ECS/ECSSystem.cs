using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleNumbers.ECS
{
    public abstract class ECSSystem
    {
        private HashSet<int> registeredEntityIds;
        protected ECSWorld World;
        List<List<Type>> requiredComponents;

        protected List<ECSEntity> Entities
        {
            get
            {
                IEnumerable<ECSEntity> result = from id in registeredEntityIds
                                                where World.EntityExists(id)
                                                select World.GetEntityById(id);
                return result.ToList();
            }
        }

        protected ECSSystem()
        {
            registeredEntityIds = new HashSet<int>();
            requiredComponents = new List<List<Type>>();
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
            bool matches = true;

            foreach (List<Type> combination in requiredComponents)
            {
                matches = true;
                foreach (Type requiredType in combination)
                {
                    if (!entity.HasComponent(requiredType))
                    {
                        matches = false;
                        break;
                    }
                }
                if (matches)
                {
                    return true;
                }
            }
            return matches;
        }

        protected void AddRequiredComponents(List<Type> componentList)
        {
            requiredComponents.Add(componentList);
        }

        public abstract void Update(GameTime gametime);

        public virtual void DeleteEntity(int id)
        {
            if (registeredEntityIds.Contains(id))
            {
                registeredEntityIds.Remove(id);
            }
        }

        public void BindWorld(ECSWorld manager)
        {
            World = manager;
        }

    }
}
