using Zenject;

namespace Game {
    public class Bank : IBank {
        private int currencyAmount;

        public int CurrentAmount => currencyAmount;
        [Inject]
        public Bank() {
            currencyAmount = 1000;
        }

        public bool CanSpendMoney(int amount) {
            return currencyAmount >= amount;
        }

        public void SpendMoney(int amount) {
            if (!CanSpendMoney(amount))
                throw new System.Exception("Failed bank transaction!");
            currencyAmount -= amount;
        }

        public void AddMoney(int amount) {
            currencyAmount += amount;
        }
    }
}