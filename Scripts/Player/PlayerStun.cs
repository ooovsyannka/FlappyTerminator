using System;
using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Coroutine _restoring;

    public bool IsStun { get; private set; }

    public event Action Stunned;
    public event Action Recovered;

    public void GetStun()
    {
        IsStun = true;
        Stunned?.Invoke();

        if (_restoring != null)
            StopCoroutine(_restoring);

        _restoring = StartCoroutine(Restoring());
    }


    private IEnumerator Restoring()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        yield return waitForSeconds;

        IsStun = false;
        Recovered?.Invoke();
    }
}
