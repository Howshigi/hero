using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;  
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public AudioSource audioSource; 
    public AudioClip Walk;
    public float gameTime = 60f; 
    private float currentTime;
    private bool isGameActive = false;

    private int score;

    void Awake()
    {
        
        Time.timeScale = 0;

        
        easyButton.onClick.AddListener(() =>
        {
            StartGame(60f); 
        });
        normalButton.onClick.AddListener(() =>
        {
            StartGame(50f); 
        });

        hardButton.onClick.AddListener(() =>
        {
            StartGame(30f); 
        });
    }

    void Start()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + gameTime.ToString("F1"); 
        currentTime = gameTime;
    }

    void Update()
    {
        if (isGameActive)
        {
            currentTime -= Time.deltaTime;
            timerText.text = "Time: " + currentTime.ToString("F1"); 

            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void StartGame(float timeLimit)
    {
        titleScreen.SetActive(false);
        Time.timeScale = 1; 

        isGameActive = true;
        currentTime = timeLimit;

        
        audioSource.Stop();  
        
        PlayWalkingSound();  
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0;  
        isGameActive = false;
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over! Time's up!";

        
        StopWalkingSound();  
        
        audioSource.Play();  
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayWalkingSound()
    {
        audioSource.clip = Walk;  
        audioSource.loop = true;  
        audioSource.Play();       
    }

    public void StopWalkingSound()
    {
        audioSource.clip = Walk;  

        audioSource.loop = false; 
        audioSource.Stop();        
    }
}


