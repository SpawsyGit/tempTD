using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    internal class UISelectedTower : UIElement {
        public event Action OnBuyButtonClicked;
        [SerializeField] private Image towerImage;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI costText;

        public void Setup(TowerInfo info) {
            towerImage.sprite = info.TowerSprite;
            costText.text = info.TowerCost.ToString();
        }

        private void OnEnable() {
            buyButton.onClick.AddListener(BuyButtonClicked);
        }
        private void OnDisable() {
            buyButton.onClick.RemoveListener(BuyButtonClicked);
        }

        private void BuyButtonClicked() {
            OnBuyButtonClicked?.Invoke();
        }
    }
}