using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public AudioClip happyToddlersSound; // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource playerAudioSource = other.GetComponent<AudioSource>();
            if (playerAudioSource != null && happyToddlersSound != null)
            {
                playerAudioSource.PlayOneShot(happyToddlersSound);
                Debug.Log("Toddlers All Happy!");
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip missing!");
            }

            ActivatePowerUp();
            Destroy(gameObject); // Destroy the power-up object
        }
    }

    private void ActivatePowerUp()
    {
        // Find all toddlers in the scene
        AlienToddler[] toddlers = FindObjectsOfType<AlienToddler>();

        foreach (AlienToddler toddler in toddlers)
        {
            // Simulate giving the correct item to each toddler
            toddler.ReceivePowerUpItem();
        }

        // Any additional effects like sound or visual feedback
    }
}
