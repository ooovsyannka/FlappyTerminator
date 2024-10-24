using System.Collections;
using Unity.Burst;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class PlayerRenderer : MonoBehaviour
{
    [SerializeField] private Color _flashColor;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _smothlyChangeColor;
    private Color _originalColor;
    private float _duration = 3;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _originalColor = _spriteRenderer.color;
    }

    public void ChangeToBlueColor()
    {
        _spriteRenderer.color = _flashColor;
    }

    public void ChangeToOriginalColor()
    {
        if (_smothlyChangeColor != null)
            StopCoroutine(_smothlyChangeColor);

        _smothlyChangeColor = StartCoroutine(SmothlyChangeToOriginalColor(_originalColor));
    }

    public void ResetRenderer()
    {
        if (_smothlyChangeColor != null)
            StopCoroutine(_smothlyChangeColor);

        _spriteRenderer.color = _originalColor;
    }

    private IEnumerator SmothlyChangeToOriginalColor(Color desiredColor)
    {
        float elapsedTime = 0;

        while (_spriteRenderer.color != desiredColor)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, desiredColor, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
