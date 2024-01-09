using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InventorySystem;
using InventorySystem.Common.EventArgument;
using UISystem.InventoryUI;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace UISystem
{
    public class RecentAcquiredVisual : UIVisual
    {
        public event Action<WrappedItem> OnDisplayTimeElapsed;
        
        [SerializeField] private GameObject slotPrefab;
        [field: SerializeField] public int MaxDisplayAmount { get; private set; }
        [SerializeField] private float displayTime;
        
        private ObjectPool<RecentAcquiredSlotController> _slotPool;

        private void Awake()
        {
            _slotPool =
                new ObjectPool<RecentAcquiredSlotController>(OnCreate, OnGetFromPool, OnReleaseToPool, OnDestroyObj,
                    true, MaxDisplayAmount, MaxDisplayAmount);
        }

        public IEnumerator AddSlotToDisplay(WrappedItem wrappedItem)
        {
            RecentAcquiredSlotController slot = _slotPool.Get();
            slot.SetItem(wrappedItem);
            
            yield return new WaitForSeconds(displayTime);

            slot.Clear();
            _slotPool.Release(slot);
            OnDisplayTimeElapsed?.Invoke(wrappedItem);
        }

        #region Object Pool Methods

        private RecentAcquiredSlotController OnCreate()
        {
            return Instantiate(slotPrefab, gameObject.transform, false).GetComponent<RecentAcquiredSlotController>();
        }

        private void OnGetFromPool(RecentAcquiredSlotController slot)
        {
            slot.Open();
        }

        private void OnReleaseToPool(RecentAcquiredSlotController slot)
        {
            slot.Close();
        }

        private void OnDestroyObj(RecentAcquiredSlotController slot)
        {
            Destroy(slot.gameObject);
        }

        #endregion
    }
}