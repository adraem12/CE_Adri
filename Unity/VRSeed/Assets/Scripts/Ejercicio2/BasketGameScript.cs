using System;
using TMPro;
using UnityEngine;

public class BasketGameScript : MonoBehaviour
{
    public Transform[] ballSpawners = new Transform[2];
    public GameObject ballPrefab;
    BallScript[] balls = new BallScript[2];
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI streakText;
    public GameObject gameEndPanel;
    public TextMeshProUGUI scoreEndText;
    public TextMeshProUGUI streakEndText;
    int score;
    int currentStreak, maxStreak;
    float timeLeft;
    bool playing = false;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SpawnBall(0);
        SpawnBall(1);
    }

    void Update()
    {
        if (timeLeft > 0) 
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft > 60)
            {
                float mins = Mathf.FloorToInt(timeLeft / 60);
                float secs = Mathf.FloorToInt(timeLeft % 60);
                timerText.text = string.Format("{0:00}:{1:00}", mins, secs);
            }
            else
                timerText.text = timeLeft.ToString("0.00");
        }
        else if (playing)
        {
            playing = false;
            scoreEndText.text = "Score: " + score;
            streakEndText.text = "Max streak: " + maxStreak;
            timeLeft = 0;
            score = 0;
            currentStreak = 0;
            maxStreak = 0;
            timerText.text = timeLeft.ToString("0.00");
            gameEndPanel.SetActive(true);
            foreach (BallScript ball in balls)
                ball.CallOnHit(false);
        }
    }

    void SpawnBall(int num)
    {
        balls[num] = Instantiate(ballPrefab, ballSpawners[num].position, Quaternion.identity).GetComponent<BallScript>();
        balls[num].OnHit += HitBall;
        balls[num].OnPick += CheckGameState;
    }

    void HitBall(object sender, BallScriptEventArgs ballArgs)
    {
        ballArgs.NewBallScript.OnHit -= HitBall;
        if (ballArgs.ScoredBall)
        {
            score += 1;
            currentStreak += 1;
            audioSource.Play();
        }
        else
        {
            if (currentStreak > maxStreak)
                maxStreak = currentStreak;
            currentStreak = 0;
        }
        SpawnBall(ballArgs.NewBallScript == balls[0] ? 0 : 1);
        UpdateTexts();
    }

    void UpdateTexts()
    {
        scoreText.text = "Score: " + score;
        streakText.text = "Streak: " + currentStreak;
    }

    private void CheckGameState(object sender, EventArgs e)
    {
        if (!gameEndPanel.activeSelf && !playing)
        {
            playing = true;
            timeLeft = 60;
        }
    }
}