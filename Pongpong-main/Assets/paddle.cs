using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerA = false;
    public GameObject circle;
    private Rigidbody2D rb;
    private Vector2 playerMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isPlayerA)
        {
            PaddleAController();
        }
        else
        {
            PaddleBController();
        }
    }

    private void PaddleBController()
    {
        if (circle.transform.position.y > transform.position.y + 0.5f)
        {
            playerMovement = new Vector2(0, 1);
        }
        else if (circle.transform.position.y < transform.position.y - 0.5f)
        {
            playerMovement = new Vector2(0, -1);
        }
        else
        {
            playerMovement = Vector2.zero;
        }
    }

    private void PaddleAController()
    {
        playerMovement = new Vector2(0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMovement * speed;
    }

    // Method to apply power-ups
    public void ApplyPowerUp(PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost(duration));
                break;
            case PowerUpType.SpeedDecrease:
                StartCoroutine(SpeedDecrease(duration));
                break;
            case PowerUpType.biggen:
                StartCoroutine(biggen(duration));
                break;
            case PowerUpType.SizeDecrease:
                StartCoroutine(SizeDecrease(duration));
                break;
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        speed *= 2; // Double the speed
        yield return new WaitForSeconds(duration);
        speed /= 2; // Reset to normal speed
    }

    private IEnumerator SpeedDecrease(float duration)
    {
        speed /= 2; // Halve the speed
        yield return new WaitForSeconds(duration);
        speed *= 2; // Reset to normal speed
    }

    private IEnumerator biggen(float duration)
    {
        transform.localScale *= 1.5f; // Increase size by 50%
        yield return new WaitForSeconds(duration);
        transform.localScale /= 1.5f; // Reset to normal size
    }

    private IEnumerator SizeDecrease(float duration)
    {
        transform.localScale /= 1.5f; // Decrease size by 33%
        yield return new WaitForSeconds(duration);
        transform.localScale *= 1.5f; // Reset to normal size
    }
}
