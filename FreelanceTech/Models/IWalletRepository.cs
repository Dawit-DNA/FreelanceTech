using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    public interface IWalletRepository
    {
        public bool Deposit(Wallet wallet);
        Wallet Balance(string Id);
    }
}
