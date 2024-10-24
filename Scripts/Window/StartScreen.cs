using System;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;

    public override void Close()
    {
        WindowGroup.alpha = 0;
        CameraRenderer.StartBackgroundColor();
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        WindowGroup.alpha = WindowAlpha;
        CameraRenderer.StartBackgroundColor();
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}
