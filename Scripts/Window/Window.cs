using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour 
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private CameraBackgroundRenderer _cameraRenderer;
    [SerializeField] private CanvasGroup _windowGroup;

    protected int WindowAlpha = 1;
    protected CanvasGroup WindowGroup => _windowGroup;
    protected Button ActionButton => _actionButton;
    protected CameraBackgroundRenderer CameraRenderer => _cameraRenderer;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();

    public abstract void Open();

    public abstract void Close();
}
