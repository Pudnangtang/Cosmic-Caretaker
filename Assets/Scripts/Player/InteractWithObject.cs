using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public Transform handHolder; // Reference to the HandHolder transform
    private GameObject heldObject; // This now explicitly holds a GameObject, not just a Transform
    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        mainCamera = Camera.main; // Initialize the main camera
    }

    private void Update()
    {
        // Check for space key press
        if (Input.GetKeyDown(KeyCode.Space) && heldObject != null)
        {
            // Destroy the currently held item
            DropHeldItem();
        }
    }


    private void DropHeldItem()
    {
        // If there's an object being held, drop it
        if (heldObject != null)
        {
            // Destroy the held object
            Destroy(heldObject);

            // Clear the reference to the held object
            heldObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player collides with an item, pick it up
        if ((other.CompareTag("Toy") || other.CompareTag("Food")) && heldObject == null)
        {
            PickUpObject(other.gameObject);
        }
        // When the player collides with a baby while holding an item, give it to the baby
        else if (other.CompareTag("Baby") && heldObject != null)
        {
            GiveToBaby(other.gameObject);
        }
    }

    private void PickUpObject(GameObject item)
    {
        // Verify the item is not the player or part of the player
        if (item != this.gameObject && !item.transform.IsChildOf(this.transform))
        {
            heldObject = item;

            // Optionally, disable the item's collider
            Collider2D collider = item.GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = false;

            // Parent the item to the hand holder and reset its local position and rotation
            item.transform.SetParent(handHolder);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    private void GiveToBaby(GameObject baby)
    {
        // Get the baby's script
        AlienToddler babyScript = baby.GetComponent<AlienToddler>();
        if (babyScript != null && heldObject != null)
        {
            // Determine the type of the held object
            AlienToddler.ItemType itemType = GetItemType(heldObject); // Note the change here

            // Give the item to the baby
            babyScript.GiveItem(itemType);

            // Destroy or release the held object
            Destroy(heldObject);
            heldObject = null;
        }
        else
        {
            Debug.LogError("No baby script found or no object held.");
        }
    }

    private AlienToddler.ItemType GetItemType(GameObject item) // Note the return type change here
    {
        // Implement logic to determine if the item is a Toy or Food
        // This can be based on the item's tag, name, or a component it has
        if (item.CompareTag("Toy"))
        {
            return AlienToddler.ItemType.Toy; // Note the change here
        }
        else if (item.CompareTag("Food"))
        {
            return AlienToddler.ItemType.Food; // Note the change here
        }
        else
        {
            Debug.LogError("Unknown item type.");
            return AlienToddler.ItemType.Toy; // Default return, modify as needed
        }
    }
}

public enum ItemType
{
    Toy,
    Food
}