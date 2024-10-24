using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private ScoreCount _scoreCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Died += AddScore;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Died -= AddScore;
        }
    }

    private void AddScore(Enemy enemy)
    {
        _scoreCount.Add();

        enemy.Died -= AddScore;
    }
}
