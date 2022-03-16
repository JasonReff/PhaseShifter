using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private float _speed;
    void Update()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + _rb.velocity * _speed * Time.deltaTime, _rawImage.uvRect.size);
    }
}
