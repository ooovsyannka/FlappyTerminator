using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletSpawner), typeof(EnemyState))]

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _shootDelayTime;

    private EnemyState _state;
    private BulletSpawner _bulletSpawner;
    private Transform _bulletParent;
    private Coroutine _shootDelay;
    private WaitForSeconds _waitShootDelay;
    private void Awake()
    {
        _waitShootDelay = new WaitForSeconds(_shootDelayTime);
        _state = GetComponent<EnemyState>();
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

    private void OnEnable()
    {
        _shootDelay = StartCoroutine(ShootDelay());
    }

    public void GetEndGameScreen(EndGameScreen endGameScreen)
    {
        _bulletSpawner.GetEndGameScreen(endGameScreen);
    }

    public void GetBulletParent(Transform bulletParent)
    {
        _bulletParent = bulletParent;
    }

    public void StopShoot()
    {
        StopCoroutine(_shootDelay);
    }

    private IEnumerator ShootDelay()
    {
        while (enabled)
        {
            yield return _waitShootDelay;

            _state.Change(State.Shoot);
            _bulletSpawner.GetBullet(transform.position, transform, transform.rotation, _bulletParent);
        }
    }
}