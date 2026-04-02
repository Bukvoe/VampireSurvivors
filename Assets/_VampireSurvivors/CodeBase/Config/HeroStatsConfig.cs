using UnityEngine;

namespace _VampireSurvivors.CodeBase.Config
{
    [CreateAssetMenu(menuName = "Configs/HeroStatsConfig")]
    public class HeroStatsConfig : ScriptableObject
    {
        public int MaxHp;
        public int Damage;
        public float AttackRate;
        public float MoveSpeed;
    }
}
