using UnityEngine;
using UnityEngine.EventSystems;

public class MinimapImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MinimapManager _manager;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_manager.Enlarged)
        {
            _manager.ShrinkMinimap();
        }
        else
        {
            _manager.EnlargeMinimap();
        }
    }
}
