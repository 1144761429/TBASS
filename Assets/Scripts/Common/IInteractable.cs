using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    GameObject SourceGameObject { get; }
    bool AutoInteractable { get; }
    void Interact();
}
