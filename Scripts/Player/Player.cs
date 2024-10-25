using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerRenderer), typeof(PlayerCollisionHandler))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerStun _stun;
    [SerializeField] private PlayerState _state;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ScoreCount _scoreCount;

    private PlayerCollisionHandler _collisionHandler;
    private PlayerRenderer _renderer;
    private PlayerMover _mover;
    private bool _isDie;

    public event Action GameOver;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _renderer = GetComponent<PlayerRenderer>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
        _stun.Recovered += _renderer.ChangeToOriginalColor;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
        _stun.Recovered -= _renderer.ChangeToOriginalColor;
    }

    private void Update()
    {
        if (_isDie == false)
        {
            if (_stun.IsStun == false)
            {
                if (_inputReader.IsWingFlap)
                {
                    _state.Change(State.WingFlap);
                    _mover.WingFlap();
                }

                if (_inputReader.IsShoot)
                {
                    if (_combat.CanShoot)
                    {
                        _state.Change(State.Shoot);
                        _combat.Shoot(transform.rotation);
                    }
                }
            }

            _mover.Rotate();
        }
    }

    public void Reset()
    {
        _isDie = false;
        _state.Change(State.AnyState);
        _mover.Reset();
        _scoreCount.Reset();
        _renderer.ResetRenderer();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Ground || interactable is Enemy || interactable is EnemyBullet)
        {
            _isDie = true;
            _state.Change(State.Die);
            GameOver?.Invoke();
        }
        else if (interactable is Cloud)
        {
            _renderer.ChangeToBlueColor();
            _stun.GetStun();
            _state.Change(State.Stun);
        }
    }
}
