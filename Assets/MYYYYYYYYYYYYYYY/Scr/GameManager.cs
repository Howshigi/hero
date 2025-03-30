using UnityEngine;
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
    public AudioClip Walk; // เสียงเดิน
    public float gameTime = 60f; // เวลาทั้งหมด (วินาที)
    private float currentTime;
    private bool isGameActive = false;

    private int score;

    void Awake()
    {
        // หยุดเกมในตอนเริ่ม
        Time.timeScale = 0;

        // กำหนด action เมื่อกดปุ่ม UI
        easyButton.onClick.AddListener(() =>
        {
            StartGame(60f); // กำหนดเวลา 60 วินาที
        });
        normalButton.onClick.AddListener(() =>
        {
            StartGame(50f); // กำหนดเวลา 50 วินาที
        });

        hardButton.onClick.AddListener(() =>
        {
            StartGame(30f); // กำหนดเวลา 30 วินาที
        });
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

        // หยุดเสียงพื้นหลัง
        audioSource.Stop();  
        // เริ่มเสียงเดิน
        PlayWalkingSound();  
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0;  // หยุดเกมในตอนจบ

        isGameActive = false;
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over! Time's up!";

        // หยุดเสียงเดิน
        StopWalkingSound();  
        // เล่นเสียงพื้นหลัง (audioSource) ตอนจบเกม
        audioSource.Play();  
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // เริ่มเกมใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayWalkingSound()
    {
        audioSource.clip = Walk;  // กำหนดเสียงเดิน
        audioSource.loop = true;  // ทำให้เสียงเดินเล่นวนซ้ำ
        audioSource.Play();       // เริ่มเล่นเสียงเดิน
    }

    public void StopWalkingSound()
    {
        audioSource.clip = Walk;  // กำหนดเสียงเดิน

        audioSource.loop = false;  // หยุดวนซ้ำ
        audioSource.Stop();        // หยุดเสียง
    }
}


