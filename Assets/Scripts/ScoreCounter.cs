using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public static int Score { get; private set; }

    void OnEnable()
    {
        Target.OnTargetHit += OnTargetHit; 
    }

    void OnDisable()
    {
        Target.OnTargetHit -= OnTargetHit;
    }

    void OnTargetHit()
    {
        Score++;
        text.text = $"Score: {Score}";
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}