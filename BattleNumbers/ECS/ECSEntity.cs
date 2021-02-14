using System;
using System.Collections.Generic;

namespace BattleNumbers.ECS
{
    public class ECSEntity
    {
        public int Id { get; private set; }
        private Dictionary<Type, ECSComponent> components;

        public ECSEntity(int id)
        {
            Id = id;
            components = new Dictionary<Type, ECSComponent>();
        }

        internal void AddComponent(ECSComponent component)
        {
            components[component.GetType()] = component;
        }

        internal void RemoveComponent<T>() where T : ECSComponent
        {
            components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : ECSComponent
        {
            return (T)components[typeof(T)];
        }

        public bool HasComponent(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        public bool HasComponent<T>() where T : ECSComponent
        {
            return components.ContainsKey(typeof(T));
        }
    }
}
