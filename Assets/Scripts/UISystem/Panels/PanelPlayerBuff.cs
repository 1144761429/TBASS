using System;
using System.Collections.Generic;
using BuffSystem.Common;
using UISystem;
using UnityEngine;
using UnityEngine.Pool;

public class PanelPlayerBuff : PanelBase
{
    [SerializeField] private int maxDisplayAmount;
    [SerializeField] private GameObject buffSlotPrefab;

    private ObjectPool<PanelPlayerBuffSlot> _slotObjectPool;
    private PriorityQueue<Buff, int> _displayableBuffPriorityQueue;
    private List<Buff> _displayedBuffs;
    private List<PanelPlayerBuffSlot> _activeBuffSlots;

    private BuffHandler _playerBuffHandler;

    public override void Init()
    {
        _slotObjectPool =
            new ObjectPool<PanelPlayerBuffSlot>(OnCreate, OnGetFromPool, OnReleaseToPool, OnDestroyObj, true,
                maxDisplayAmount, maxDisplayAmount);
        _displayableBuffPriorityQueue = new PriorityQueue<Buff, int>();
        _displayedBuffs = new List<Buff>();
        _activeBuffSlots = new List<PanelPlayerBuffSlot>();

        _playerBuffHandler = PlayerStats.Instance.BuffHandler;
        if (_playerBuffHandler == null)
        {
            throw new Exception(
                "PlayerBuffHandler is null in PanelPlayerBuff. " +
                "This could be caused by inappropriate script execution order: PanelPlayerBuff.Init() is called before Player.Awake().");
        }

        _playerBuffHandler.AddBuffCallback += (buff) =>
        {
            _displayableBuffPriorityQueue.Enqueue(buff, ((IDisplayable)buff).Priority);
        };
        _playerBuffHandler.AddBuffCallback += RearrangeDisplayedBuffVisual;

        _playerBuffHandler.RemoveBuffCallback += (buff) => { _displayedBuffs.Remove(buff); };
        _playerBuffHandler.RemoveBuffCallback += RearrangeDisplayedBuffVisual;
    }

    private void Update()
    {
        UpdateVisual();
    }

    /// <summary>
    /// Update the remaining time of all the buffs that are displaying
    /// </summary>
    private void UpdateVisual()
    {
        foreach (var buffSlot in _activeBuffSlots)
        {
            buffSlot.UpdateRemainingTime();
        }
    }

    /// <summary>
    /// Display the a specific number of top priority buffs according to maxDisplayAmount by removing all the displaying buff first.
    /// Then put them back to the priority queue so we can rearrange the priority of the buffs.
    /// Finally, dequeue the top buffs into the _displayedBuffs list and display them on the panel.
    /// </summary>
    /// <param name="buff">Though this parameter is not used in the method, it makes this method valid to be passed to <c>Action with a Buff type parameter</c>>. </param>
    private void RearrangeDisplayedBuffVisual(Buff buff)
    {
        // Enqueue all the buffs that are being displayed to the priority queue,
        // so we can rearrange the position of all buffs in the priority queue.
        foreach (var displayedBuff in _displayedBuffs)
        {
            _displayableBuffPriorityQueue.Enqueue(displayedBuff, ((IDisplayable)displayedBuff).Priority);
        }

        _displayedBuffs.Clear();


        // Disable all the buff slot on the screen
        foreach (var slot in _activeBuffSlots)
        {
            _slotObjectPool.Release(slot);
        }

        _activeBuffSlots.Clear();

        // Dequeue the first few buffs and put them into the _displayedBuff list
        for (int i = 0; i < maxDisplayAmount; i++)
        {
            // If there are no more buffs in the priority queue, then break the loop
            if (_displayableBuffPriorityQueue.TryDequeue(out Buff firstPriorityBuff, out int priority))
            {
                // Only buffs that inherit IDisplayable can be added to _displayedBuffs list and show them on the screen
                if (!(buff is IDisplayable))
                {
                    continue;
                }

                _displayedBuffs.Add(firstPriorityBuff);
                PanelPlayerBuffSlot buffSlot = _slotObjectPool.Get();
                _activeBuffSlots.Add(buffSlot);
                buffSlot.UpdateVisual(firstPriorityBuff);
            }
            else
            {
                break;
            }
        }
    }

    #region Object Pool Methods

    private PanelPlayerBuffSlot OnCreate()
    {
        GameObject slot = Instantiate(buffSlotPrefab, gameObject.transform, false);
        print(slot.gameObject.name);
        return slot.GetComponent<PanelPlayerBuffSlot>();
    }

    private void OnGetFromPool(PanelPlayerBuffSlot slot)
    {
        slot.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(PanelPlayerBuffSlot slot)
    {
        slot.gameObject.SetActive(false);
    }

    private void OnDestroyObj(PanelPlayerBuffSlot slot)
    {
        Destroy(slot.gameObject);
    }

    #endregion
}