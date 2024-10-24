using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScoreCount))]

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private ScoreCount _scoreCount;

    private void Awake()
    {
        _scoreCount = GetComponent<ScoreCount>();
    }

    private void OnEnable()
    {
        _scoreCount.ScoreChanged += ShowCount;   
    }

    private void OnDisable()
    {
        _scoreCount.ScoreChanged -= ShowCount;   
    }

    private void ShowCount(int count)
    {
        _scoreText.text = count.ToString();
    }
}
