using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game {
    internal class TowersManager : IGOInputClickListener, IDisposable {
        private IUIController uiController;
        private TowersFabric fabric;
        private IBank bank;
        private IGOInputClickManager clickManager;
        private List<TowerGO> towers;
        private TowerInfo[] towerInfos;
        private TowerPlace selectedPlace;

        public TowersManager(IUIController uiController, IBank bank, IGOInputClickManager clickManager, TowerInfo[] towerInfos) {
            this.uiController = uiController;
            this.bank = bank;
            this.clickManager = clickManager;
            this.towerInfos = towerInfos;
            Setup();
            
        }
        private void Setup() {
            towers = new List<TowerGO>();
            clickManager.AddListener(this);
        }
        public void OnGameObjectClicked(GameObject targetObject) {            
            if (!targetObject.TryGetComponent(out TowerPlace towerPlace)) return;
            selectedPlace = towerPlace;
            OpenTowersBuildPopup();
        }

        private void OpenTowersBuildPopup() {
            var popup = uiController.ShowUIElement<UIPopupTowersBuild>();
            popup.Setup(towerInfos);
            popup.OnTowerBuyClicked += OnClickBuildTower;
            popup.OnPopupHiddenCompletelyEvent += OnPopupClosed;
        }
        private void OnPopupClosed(IUIPopup element) {
            var popup = element as UIPopupTowersBuild;
            popup.OnTowerBuyClicked -= OnClickBuildTower;
            popup.OnPopupHiddenCompletelyEvent -= OnPopupClosed;
        }
        private void OnClickBuildTower(int index) {
            var info = towerInfos[index];
            if (!bank.CanSpendMoney(info.TowerCost)) Debug.Log("No money?");
            BuildTower(info);
        }
        private void BuildTower(TowerInfo info) {
            var tower = GameObject.Instantiate(info.TowerPrefab, selectedPlace.transform);
            tower.transform.position = selectedPlace.transform.position;
            towers.Add(tower);
        }

        public void Dispose() {
            clickManager.RemoveListener(this);
        }
    }
}
