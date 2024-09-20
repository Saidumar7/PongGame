using UnityEngine;

public enum PowerUpType { SpeedBoost, SpeedDecrease, biggen, SizeDecrease }

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float effectDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplyPowerUp(powerUpType, effectDuration);
                Destroy(gameObject);
            }
        }
    }
}
