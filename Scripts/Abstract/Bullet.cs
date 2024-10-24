using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _force;

    private Rigidbody2D _rigidbody;
    private Transform _direction;

    public event Action<Bullet> Died;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector2.zero;

        if (_direction != null)
            _rigidbody.AddForce(_direction.right * _force, ForceMode2D.Impulse);
    }

    public void SetDirection(Transform direction) => _direction = direction;

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
