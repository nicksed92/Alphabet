using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoResize : MonoBehaviour
{
    [SerializeField] private float _defaultSize = 5f;

    private Camera _camera;
    private float _screenWidth;
    private float _screenHeight;
    private float _aspectRatio;

    void Start()
    {
        _camera = GetComponent<Camera>();

        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        _aspectRatio = _screenWidth / _screenHeight;

        SetCameraSize();

    }

    private void SetCameraSize()
    {
        float newSize = _defaultSize;

        if (_aspectRatio <= 1.5f) newSize = 7.5f;
        else if (_aspectRatio > 1.5f && _aspectRatio <= 1.7f) newSize = 6.2f;
        else if (_aspectRatio > 1.7f && _aspectRatio <= 1.9f) newSize = 5.9f;
        else if (_aspectRatio > 1.9f) newSize = 5.4f;

        _camera.orthographicSize = newSize;
    }
}