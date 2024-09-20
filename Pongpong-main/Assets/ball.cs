using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float speedIncrease = 0.2f;
    public Text playerText;
    public Text opponentText;

    // Audio Clips
    public AudioClip goalSound;
    public AudioClip wallHitSound;
    public AudioClip paddleHitSound;

    private int hitCounter;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void RestartBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > 0)
        {
            RestartBall();
            playerText.text = (int.Parse(playerText.text) + 1).ToString();
            PlaySound(goalSound);
        }
        else if (transform.position.x < 0)
        {
            RestartBall();
            opponentText.text = (int.Parse(opponentText.text) + 1).ToString();
            PlaySound(goalSound);
        }
    }

    private void PlayerBounce(Transform obj)
    {
        hitCounter++;
        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = obj.position;

        float xDirection = transform.position.x > 0 ? -1 : 1;
        float yDirection = (ballPosition.y - playerPosition.y) / obj.GetComponent<Collider2D>().bounds.size.y;

        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }

        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
        PlaySound(paddleHitSound);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PaddleA" || other.gameObject.name == "PaddleB")
        {
            PlayerBounce(other.transform);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            PlaySound(wallHitSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
