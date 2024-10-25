using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyCombat), typeof(EnemyCollisionHandler), typeof(EnemyState))]

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private EnemyAnimation _animation;
    [SerializeField] private float _dieDelay;

    private EnemyState _state;
    private EnemyCombat _combat;
    private EnemyCollisionHandler _collisionHandler;
    private CircleCollider2D _circleCollider;
    private ScoreCount _scoreCount;
    private WaitForSeconds _waitDieDelay;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _combat = GetComponent<EnemyCombat>();
        _state = GetComponent<EnemyState>();
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _waitDieDelay = new WaitForSeconds(_dieDelay);
    }

    private void OnEnable()
    {
        _state.Change(State.AnyState);
        _collisionHandler.CollisionDetected += ProcessCollision;
        _circleCollider.enabled = true;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    public void GetEndGameScreen(EndGameScreen endGameScreen)
    {
        _combat.GetEndGameScreen(endGameScreen);
    }

    public void GetScoreCount(ScoreCount scoreCount)
    {
        _scoreCount = scoreCount;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is PlayerBullet bullet)
        {
            _state.Change(State.Die);
            bullet.Die();
            _scoreCount.Add();
            StartCoroutine(DieDelay());
        }
        else if (interactable is ObjectRemover)
        {
            _combat.StopShoot();
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DieDelay()
    {
        _circleCollider.enabled = false;
        _animation.PlayAnimation(true);
        _combat.StopShoot();

        yield return _waitDieDelay;

        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
