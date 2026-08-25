//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public FixedJoystick joystick;
//    public float moveSpeed;

//    float hInput, vInput;

//    int score = 0;

//    public GameObject winText;
//    public int winScore;
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void FixedUpdate()
//    {
//        hInput = joystick.Horizontal * moveSpeed;
//        vInput = joystick.Vertical * moveSpeed;

//        transform.Translate(hInput, vInput, 0);
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if(collision.gameObject.tag == "Power")
//        {
//            score++;
//            Destroy(collision.gameObject);

//            if(score >= winScore)
//            {
//                winText.SetActive(true);
//            }
//        }


//    }
//}
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed;

    float hInput, vInput;
    int score = 0;

    [Header("Win")]
    public int winScore = 10;
    public GameObject winText;

    [Header("Lose")]
    public GameObject loseText;

    [Header("Timer")]
    public float gameTime = 30f;
    public TextMeshProUGUI timerText;

    [Header("Power Counter")]
    public TextMeshProUGUI powerCounterText;

    [Header("Power Spawn")]
    public GameObject powerPrefab;
    public int powerCount = 10;

    [Header("Spawn Boundary")]
    public float minX = -4.2f;
    public float maxX = 4.2f;
    public float minY = -2.2f;
    public float maxY = 2.2f;

    private bool gameEnded = false;

    void Start()
    {
        gameEnded = false;
        score = 0;

        if (winText != null)
            winText.SetActive(false);

        if (loseText != null)
            loseText.SetActive(false);

        UpdatePowerCounter();

        if (powerPrefab != null)
            SpawnPowers();

        StartCoroutine(GameTimer());
    }

    private void FixedUpdate()
    {
        if (gameEnded)
            return;

        if (joystick == null)
            return;

        hInput = joystick.Horizontal * moveSpeed;
        vInput = joystick.Vertical * moveSpeed;

        transform.Translate(hInput, vInput, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameEnded)
            return;

        if (collision.gameObject.CompareTag("Power"))
        {
            score++;

            Destroy(collision.gameObject);

            UpdatePowerCounter();

            Debug.Log("Power Collected: " + score);

            if (score >= winScore)
            {
                WinGame();
            }
        }
    }

    void UpdatePowerCounter()
    {
        if (powerCounterText != null)
        {
            powerCounterText.text = score + " / " + winScore;
        }
    }

    void SpawnPowers()
    {
        for (int i = 0; i < powerCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            Instantiate(
                powerPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }

    IEnumerator GameTimer()
    {
        float timeLeft = gameTime;

        while (timeLeft > 0 && !gameEnded)
        {
            timeLeft -= Time.deltaTime;

            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Ceil(timeLeft);
            }

            yield return null;
        }

        if (!gameEnded)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        gameEnded = true;

        StopAllCoroutines();

        if (winText != null)
            winText.SetActive(true);

        Debug.Log("YOU WIN!");
    }

    void LoseGame()
    {
        gameEnded = true;

        StopAllCoroutines();

        if (loseText != null)
            loseText.SetActive(true);

        Debug.Log("YOU LOSE!");
    }

}