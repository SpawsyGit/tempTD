using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "TowerInfo", menuName = "Gameplay/Tower")]
    public class TowerInfo : ScriptableObject {
        [SerializeField] private Sprite towerSprite;
        [SerializeField] private int towerCost;
        [SerializeField] private TowerGO towerPrefab;

        public TowerGO TowerPrefab => towerPrefab;
        public Sprite TowerSprite => towerSprite;
        public int TowerCost => towerCost;
    }
}