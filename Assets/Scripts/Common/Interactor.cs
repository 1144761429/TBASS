using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Action<IInteractable> OnAddInteractableToList;
    public Action<IInteractable> OnRemoveInteractableFromList;

    [field:SerializeField] public float Radius { get; private set; }
    
    private CircleCollider2D _collider;
    private Dictionary<GameObject, IInteractable> _interactables;

    /// <summary>
    /// The number of interactable GameObjects within the distance of the Interactor.
    /// </summary>
    public int Amount => _interactables.Count;

    private void Awake()
    {
        if (Radius == 0)
        {
            Debug.LogWarning($"The radius of interactor attached on {gameObject} is 0.");
        }
        
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = Radius;
        _interactables = new Dictionary<GameObject, IInteractable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            if (interactable.IsAutoInteractable)
            {
                interactable.Interact();
            }
            else
            {
                _interactables.Add(other.gameObject, interactable);
                OnAddInteractableToList?.Invoke(interactable);
            }
        }
    }

    //Consider to remove this trigger check since it costs performance
    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable) &&
            !_interactables.ContainsKey(other.gameObject))
        {
            if (interactable.IsAutoInteractable)
            {
                interactable.Interact();
            }
            else
            {
                _interactables.Add(other.gameObject, interactable);
                OnAddInteractableToList?.Invoke(interactable);
            }
        }

        yield return new WaitForSeconds(0.2f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            _interactables.Remove(other.gameObject);
            OnRemoveInteractableFromList?.Invoke(interactable);
        }
    }
}