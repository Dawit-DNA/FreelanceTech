﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreelanceTech.Data;
using FreelanceTech.Models;
using FreelanceTech.Areas.Identity.Pages.Account;
using FreelanceTech.ViewModel;

namespace FreelanceTech.Controllers
{
    public class WalletsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWalletRepository walletRepository;

        public WalletsController(ApplicationDbContext context, IWalletRepository walletRepository)
        {
            _context = context;
            this.walletRepository = walletRepository;
        }

        // GET: Wallets
        public async Task<IActionResult> Index()
        {
            string userId = RegisterModel.registeredUser;
            var customer = await _context.Customer
                  .FirstOrDefaultAsync(m => m.customerId == userId);
            var address = await _context.Address
                 .FirstOrDefaultAsync(m => m.userId == userId);
            var user = await _context.Users
                             .FirstOrDefaultAsync(m => m.Id == userId);
            CustomerViewModel model = new CustomerViewModel();
            model.customerId = customer.customerId;
            model.phoneNumber = customer.phoneNumber;
            model.lastName = user.lastName;
            model.firstName = user.firstName;

            model.photo = customer.photo;

            Wallet wallet = walletRepository.Balance(RegisterModel.registeredUser);
            model.wallet = wallet;
            return View();
        }

        // GET: Wallets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet
                .FirstOrDefaultAsync(m => m.userId == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // GET: Wallets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userId,balance")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("userId,balance")] Wallet wallet)
        {
            if (id != wallet.userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wallet);
        }

        // GET: Wallets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet
                .FirstOrDefaultAsync(m => m.userId == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var wallet = await _context.Wallet.FindAsync(id);
            _context.Wallet.Remove(wallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletExists(string id)
        {
            return _context.Wallet.Any(e => e.userId == id);
        }
    }
}
