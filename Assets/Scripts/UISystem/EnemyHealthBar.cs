using Characters.Enemies;
using Characters.Enemies.SerializableData;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image outline;
    [SerializeField] private Image fill;

    [SerializeField] private Enemy enemy;

    private EnemyStats _data;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        
        ((IDamageable)enemy).OnTakeDamage += UpdateHealthBar;
    }

    private void Start()
    {
        _data = enemy.Stats;
    }

    private void UpdateHealthBar()
    {
        float ratio = _data.CurrentHP / _data.StaticData.MaxHP;
        fill.fillAmount = ratio;
    }
}