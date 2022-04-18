using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class UltimatePickup : Pickup
{
    [SerializeField] private int _chargeAmount;
    [SerializeField] private float _lifetime, _fadeTime;
    [SerializeField] private SpriteRenderer _renderer;
    private Tween _fade;

    public static event Action<int> OnPickedUp;

    private void Awake()
    {
        StartCoroutine(LifetimeCoroutine());
    }
    protected override void OnPickup()
    {
        base.OnPickup();
        _fade.Kill();
        OnPickedUp?.Invoke(_chargeAmount);
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifetime);
        _fade = _renderer.DOFade(0, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        Destroy(gameObject);
    }
}
