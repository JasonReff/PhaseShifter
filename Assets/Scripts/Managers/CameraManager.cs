using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera _virtualCamera, _minimapVirtualCamera;


    public void FollowPlayer()
    {
        _virtualCamera.Follow = CharacterManager.Instance.Player.transform;
        _minimapVirtualCamera.Follow = CharacterManager.Instance.Player.transform;
    }

    
    
}