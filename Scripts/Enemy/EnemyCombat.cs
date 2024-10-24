using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletSpawner), typeof(EnemyState))]

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _shootMaxDelay;
    [SerializeField] private float _shootMinDelay;

    private EnemyState _state;
    private BulletSpawner _bulletSpawner;
    private Coroutine _shootDelay;
    private Transform _bulletParent;

    private void Awake()
    {
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
        float randomDelay = UnityEngine.Random.Range(_shootMinDelay, _shootMaxDelay);
        WaitForSeconds waitForSeconds = new WaitForSeconds(randomDelay);

        while (enabled)
        {
            yield return waitForSeconds;

            _state.Change(State.Shoot);
            _bulletSpawner.GetBullet(transform.position, transform, transform.rotation, _bulletParent);
        }
    }
}