using Game.UI;
using System;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;
using Zenject;
namespace Game {
    internal class InputClickManager : IUpdateListener, IDisposable {
        public event Action<Vector2> OnScreenClickedEvent;
        private IMainAppUpdater updater;
        private IUIController uiController;

        private readonly float maxClickTime = 0.2f;
        private readonly float maxClickMagnitude = 25.0f;
        private bool isClicking;
        private float clickStartTime;
        private Vector2 clickStartPosition;

        [Inject]
        public InputClickManager(IMainAppUpdater updater, IUIController uiController) {
            this.updater = updater;
            this.uiController = uiController;

            this.updater.AddListener(this);
        }

        private void DetectClick() {
            //Start click
            if (Input.GetMouseButtonDown(0) && !this.IsPointerOverUI()) {
                this.clickStartTime = Time.time;
                this.isClicking = true;
                this.clickStartPosition = Input.mousePosition;
            } 
            //End click
            else if (Input.GetMouseButtonUp(0)) {
                var isClicked = this.isClicking;
                this.isClicking = false;

                var clickPosition = Input.mousePosition;
                if (isClicked &&
                    this.IsClickDistance(clickPosition) &&
                    this.IsClickTimePeriod() &&
                    this.IsClickOnScene(clickPosition)) {
                    OnScreenClickedEvent?.Invoke(clickPosition);
                }
            }
            
        }
        private bool IsPointerOverUI() {
            return false;
        }
        private bool IsClickTimePeriod() {
            return Time.time - this.clickStartTime <= this.maxClickTime;
        }

        private bool IsClickDistance(Vector2 clickPosition) {
            return Vector2.Distance(clickPosition, this.clickStartPosition) <= this.maxClickMagnitude;
        }
        public void Dispose() {
            this.updater.RemoveListener(this);
        }
        private bool IsClickOnScene(Vector2 clickPosition) {
            var raycastResults = uiController.Raycast(clickPosition);
            return raycastResults.Count == 0;
        }

        public void OnUpdate(float deltaTime) {
            DetectClick();
        }


    }
}