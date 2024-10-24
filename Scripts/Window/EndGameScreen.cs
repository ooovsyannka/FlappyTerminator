using System;
public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    public override void Close()
    {
        WindowGroup.alpha = 0;
        CameraRenderer.StartBackgroundColor();
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        WindowGroup.alpha = WindowAlpha;
        CameraRenderer.EndGameBackgroundColor();
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}