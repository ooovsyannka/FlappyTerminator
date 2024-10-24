using System;
using UnityEngine;

public class PlayerState : MonoBehaviour 
{
    public event Action<State> Changed;

    public void Change(State state)
    {
        Changed?.Invoke(state);
    }    
}
