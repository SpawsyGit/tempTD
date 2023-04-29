using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    internal class UIPopupTowersBuild : UIPopup {
        public event Action<int> OnTowerBuyClicked;
        [SerializeField] UITowersList towersList;
        [SerializeField] UISelectedTower selectedTower;
        [SerializeField] Button closeButton;

        private TowerInfo[] towerInfos;
        private int selectedIndex;
        public void Setup(TowerInfo[] infos) {
            towerInfos = infos;
            towersList.Setup(infos);
            selectedTower.Setup(infos[0]);
        }
        private void OnEnable() {
            closeButton.onClick.AddListener(OnClose);
            towersList.OnTowerSelected += OnTowerSelected;
            selectedTower.OnBuyButtonClicked += OnBuyButtonClicked;
        }
        private void OnDisable() {
            closeButton.onClick.RemoveListener(OnClose);
            towersList.OnTowerSelected -= OnTowerSelected;
            selectedTower.OnBuyButtonClicked -= OnBuyButtonClicked;
        }

        private void OnTowerSelected(int index) {
            selectedIndex = index;
            selectedTower.Setup(towerInfos[index]);
        }

        private void OnBuyButtonClicked() {
            OnTowerBuyClicked?.Invoke(selectedIndex);
        }
        private void OnClose() {
            Hide();
        }

    }
}