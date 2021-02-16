using BattleNumbers.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class TokenTypeComponent : IECSComponent
    {
        public string Name { set; get; }
        public TokenTypeComponent() { }
    }
}
