using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool IsWingFlap { get; private set; }
    public bool IsShoot { get; private set; }

    private void Update()
    {
        IsWingFlap = Input.GetKeyDown(KeyCode.W);
        IsShoot = Input.GetKeyDown(KeyCode.Space);
    }
}
