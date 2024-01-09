using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerStatusBarVisual : UIVisual
{
    [field: SerializeField] public int MaxDisplayAmount { get; private set; }
    [field: SerializeField] private GameObject statusSlotPrefab;
    
    private ObjectPool<PlayerStatusSlotController> _slotPool;
    private List<PlayerStatusSlotController> _activeSlotControllers;

    private void Awake()
    {
        _slotPool = new ObjectPool<PlayerStatusSlotController>(OnCreate, OnGetFromPool, OnReleaseToPool,
            OnDestroyObj, false, MaxDisplayAmount, MaxDisplayAmount);
        _activeSlotControllers = new List<PlayerStatusSlotController>(MaxDisplayAmount);
    }

    public void UpdateStatusToDisplay(object sender, AddStatusEventArgs args)
    {
        // Release all the slot controller back to he object pool.
        foreach (PlayerStatusSlotController slot in _activeSlotControllers)
        {
            _slotPool.Release(slot);
        }

        _activeSlotControllers.Clear();

        // For each status to display, get a slot controller from the pool and assign the status to the controller.
        foreach (var status in args.StatusToDisplay)
        {
            PlayerStatusSlotController slotController = _slotPool.Get();
            slotController.SetStatus(status);
            _activeSlotControllers.Add(slotController);
        }
    }
    
    public void UpdateStatusToDisplay(object sender, RemoveStatusEventArgs args)
    {
        // Release all the slot controller back to he object pool.
        foreach (PlayerStatusSlotController slot in _activeSlotControllers)
        {
            _slotPool.Release(slot);
        }

        _activeSlotControllers.Clear();

        // For each status to display, get a slot controller from the pool and assign the status to the controller.
        foreach (var status in args.StatusToDisplay)
        {
            PlayerStatusSlotController slotController = _slotPool.Get();
            slotController.SetStatus(status);
            _activeSlotControllers.Add(slotController);
        }
    }

    #region Object Pool Methods

    private PlayerStatusSlotController OnCreate()
    {
        GameObject slot = Instantiate(statusSlotPrefab, transform, false);
        return slot.GetComponent<PlayerStatusSlotController>();
    }

    private void OnGetFromPool(PlayerStatusSlotController slot)
    {
        slot.Open();
    }

    private void OnReleaseToPool(PlayerStatusSlotController slot)
    {
        slot.Close();
    }

    private void OnDestroyObj(PlayerStatusSlotController slot)
    {
        Destroy(slot.gameObject);
    }

    #endregion
}