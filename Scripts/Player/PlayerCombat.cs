using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _shootDelayTime;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private Transform _bulletParent;

    private BulletSpawner _bulletSpawner;
    private Coroutine _shootDelayCoroutine;
    private WaitForSeconds _waitShootDelay; 

    public bool CanShoot { get; private set; }

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
        CanShoot = true;
        _bulletSpawner.GetEndGameScreen(_endGameScreen);
        _waitShootDelay = new WaitForSeconds(_shootDelayTime);
    }

    public void Shoot(Quaternion rotation)
    {
        _bulletSpawner.GetBullet(transform.position, transform, rotation, _bulletParent);
        CanShoot = false;

        if (_shootDelayCoroutine != null)
            StopCoroutine(_shootDelayCoroutine);

        _shootDelayCoroutine = StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return _waitShootDelay;

        CanShoot = true;
    }
}