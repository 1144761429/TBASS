using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image outline;
    [SerializeField] private Image fill;

    [SerializeField] private EnemyStats enemyStats;

    private EnemyDataSO _data;

    private void Awake()
    {
        enemyStats.TakeDamageCallback += UpdateHealthBar;
    }

    private void Start()
    {
        _data = enemyStats.RuntimeData;
    }

    private void UpdateHealthBar()
    {
        float ratio = _data.CurrentHp / _data.MaxHp;
        fill.fillAmount = ratio;
    }
}