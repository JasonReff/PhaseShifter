using UnityEngine;

public abstract class WeaponStats : ScriptableObject
{
    [SerializeField] 
    private float maxChargeTime;
    [SerializeField]
    private float attackDuration;
    [SerializeField] 
    private float attackCooldown;
    [SerializeField]
    private float knockback;
    [SerializeField]
    private float chargeTimeMultiplier;

    public float AttackDuration { get => attackDuration; set => attackDuration = value; }
    public float Knockback { get => knockback; set => knockback = value; }
    public float ChargeTimeMultiplier { get => chargeTimeMultiplier; set => chargeTimeMultiplier = value; }
    public float MaxChargeTime { get => maxChargeTime; set => maxChargeTime = value; }
    public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
}
