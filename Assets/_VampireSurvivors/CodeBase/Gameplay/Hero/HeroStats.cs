using R3;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Gameplay.Hero
{
    public class HeroStats
    {
        private readonly ReactiveProperty<int> _maxHp = new();
        private readonly ReactiveProperty<int> _damage = new();
        private readonly ReactiveProperty<float> _attackRate = new();
        private readonly ReactiveProperty<float> _moveSpeed = new();

        public ReadOnlyReactiveProperty<int> MaxHp => _maxHp;
        public ReadOnlyReactiveProperty<int> Damage => _damage;
        public ReadOnlyReactiveProperty<float> AttackRate => _attackRate;
        public ReadOnlyReactiveProperty<float> MoveSpeed => _moveSpeed;

        public void UpdateMoveSpeed(float value)
        {
            _moveSpeed.Value = Mathf.Max(value, 0);
        }
    }
}
