using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private Transform _minimapTransform;
    [SerializeField] private CinemachineVirtualCamera _minimapVirtualCamera;
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private Vector3 _smallScale, _largeScale, _smallPosition, _largePosition;
    [SerializeField] private Vector3 _input;
    [SerializeField] private float _cameraMoveSpeed;
    [SerializeField] private MinimapImage _minimap;

    private bool _enlarged = false;

    public bool Enlarged { get => _enlarged; }


    private void OnEnable()
    {
        EnemySpawner.OnCombatStart += HideMinimap;
        EnemySpawner.OnCombatEnd += ShowMinimap;
    }

    private void OnDisable()
    {
        EnemySpawner.OnCombatStart -= HideMinimap;
        EnemySpawner.OnCombatEnd -= ShowMinimap;
    }

    public void EnlargeMinimap()
    {
        _minimapTransform.localPosition = _largePosition;
        _minimapTransform.localScale = _largeScale;
        _enlarged = true;
        Time.timeScale = 0;
        _minimapVirtualCamera.Follow = null;
    }

    public void ShrinkMinimap()
    {
        _minimapTransform.localPosition = _smallPosition;
        _minimapTransform.localScale = _smallScale;
        _enlarged = false;
        Time.timeScale = 1;
        _minimapVirtualCamera.Follow = CharacterManager.Instance.Player.transform;
    }

    private void Update()
    {
        //get input

        if (_enlarged)
            _minimapVirtualCamera.ForceCameraPosition(_minimapCamera.transform.position + _input * Time.unscaledDeltaTime * _cameraMoveSpeed, Quaternion.identity);
    }

    private void HideMinimap()
    {
        _minimap.gameObject.SetActive(false);
    }

    private void ShowMinimap()
    {
        _minimap.gameObject.SetActive(true);
    }


}
