using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Enemy Stats/Enemy Melee")]
public class EnemyMeleeStats : WeaponStats
{
    [SerializeField] private float _attackSpeed;

    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
}