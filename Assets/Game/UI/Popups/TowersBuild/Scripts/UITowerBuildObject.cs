using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class UITowerBuildObject : MonoBehaviour {
        public event Action<int> OnTowerClicked;
        [SerializeField] Image towerImage;
        [SerializeField] Button clickButton;
        private int index;

        public int Index => index;
        public void Setup(TowerInfo info, int index) {
            towerImage.sprite = info.TowerSprite;
            this.index = index;
        }

        private void OnEnable() {
            clickButton.onClick.AddListener(OnButtonClicked);
        }
        private void OnDisable() {
            clickButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() {
            OnTowerClicked(index);
        }
    }
}