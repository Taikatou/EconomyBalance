using System.Collections.Generic;
using RPG.Stats;

namespace Assets.RPG.Scripts.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}