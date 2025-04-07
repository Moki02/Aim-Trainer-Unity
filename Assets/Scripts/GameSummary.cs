using TMPro;
using UnityEngine;
using System.Collections;

public class GameSummary : MonoBehaviour 
{
    [Header("UI References")]
    [SerializeField] TMP_Text summaryText;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 1f;

    void OnEnable()
    {
        Timer.OnGameEnded += ShowSummary;
        ResetPanel();
    }

    void ResetPanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        summaryText.text = "";
    }

    void ShowSummary() 
    {
        // Use TargetShooter as primary stats source
        int totalShots = TargetShooter.TotalShotsFired;
        int hits = TargetShooter.SuccessfulHits;
        int misses = totalShots - hits;
        float avgAccuracy = hits > 0 ? TargetShooter.TotalAccuracyScore / hits : 0;
        float hitRate = totalShots > 0 ? (float)hits / totalShots * 100 : 0;

        summaryText.text = 
            "GAME SUMMARY\n" +
            "------------------\n" +
            $"Total Shots: {totalShots}\n" +
            $"Targets Hit: {hits}\n" +
            $"Missed Shots: {misses}\n" +
            "------------------\n" +
            $"Average Accuracy: {avgAccuracy:F0}%\n" +
            $"Hit Rate: {hitRate:F0}%";

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed/fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}