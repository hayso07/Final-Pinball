using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public static game_manager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ballsText;
    public Rigidbody2D ball;

    public Transform ballSpawn;


    public int startingBalls = 3;
    public float respawnDelay = 2f;

    public mouth_script[] mouths;
    public mouth_script mainMouth;

    public float eatTime = 2f;


    int score;
    int ballsLeft;
    int mainHits;

    bool gameOver;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    public AudioSource audioSource;
    public AudioClip ballDropSound;
    public AudioClip mouthHitSound;
    public AudioClip roarSound;
    public AudioClip winSound;
    public AudioClip metalSound;
    public AudioClip rampSound;

    float roarTimer;
    public float roarMinTime = 15f;
    public float roarMaxTime = 30f;

    public float flipperMiddle = 0.2f;
    public float dropY = 6.8f;
    public float minOffset = 0.8f;
    public float maxOffset = 1.8f;



    void Start()
    {
        ballsLeft = startingBalls;

        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        UpdateScoreText();
    }

    void Awake()
    {
        instance = this;
    }

  
    void Update()
    {
        if(gameOver)
        {
            restartText.enabled = Mathf.Repeat(Time.time, 1f) < 0.6f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }

        if (!gameOver)
        {
            roarTimer -= Time.deltaTime;

            if (roarTimer <= 0f)
            {
                audioSource.PlayOneShot(roarSound);
                roarTimer = Random.Range(roarMinTime, roarMaxTime);
            }
        }
    }
    public void AddScore(int amount)
    {
        if (gameOver)
        {
            return;
        }
        score += amount;
        UpdateScoreText();
    }

    public void BallLost()
    {
        if (gameOver)
        {
            return;
        }

        ballsLeft--;

        SetBallActive(false);
        audioSource.PlayOneShot(ballDropSound);

        if (ballsLeft <= 0)
        {
            gameOver = true;


            scoreText.gameObject.SetActive(false);
            ballsText.gameObject.SetActive(false);
            gameOverText.text = "YOU LOSE\n<size=60%>SCORE: " + score + "</size>";
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
        }
        else{
            UpdateScoreText();
            StartCoroutine(RespawnBall());
        }
    }

    public void MouthHit(mouth_script mouth)
    {
        audioSource.PlayOneShot(mouthHitSound);

        if (mouth == mainMouth)
        {
            mainHits++;
            AddScore(10000);

            if (mainHits >= 3)
            {
                AddScore(100000);
                audioSource.PlayOneShot(winSound);
                mainHits = 0;
                mainMouth.SetLit(false);
                foreach (mouth_script m in mouths)
                {
                    m.SetLit(true);
                }
            }
            return;

        }

        mouth.SetLit(false);
        AddScore(5000);
        StartCoroutine(EatBall());

        foreach (mouth_script m in mouths)
        {
            if (m.lit)
            {
                return;
            }
        }

        mainMouth.SetLit(true);
        audioSource.PlayOneShot(roarSound);
    }

    IEnumerator EatBall()
    {
        SetBallActive(false);
        yield return new WaitForSeconds(eatTime);

        float side = Random.value < 0.5f ? -1f : 1f;
        float x = flipperMiddle + side * Random.Range(minOffset, maxOffset);

        PlaceBall(new Vector2(x, dropY));
    }

    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(respawnDelay);

        PlaceBall(ballSpawn.position);
    }

    void PlaceBall(Vector2 pos)
    {
        ball.position = pos;
        ball.transform.position = pos;
        ball.linearVelocity = Vector2.zero;
        ball.angularVelocity = 0f;
        ball.gameObject.layer = LayerMask.NameToLayer("Default");

        ball.GetComponent<SpriteRenderer>().sortingOrder = 1000;
        SetBallActive(true);
    }


    void SetBallActive(bool active)
    {

        ball.simulated = active;

        ball.GetComponent<SpriteRenderer>().enabled = active;
    }

    void UpdateScoreText()
    {
        scoreText.text = "<size=55%>SCORE</size>\n" + score.ToString("D6");
        ballsText.text = "<size=55%>BALLS</size>\n" + ballsLeft;
    }

    
}
