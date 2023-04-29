using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public interface IGOInputClickManager {
        void AddListener(IGOInputClickListener listener);

        void RemoveListener(IGOInputClickListener listener);
    }
    internal class GOInputClickManager : IGOInputClickManager, IDisposable {
        [SerializeField]
        private float maxRaycastDistance = 500;

        private List<IGOInputClickListener> listeners;
        private Camera mainCamera;
        InputClickManager clickManager;

        public GOInputClickManager(/*Camera mainCamera, */InputClickManager clickManager) {
            this.mainCamera = Camera.main;
            Debug.Log(mainCamera.name);
            this.clickManager = clickManager;
            this.clickManager.OnScreenClickedEvent += this.OnScreenClicked;

            listeners = new List<IGOInputClickListener>();
        }
        public void AddListener(IGOInputClickListener listener) {
            listeners.Add(listener);
        }


        public void RemoveListener(IGOInputClickListener listener) {
            this.listeners.Remove(listener);
        }
        private void OnScreenClicked(Vector2 screenPosition) {
            var clickRay = this.mainCamera.ScreenPointToRay(screenPosition);
            var hitInfo = Physics2D.Raycast(clickRay.origin, clickRay.direction, maxRaycastDistance);
            if (!hitInfo) return;

            var targetObject = hitInfo.transform.gameObject;
            for (int i = 0, count = this.listeners.Count; i < count; i++) {
                var listener = this.listeners[i];
                listener.OnGameObjectClicked(targetObject);
            }
        }

        public void Dispose() {
            this.clickManager.OnScreenClickedEvent -= this.OnScreenClicked;
        }
    }
}