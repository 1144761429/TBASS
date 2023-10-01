using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace UISystem
{
    public class PanelRecentAcquired : PanelBase
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private int maxDisplayAmount;
        [SerializeField] private float displayTime;

        private WaitingQueue<WrappedItem> _itemWaitingQueue;
        private ObjectPool<PanelRecentAcquiredSlot> _slotObjPool;


        public override void Init()
        {
            _itemWaitingQueue = new WaitingQueue<WrappedItem>(maxDisplayAmount);

            PlayerInventory.Instance.Consumables.OnAddItem += AddAcquiredItem;

            _slotObjPool =
                new ObjectPool<PanelRecentAcquiredSlot>(OnCreate, OnGetFromPool, OnReleaseToPool, OnDestroyObj, true, 6,
                    20);

            _itemWaitingQueue.ActiveQueueEnqueueCallback +=
                (wrappedItem) => StartCoroutine(nameof(AddToDisplayQueue), wrappedItem);
        }

        public void AddAcquiredItem(WrappedItem wrappedItem)
        {
            _itemWaitingQueue.Enqueue(wrappedItem);
        }

        private IEnumerator AddToDisplayQueue(WrappedItem wrappedItem)
        {
            PanelRecentAcquiredSlot slot = _slotObjPool.Get();
            slot.UpdateVisual(wrappedItem);

            yield return new WaitForSeconds(displayTime);

            _itemWaitingQueue.Dequeue();
            _slotObjPool.Release(slot);
        }

        #region Object Pool Methods

        private PanelRecentAcquiredSlot OnCreate()
        {
            return Instantiate(slotPrefab, gameObject.transform, false).GetComponent<PanelRecentAcquiredSlot>();
        }

        private void OnGetFromPool(PanelRecentAcquiredSlot slot)
        {
            slot.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(PanelRecentAcquiredSlot slot)
        {
            slot.gameObject.SetActive(false);
        }

        private void OnDestroyObj(PanelRecentAcquiredSlot slot)
        {
            Destroy(slot.gameObject);
        }

        #endregion
    }
}