using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public interface IBank {
        public int CurrentAmount { get; }
        public bool CanSpendMoney(int amount);

        public void SpendMoney(int amount);

        public void AddMoney(int amount);
    }
}