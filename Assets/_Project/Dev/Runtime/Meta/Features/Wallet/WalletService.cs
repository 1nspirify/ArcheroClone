using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Dev.Runtime.Utilities.Reactive;

namespace _Project.Dev.Runtime.Meta.Features.Wallet
{
    public class WalletService
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;

        public WalletService(Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies)
        {
            _currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>(currencies);
        }

        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList();

        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes currency) => _currencies[currency];

        public bool Enough(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _currencies[type].Value >= amount;
        }

        public void Add(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value += amount; 
        }

        public void Spend(CurrencyTypes type, int amount) 
        {
            if (Enough(type, amount) == false)
                throw new InvalidOperationException($"Cannot spend {amount} to {type.ToString()}");

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));  
            
            _currencies[type].Value -= amount;
        }
    }
    
    //Можно сделать его с дженериками 
}