using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECS
{
    public abstract class ECSArchetype
    {
        private Type[] _componentTypes;

        protected ECSArchetype(Type[] componentTypes)
        {
            _componentTypes = componentTypes;
        }

        public abstract ECSEntity CreateEntity(ECSEntity entity, params object[] args);

        public Type[] GetComponentTypes() => _componentTypes;
    }
}
