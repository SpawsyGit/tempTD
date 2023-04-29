using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    internal class UITowersList : UIElement {
        public event Action<int> OnTowerSelected;
        [SerializeField] UITowerBuildObject towerPrefab;
        [SerializeField] Transform towersRoot;

        private List<UITowerBuildObject> towerList;

        public void Setup(TowerInfo[] infos) {
            towerList = new List<UITowerBuildObject>();
            for (int i = 0;  i < infos.Length; i++) {
                var t = Instantiate(towerPrefab, towersRoot);
                t.Setup(infos[i], i);
                towerList.Add(t);
                t.OnTowerClicked += OnTowerClicked;
            }
        }

        private void OnDisable() {
            Clear();
        }
        private void OnTowerClicked(int index) {
            OnTowerSelected?.Invoke(index);
        }
        private void Clear() {
            if (towerList == null) return;
            foreach (var t in towerList) {
                t.OnTowerClicked -= OnTowerClicked;
                Destroy(t.gameObject);
            }
        }
    }
}