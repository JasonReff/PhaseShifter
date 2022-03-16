using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private MinimapManager _minimap;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera, _minimapVirtualCamera;


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

    public void FollowPlayer()
    {
        _virtualCamera.Follow = CharacterManager.Instance.Player.transform;
        _minimapVirtualCamera.Follow = CharacterManager.Instance.Player.transform;
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