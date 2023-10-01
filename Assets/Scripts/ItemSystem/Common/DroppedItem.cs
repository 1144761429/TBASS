using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    public WrappedItem wrappedItem;

    public GameObject SourceGameObject
    {
        get => gameObject;
    }

    public bool AutoInteractable
    {
        get => true;
    }

    public void Interact()
    {
        if (PlayerInventory.Instance.AddItem(wrappedItem))
        {
            Debug.Log("Dropped item interacted");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is full. You can not pick up items currently.");
        }
    }
}