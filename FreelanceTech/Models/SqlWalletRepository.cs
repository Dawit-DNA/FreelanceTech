﻿using FreelanceTech.Areas.Identity.Pages.Account;
using FreelanceTech.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    public class SqlWalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public SqlWalletRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public Wallet Balance(string Id)
        {
            return applicationDbContext.Wallet.Find(Id);
        }

        public bool Deposit(Wallet wallet)
        {
            Wallet oldWallet = applicationDbContext.Wallet.Find(RegisterModel.registeredUser);
            if (oldWallet != null){
                oldWallet.balance += wallet.balance;
                applicationDbContext.Wallet.Update(oldWallet);
                applicationDbContext.SaveChanges();
            }
            else
            {
                applicationDbContext.Wallet.Add(wallet);
                applicationDbContext.SaveChanges();
            }
            return true;
        }
    }
}
