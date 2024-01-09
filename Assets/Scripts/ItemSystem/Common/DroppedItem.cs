using UISystem.InventoryUI;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string name;
    [SerializeField] private int id;
    [SerializeField] private int amount;
    
    [field: SerializeField] public bool IsAutoInteractable { get; private set; }
    
    public WrappedItem WrappedItem { get; private set; }
    public GameObject SourceGameObject => gameObject;

    private void Awake()
    {
        WrappedItem = new WrappedItem(name, id, amount);
    }

    public void Interact()
    {
        if (PlayerInventoryController.Instance.AddItem(WrappedItem))
        {
            //Debug.Log("Dropped item interacted");
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Inventory is full. You can not pick up items currently.");
        }
    }
}