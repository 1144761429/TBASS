using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UISystem;
using UnityEngine.UI;

public class PanelPlayerInteractor : PanelBase
{
    private Interactor _interactor;
    private Dictionary<GameObject, PanelPlayerInteractorSlot> _slots;
    private ObjectPool<PanelPlayerInteractorSlot> _slotsObjPool;
    [SerializeField] private GameObject _interactorSlotPrefab;
    [SerializeField] private GameObject _interactorSlotMorePrefab;
    [SerializeField] private Transform _slotParent;

    private RectTransform _rectTransform;
    private RectTransform _scrollablePanelTransform;
    private float _slotHeight;
    private float _spacing;

    public ScrollRect scrollRect;

    public override void Init()
    {
        base.Init();

        _slots = new Dictionary<GameObject, PanelPlayerInteractorSlot>();
        _slotsObjPool = new ObjectPool<PanelPlayerInteractorSlot>(CreateSlot, OnGetSlot, OnReleaseSlot, OnDestroySlot);
        _interactor = GameObject.FindWithTag("Player").GetComponentInChildren<Interactor>();
        _rectTransform = GetComponent<RectTransform>();
        _scrollablePanelTransform = GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>();

        _slotHeight = _interactorSlotPrefab.GetComponent<RectTransform>().sizeDelta.y;
        _spacing = GetComponentInChildren<VerticalLayoutGroup>().spacing;
    }

    private void OnEnable()
    {
        _interactor.OnAddInteractableToList += AddInteractableSlot;
        _interactor.OnRemoveInteractableFromList += RemoveInteractableSlot;
    }

    private void Update()
    {

    }

    private void AddInteractableSlot(IInteractable interactable)
    {
        if (_interactor.Amount == 6)
        {
            PanelPlayerInteractorSlot tempSlot = _slotsObjPool.Get();
            _slots.Add(_interactorSlotMorePrefab, tempSlot);
            return;
        }

        if (_interactor.Amount > 6)
        {
            //Update "More" slot
            return;
        }

        PanelPlayerInteractorSlot slot = _slotsObjPool.Get();
        _slots.Add(interactable.SourceGameObject, slot);
        slot.UpdateVisual(null, interactable.SourceGameObject.name);


    }

    private void RemoveInteractableSlot(IInteractable interactable)
    {
        _slotsObjPool.Release(_slots[interactable.SourceGameObject]);
        _slots.Remove(interactable.SourceGameObject);
    }

    #region Object Pool Methods
    private PanelPlayerInteractorSlot CreateSlot()
    {
        PanelPlayerInteractorSlot slot = Instantiate(_interactorSlotPrefab, _slotParent, false).GetComponent<PanelPlayerInteractorSlot>();
        return slot;
    }

    private void OnGetSlot(PanelPlayerInteractorSlot slot)
    {
        slot.gameObject.SetActive(true);
    }

    private void OnReleaseSlot(PanelPlayerInteractorSlot slot)
    {
        slot.gameObject.SetActive(false);
    }

    private void OnDestroySlot(PanelPlayerInteractorSlot slot)
    {
        Destroy(slot.gameObject);
    }

    #endregion
}
