using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        // Make sure to reset all game stats when starting
        TargetShooter.ResetStats();
        ScoreCounter.ResetScore();
        MissCounter.ResetMisses();
        
        // Load your game scene (replace "GameScene" with your actual game scene name)
        SceneManager.LoadScene("Game");
    }
}