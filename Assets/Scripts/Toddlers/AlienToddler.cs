using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class AlienToddler : MonoBehaviour
{
    public enum ItemType { Toy, Food }
    public float timeToDisappear = 20f; // Time in seconds before toddler disappears

    private ItemType desiredItem;
    private float timer;

    public Image itemWantedImage; // Reference to the UI Image showing the wanted item
    public Sprite toySprite; // Sprite to show when a Toy is wanted
    public Sprite foodSprite; // Sprite to show when Food is wanted

    private Animator animator; // Reference to the Animator component

    public AudioClip correctItemSound; // Assign this in the Inspector
    public AudioClip wrongItemSound;   // Assign this in the Inspector
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        animator = GetComponent<Animator>(); // Make sure to get the Animator component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null) { Debug.LogError("AudioSource component missing from this GameObject"); }
        ChooseRandomItem();
    }

    void Update()
    {
        // Countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Disappear();
        }
    }

    public void ReceivePowerUpItem()
    {
        // Logic to simulate receiving the correct item
        // This might be similar to what happens in GiveItem, but without the need to check the item type
        ScoreManager.Instance.CorrectItemGiven(); // Increase score
        ResetTimer();
        ChooseRandomItem();
    }

    void ChooseRandomItem()
    {
        // Randomly choose between Toy and Food
        desiredItem = (ItemType)Random.Range(0, 2);
        UpdateItemWantedDisplay(); // Update the UI display
        timer = timeToDisappear; // Reset the timer
    }

    void UpdateItemWantedDisplay()
    {
        if (itemWantedImage != null)
        {
            // Set the sprite to the corresponding item wanted
            itemWantedImage.sprite = (desiredItem == ItemType.Toy) ? toySprite : foodSprite;
            itemWantedImage.enabled = true; // Make sure the image is enabled
        }
    }

    public void GiveItem(ItemType itemType)
    {
        if (itemType == desiredItem)
        {
            Debug.Log("Correct item given!");
            animator.SetTrigger("Happy");
            ScoreManager.Instance.CorrectItemGiven(); // Increase score
            PlaySound(correctItemSound); // Play the correct item sound
            ResetTimer();
            ChooseRandomItem();
        }
        else
        {
            Debug.Log("Wrong item!");
            animator.SetTrigger("Sad");
            ScoreManager.Instance.WrongItemGiven(); // Decrease score
            PlaySound(wrongItemSound); // Play the wrong item sound
            ChooseRandomItem();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void ResetTimer()
    {
        timer = timeToDisappear; // Reset the timer
                                 // Any additional logic you want to execute when the timer is reset
    }


    void Disappear()
    {
        Debug.Log("Toddler disappeared!");
        // Trigger the animation before destroying the GameObject
        animator.SetTrigger("Gone");

        // Optionally, wait for the animation to finish before destroying the object.
        // This can be done using a coroutine or an animation event at the end of the animation clip.
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        // Assuming the NPC_Gone animation is 1 second long.
        // You can also use an Animation Event to trigger the end of this wait.
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

