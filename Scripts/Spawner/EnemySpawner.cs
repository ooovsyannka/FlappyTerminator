using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreCount _scoreCount;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _delay;

    private Spawner<Enemy> _spawner;

    private void Awake()
    {
        _spawner = new Spawner<Enemy>(_enemyPrefab);
    }

    private void OnEnable()
    {
        _endGameScreen.RestartButtonClicked += _spawner.CleanActiveObject;
    }

    private void OnDisable()
    {
        _endGameScreen.RestartButtonClicked -= _spawner.CleanActiveObject;
    }

    private void Start()
    {
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        bool isNewObject;

        while (enabled)
        {
            Enemy enemy = _spawner.Spawn(GetSpawnPosition(), transform, out isNewObject);

            if (isNewObject)
            {
                enemy.GetEndGameScreen(_endGameScreen);
                enemy.GetScoreCount(_scoreCount);
                enemy.GetComponent<EnemyCombat>().GetBulletParent(_bulletParent);
            }

            enemy.gameObject.SetActive(true);
            enemy.Died += ReturnEnemyInPool;

            yield return waitForSeconds;
        }
    }

    private void ReturnEnemyInPool(Enemy enemy)
    {
        enemy.Died -= ReturnEnemyInPool;
        _spawner.ReturnObjectInPool(enemy);
    }

    private Vector2 GetSpawnPosition()
    {
        float randomHeight = Random.Range(_minHeight, _maxHeight);

        return new Vector2(_spawnPosition.position.x, randomHeight);
    }
}
