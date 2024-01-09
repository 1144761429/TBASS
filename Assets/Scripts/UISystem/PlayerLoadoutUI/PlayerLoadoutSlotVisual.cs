using UnityEngine;
using WeaponSystem;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class PlayerLoadoutSlotVisual : UIVisual
    {
        [field: SerializeField] public ELoadoutSlot LoadoutSlotEnum { get; private set; }
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text ammoInfo;

        private readonly Vector2 SIZE_TOP = new Vector2(150, 70);
        private readonly Vector2 SIZE_MID = new Vector2(120, 56);
        private readonly Vector2 SIZE_BOT = new Vector2(96, 44);
        
        private const int POS_Y_TOP = 0;
        private const int POS_Y_MID = 18;
        private const int POS_Y_BOT = 32;
        
        public void UpdateWeaponAmmoInfo(Weapon weapon)
        {
            ammoInfo.text = $"{weapon.RuntimeData.AmmoInMag} I {weapon.RuntimeData.AmmoInReserve}";
        }

        public void UpdateWeaponIcon(Weapon weapon)
        {
            icon.sprite = Resources.Load<Sprite>(weapon.StaticData.SpritePath);
        }

        public void UpdatePosAndSize(ELoadoutSlot front)
        {
            Vector3 localPosition = _rectTransform.localPosition;
            
            switch (LoadoutSlotEnum)
            {
                case ELoadoutSlot.Primary:
                    switch (front)
                    {
                        case ELoadoutSlot.Primary:
                            _rectTransform.sizeDelta = SIZE_TOP;
                            localPosition = new Vector3(localPosition.x, POS_Y_TOP, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                        case ELoadoutSlot.Secondary:
                            _rectTransform.sizeDelta = SIZE_MID;
                            localPosition = new Vector3(localPosition.x, POS_Y_MID, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                        case ELoadoutSlot.Adept:
                            _rectTransform.sizeDelta = SIZE_BOT;
                            localPosition = new Vector3(localPosition.x, POS_Y_BOT, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                    }
                    break;
                case ELoadoutSlot.Secondary:
                    switch (front)
                    {
                        case ELoadoutSlot.Primary:
                            _rectTransform.sizeDelta = SIZE_MID;
                            localPosition = new Vector3(localPosition.x, -POS_Y_MID, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                        case ELoadoutSlot.Secondary:
                            _rectTransform.sizeDelta = SIZE_TOP;
                            localPosition = new Vector3(localPosition.x, POS_Y_TOP, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                        case ELoadoutSlot.Adept:
                            _rectTransform.sizeDelta = SIZE_MID;
                            localPosition = new Vector3(localPosition.x, POS_Y_MID, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                    }
                    break;
                case ELoadoutSlot.Adept:
                    switch (front)
                    {
                        case ELoadoutSlot.Primary:
                            _rectTransform.sizeDelta = SIZE_BOT;
                            localPosition = new Vector3(localPosition.x, -POS_Y_BOT, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                        case ELoadoutSlot.Secondary:
                            _rectTransform.sizeDelta = SIZE_MID;
                            localPosition = new Vector3(localPosition.x, -POS_Y_MID, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                        case ELoadoutSlot.Adept:
                            _rectTransform.sizeDelta = SIZE_TOP;
                            localPosition = new Vector3(localPosition.x, POS_Y_TOP, localPosition.z);
                            _rectTransform.localPosition = localPosition;
                            break;
                    }
                    break;
            }
        }
    }
}