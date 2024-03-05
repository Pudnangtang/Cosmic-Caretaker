using UnityEngine;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public AudioClip speedBoostSound; // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource playerAudioSource = other.GetComponent<AudioSource>();
            if (playerAudioSource != null && speedBoostSound != null)
            {
                playerAudioSource.PlayOneShot(speedBoostSound);
                Debug.Log("Player is Faster now!");
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip missing!");
            }

            BabysitterMovement movementScript = other.GetComponent<BabysitterMovement>();
            if (movementScript != null)
            {
                movementScript.ActivateSpeedBoost();
            }
            else
            {
                Debug.LogError("BabysitterMovement script not found on the player object!");
            }
            Destroy(gameObject); // Destroy the power-up object
        }
    }
}
