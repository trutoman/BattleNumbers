using System;
using System.Collections.Generic;

namespace BattleNumbers.ECS
{
    public class ECSEntity
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        private Dictionary<Type, IECSComponent> components;

        public ECSEntity(int id)
        {
            Id = id;
            components = new Dictionary<Type, IECSComponent>();
        }

        public void SetName(string name) => Name = name;

        internal void AttachComponent(IECSComponent component)
        {
            components[component.GetType()] = component;
        }

        internal void DettachComponent<T>() where T : IECSComponent
        {
            components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : IECSComponent
        {
            return (T)components[typeof(T)];
        }

        public bool HasComponent(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        public bool HasComponent<T>() where T : IECSComponent
        {
            return components.ContainsKey(typeof(T));
        }
    }
}
