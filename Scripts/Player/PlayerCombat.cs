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
    
    public bool CanShoot { get; private set; }

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

    private void Start()
    {
        _bulletSpawner.GetEndGameScreen(_endGameScreen);
        CanShoot = true;    
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
        WaitForSeconds waitForSeconds = new WaitForSeconds(_shootDelayTime);

        yield return waitForSeconds;

        CanShoot = true;
    }
}