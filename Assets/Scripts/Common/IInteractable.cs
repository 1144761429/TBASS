using UnityEngine;

public interface IInteractable
{
    GameObject SourceGameObject { get; }
    bool IsAutoInteractable { get; }
    void Interact();
}
