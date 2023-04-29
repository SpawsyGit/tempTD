using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game {
    public class Tower : IUpdateListener, IDisposable {
        private IUIController uiController;
        public Tower() {

        }


        public void OnGameObjectClicked(GameObject targetObject) {
            if (!targetObject.TryGetComponent(out TowerGO towerGO)) return;

            //var popup = uiController.ShowUIElement<>();
            //popup.Init(towerGO);
        }

        public void OnUpdate(float deltaTime) {
            throw new System.NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}