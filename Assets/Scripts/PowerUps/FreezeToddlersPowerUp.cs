using UnityEngine;

public class FreezeToddlersPowerUp : MonoBehaviour
{
    public AudioClip freezeSound; // Drag the freeze sound effect here in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource playerAudioSource = other.GetComponent<AudioSource>();
            if (playerAudioSource != null && freezeSound != null)
            {
                playerAudioSource.PlayOneShot(freezeSound);
                Debug.Log("Toddlers Frozen!");
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip missing!");
            }

            foreach (var toddler in FindObjectsOfType<AlienToddlerMovement>())
            {
                toddler.FreezeMovement();
            }
            Destroy(gameObject); // Destroy the power-up object
        }
    }
}
