using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Action<IInteractable> OnAddInteractableToList;
    public Action<IInteractable> OnRemoveInteractableFromList;

    private CircleCollider2D _collider;
    private Dictionary<GameObject, IInteractable> _interactables;

    public int Amount
    {
        get { return _interactables.Count; }
    }

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _interactables = new Dictionary<GameObject, IInteractable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            if (interactable.AutoInteractable)
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable) &&
            !_interactables.ContainsKey(other.gameObject))
        {
            if (interactable.AutoInteractable)
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            _interactables.Remove(other.gameObject);
            OnRemoveInteractableFromList?.Invoke(interactable);
        }
    }
}