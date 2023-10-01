using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class PanelPlayerInteractorSlot : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private TMP_Text _tmpText;

        private void Awake()
        {
            _tmpText = GetComponentInChildren<TMP_Text>();
        }

        public void UpdateVisual(Sprite sprite, string message)
        {
            _image.sprite = sprite;
            _tmpText.text = message;
        }
    }
}