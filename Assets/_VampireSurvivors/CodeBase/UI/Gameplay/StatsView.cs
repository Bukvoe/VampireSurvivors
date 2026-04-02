using System.ComponentModel.DataAnnotations;
using TMPro;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.UI.Gameplay
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField, Required] private TextMeshProUGUI _maxHpLabel;
        [SerializeField, Required] private TextMeshProUGUI _damageLabel;
        [SerializeField, Required] private TextMeshProUGUI _attackRateLabel;
        [SerializeField, Required] private TextMeshProUGUI _moveSpeedLabel;

        public void UpdateMaxHp(int maxHp)
        {
            _maxHpLabel.text = $"{maxHp}";
        }

        public void UpdateDamage(int damage)
        {
            _damageLabel.text = $"{damage}";
        }

        public void UpdateAttackRate(float attackRate)
        {
            _attackRateLabel.text = $"{attackRate}";
        }

        public void UpdateMoveSpeed(float moveSpeed)
        {
            _moveSpeedLabel.text = $"{moveSpeed}";
        }
    }
}
