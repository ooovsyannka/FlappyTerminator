using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private EndGameScreen _endGameScreen;
    private Spawner<Bullet> _spawner;

    private void Awake()
    {
        _spawner = new Spawner<Bullet>(_bullet);
    }

    private void OnEnable()
    {
        if (_endGameScreen != null)
        {
            SubscribeToEvent();
        }
    }

    private void OnDisable()
    {
        if (_endGameScreen != null)
        {
            _endGameScreen.RestartButtonClicked -= _spawner.CleanActiveObject;
        }
    }

    public void GetBullet(Vector2 spawnPosition, Transform direction, Quaternion rotate, Transform parent)
    {
        bool isNewObject;

        Bullet bullet = _spawner.Spawn(spawnPosition, parent, out isNewObject);

        if (isNewObject)
        {
            bullet.SetDirection(direction);
        }

        bullet.transform.rotation = rotate;
        bullet.gameObject.SetActive(true);
        bullet.Died += ReturnBulletInPool;
    }

    public void GetEndGameScreen(EndGameScreen endGameScreen)
    {
        _endGameScreen = endGameScreen;
        SubscribeToEvent();
    }

    private void ReturnBulletInPool(Bullet bullet)
    {
        _spawner.ReturnObjectInPool(bullet);

        bullet.Died -= ReturnBulletInPool;
    }

    private void SubscribeToEvent() => _endGameScreen.RestartButtonClicked += _spawner.CleanActiveObject;
}
