using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _wingFlapKey = KeyCode.W;
    private KeyCode _shootKey = KeyCode.Space;

    public bool IsWingFlap { get; private set; }
    public bool IsShoot { get; private set; }

    private void Update()
    {
        IsWingFlap = Input.GetKeyDown(_wingFlapKey);
        IsShoot = Input.GetKeyDown(_shootKey);
    }
}
