using System;
using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private float _restoringDelay;

    private Coroutine _restoring;
    private WaitForSeconds _waitRestoringDelay;

    public bool IsStun { get; private set; }

    public event Action Stunned;
    public event Action Recovered;

    private void Awake()
    {
        _waitRestoringDelay  = new WaitForSeconds(_restoringDelay);
    }

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
        yield return _waitRestoringDelay;

        IsStun = false;
        Recovered?.Invoke();
    }
}
