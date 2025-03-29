using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;  // แสดงเวลาถอยหลัง
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public AudioSource audioSource; // ตัวเล่นเสียง

    public float gameTime = 60f; // เวลาทั้งหมด (วินาที)
    private float currentTime;
    private bool isGameActive = false;

    private int score;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        Time.timeScale = 0;  // หยุดเกมในตอนเริ่ม

        easyButton.onClick.AddListener(() =>
        {
            StartGame(60f); // กำหนดเวลา 60 วินาที
        });
        normalButton.onClick.AddListener(() =>
        {
            StartGame(50f); // กำหนดเวลา 30 วินาที
        });

        hardButton.onClick.AddListener(() =>
        {
            StartGame(30f); // กำหนดเวลา 30 วินาที
        });
        audioSource.Play();

    }

    

    void Start()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + gameTime.ToString("F1"); // แสดงเวลาเริ่มต้น
        currentTime = gameTime;
    }

    void Update()
    {
        if (isGameActive)
        {
            currentTime -= Time.deltaTime;
            timerText.text = "Time: " + currentTime.ToString("F1"); // อัปเดต UI เวลา

            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void StartGame(float timeLimit)
    {
        titleScreen.SetActive(false);
        Time.timeScale = 1; // เริ่มเกมใหม่

        isGameActive = true;
        currentTime = timeLimit;
        // กำหนดเวลาตามโหมด
        audioSource.Stop();

    }


    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0;  // หยุดเกมในตอนเริ่ม

        isGameActive = false;
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over! Time's up!";
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // เริ่มเกมใหม่

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}