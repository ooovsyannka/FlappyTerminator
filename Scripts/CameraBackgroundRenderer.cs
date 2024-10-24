using System.Collections;
using UnityEngine;

public class CameraBackgroundRenderer : MonoBehaviour
{
    [SerializeField] private float _changeColorDuration;
    [SerializeField] private float _flashDuration;
    [SerializeField] private Color _endGameColor;
    [SerializeField] private Color _flashColor;
    [SerializeField] private PlayerStun _playerStun;

    private Color _mainColor;
    private Camera _camera;

    private Coroutine _shothlyChangeColor;

    private void Awake()
    {
        _camera = Camera.main;
        _mainColor = _camera.backgroundColor;
    }

    private void OnEnable()
    {
        _playerStun.Stunned += FlashBackgroundColor;
    }

    private void OnDisable()
    {
        _playerStun.Stunned += FlashBackgroundColor;
    }

    public void EndGameBackgroundColor()
    {
        if (_shothlyChangeColor != null)
            StopCoroutine(_shothlyChangeColor);

        _shothlyChangeColor = StartCoroutine(ShothlyChangeColor(_camera.backgroundColor, _endGameColor));
    }

    public void StartBackgroundColor()
    {
        if (_shothlyChangeColor != null)
            StopCoroutine(_shothlyChangeColor);

        _shothlyChangeColor = StartCoroutine(ShothlyChangeColor(_camera.backgroundColor, _mainColor));
    }

    private void FlashBackgroundColor()
    {
        if (_shothlyChangeColor != null)
            StopCoroutine(_shothlyChangeColor);

        _shothlyChangeColor = StartCoroutine(SmothlyFlashEffect(_flashColor));
    }

    private IEnumerator ShothlyChangeColor(Color currentColor, Color targetColor)
    {
        float elapsedTime = 0;

        while (elapsedTime < _changeColorDuration)
        {
            _camera.backgroundColor = Color.Lerp(currentColor, targetColor, elapsedTime / _changeColorDuration);

            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    private IEnumerator SmothlyFlashEffect( Color targetColor)
    {
        float elapsedTime = 0;

        while (elapsedTime < _changeColorDuration)
        {
            _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, targetColor, elapsedTime / _flashDuration);

            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        StartBackgroundColor();
    }
}
