using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Weapon Stats/Punch")]
public class PunchStats : WeaponStats
{
    [SerializeField] private float _attackSpeed = 1f;

    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
}