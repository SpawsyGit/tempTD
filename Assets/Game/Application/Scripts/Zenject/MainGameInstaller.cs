using Game.Common;
using Game.UI;
using log4net.Core;
using UnityEngine;
using Zenject;

namespace Game {
    public class MainGameInstaller : MonoInstaller {
        [SerializeField] private UIController uiController;
        [SerializeField] private TowerInfo[] towerInfos;
        private UIController uiControllerInstance;
        public override void InstallBindings() {
            BindUIController();
            BindUi();
            BindBank();
            BindInput();
            BindTowers();
        }
        private void BindUIController() {
            uiControllerInstance = Container.InstantiatePrefabForComponent<UIController>(uiController);
            Container.Bind<IUIController>().FromInstance(uiControllerInstance);
        }

        private void BindUi() {
            uiControllerInstance.BuildUI(Container);
        }
        private void BindBank() {
            Container.BindInterfacesAndSelfTo<Bank>().AsSingle();
        }
        private void BindInput() {
            Container.BindInterfacesAndSelfTo<InputClickManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GOInputClickManager>().AsSingle();
            
            
        }
        private void BindTowers() {
            Container.Bind<TowerInfo[]>().FromInstance(towerInfos).AsSingle();
            Container.BindInterfacesAndSelfTo<TowersManager>().AsSingle();
        }
    }
}