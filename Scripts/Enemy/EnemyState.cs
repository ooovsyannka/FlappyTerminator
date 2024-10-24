using System;
using UnityEngine;

public class EnemyState : MonoBehaviour 
{
    public event Action<State> Changed;

    public void Change(State state)
    {
        Changed?.Invoke(state);
    }
}
