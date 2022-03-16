using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] private float _freezeTimeDuration, _timeSlow, _cameraShakeModifier, _zoomInTime;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private PixelPerfectCamera _pixelPerfect;
    private void OnEnable()
    {
        Attack.OnDamageDealt += AttackEffects;
    }

    private void OnDisable()
    {
        Attack.OnDamageDealt -= AttackEffects;
    }

    private void AttackEffects(Vector3 vector3)
    {
        StopTime();
        ShakeCamera(vector3);
    }

    private void StopTime()
    {
        StartCoroutine(FreezeTimeCoroutine());
    }

    private IEnumerator FreezeTimeCoroutine()
    {
        Time.timeScale = _timeSlow;
        yield return new WaitForSecondsRealtime(_freezeTimeDuration);
        Time.timeScale = 1;
    }

    private void ShakeCamera(Vector3 direction)
    {
        _impulseSource.GenerateImpulse(direction * _cameraShakeModifier);
    }

    //private IEnumerator CameraZoom()
    //{
    //    float current = 0;
    //    while (current < _zoomInTime)
    //    {
    //        current += Time.unscaledDeltaTime;
    //        _pixelPerfect.assetsPPU = 72;
    //        yield return null;
    //    }
    //    _pixelPerfect.assetsPPU = 64;
    //}
}
