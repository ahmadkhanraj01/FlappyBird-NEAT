using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Manual Play Only")]
    public Player player; // only assign this if you’re playing manually

    [Header("UI")]
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;

    private int score = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // If manual mode, pause until Play is pressed
        if (player != null)
        {
            Pause();
        }
    }

    public void Play()
    {
        if (player == null)
        {
            Debug.LogWarning("GameManager: No Player assigned. Probably running AI mode.");
            return;
        }

        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        gameOver.SetActive(false);

        // Clear old pipes
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        if (player != null)
            player.enabled = false;
    }

    public void GameOver()
    {
        if (player == null)
            return; // in AI mode, EvolutionManager handles deaths

        score = 0;
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        if (player == null)
            return; // in AI mode, BirdAI tracks score

        score++;
        scoreText.text = score.ToString();
    }
}
