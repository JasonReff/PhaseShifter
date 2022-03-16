using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinimapManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _minimapTransform;
    [SerializeField] private CinemachineVirtualCamera _minimapVirtualCamera;
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private Vector3 _smallScale, _largeScale, _smallPosition, _largePosition;
    [SerializeField] private Vector3 _input;
    [SerializeField] private float _cameraMoveSpeed;
    private bool _enlarged = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_enlarged)
        {
            ShrinkMinimap();
        }
        else
        {
            EnlargeMinimap();
        }
    }

    private void EnlargeMinimap()
    {
        _minimapTransform.localPosition = _largePosition;
        _minimapTransform.localScale = _largeScale;
        _enlarged = true;
        Time.timeScale = 0;
        _minimapVirtualCamera.Follow = null;
    }

    private void ShrinkMinimap()
    {
        _minimapTransform.localPosition = _smallPosition;
        _minimapTransform.localScale = _smallScale;
        _enlarged = false;
        Time.timeScale = 1;
        _minimapVirtualCamera.Follow = CharacterManager.Instance.Player.transform;
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        if (_enlarged)
            _minimapVirtualCamera.ForceCameraPosition(_minimapCamera.transform.position + _input * Time.unscaledDeltaTime * _cameraMoveSpeed, Quaternion.identity);
    }
}
