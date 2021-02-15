using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECS
{
    public abstract class Archetype
    {
        private Type[] _componentTypes;

        protected Archetype(Type[] componentTypes)
        {
            _componentTypes = componentTypes;
        }

        public abstract ECSEntity CreateEntity(ECSEntity entity, params object[] args);

        public Type[] GetComponentTypes() => _componentTypes;
    }
}
