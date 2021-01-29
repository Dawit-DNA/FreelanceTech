using FreelanceTech.Areas.Identity.Pages.Account;
using FreelanceTech.Data;
using FreelanceTech.Models;
using FreelanceTech.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Web.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YenePaySdk;

namespace FreelanceTech.Controllers
{
    public class CustomersController : Controller
    {
        string currentUser;
        private IJobRepository jobRepository;
        private ApplicationDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;
        private IWalletRepository walletRepository;
        private CheckoutOptions checkoutoptions;
        private string pdtToken = "APnMhGcBqU8Nfw";
        private ILogger<HomeController> _logger;

        public CustomersController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, IJobRepository jobRepository = null, ILogger<HomeController> logger = null, IProposalRepository proposalRepository = null, IWalletRepository walletRepository = null)
        {
            currentUser = RegisterModel.registeredUser;
            this.walletRepository = walletRepository;
            _logger = logger;
            this.jobRepository = jobRepository;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            string sellerCode = "0778";
            string successUrlReturn = "https://localhost:44346/Customers/Success"; //"YOUR_SUCCESS_URL";
            string ipnUrlReturn = "https://localhost:44346/Freelancer/IPNDestination"; //"YOUR_IPN_URL";
            string cancelUrlReturn = "https://localhost:44346/Freelancer/PaymentCancelReturnUrl"; //"YOUR_CANCEL_URL";
            string failureUrlReturn = ""; //"YOUR_FAILURE_URL";
            bool useSandBox = true;
            checkoutoptions = new CheckoutOptions(sellerCode, string.Empty, CheckoutType.Express, useSandBox, null, successUrlReturn, cancelUrlReturn, ipnUrlReturn, failureUrlReturn);
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            UserViewModel userViewModel = new UserViewModel();
            string userId = User.GetUserId();
            var customer = await _context.Customer
                  .FirstOrDefaultAsync(m => m.customerId == userId);
            var address = await _context.Address
                 .FirstOrDefaultAsync(m => m.userId == userId);
            var user = await _context.Users
                             .FirstOrDefaultAsync(m => m.Id == userId);
            var job = _context.Job.Where(m => m.customerId == userId);
            FreelancerViewModel freelancer = new FreelancerViewModel();
            List<ProposalViewModel> proposals = new List<ProposalViewModel>();
            ProposalViewModel proposal = new ProposalViewModel();
            foreach (var item in job)
            {
                var prop = _context.Proposal.Where(m => m.jobId == item.jobId);
                if (prop != null)
                {
                    foreach (var i in prop)
                    {
                        var userr = _context.Users.FirstOrDefault(m => m.Id == i.freelancerId);
                        var freelancer1 = _context.Freelancer.FirstOrDefault(m => m.freelancerId == i.freelancerId);
                        freelancer.freelancerId = item.freelancerId;
                        freelancer.firstName = userr.firstName;
                        freelancer.lastName = userr.lastName;
                        freelancer.email = userr.Email;
                        freelancer.phoneNumber = freelancer1.phoneNumber;
                        freelancer.title = freelancer.category;
                        proposal.freelancer = freelancer;
                        proposal.job = item;
                        proposal.freelancerId = item.freelancerId;
                        proposal.bidAmount = i.bidAmount;
                        proposal.description = i.description;
                        proposal.answers = i.answers;
                        proposals.Add(proposal);
                    }
                }
            }
            CustomerViewModel model = new CustomerViewModel();
            model.customerId = customer.customerId;
            model.phoneNumber = customer.phoneNumber;
            model.lastName = user.lastName;
            model.firstName = user.firstName;

            model.photo = customer.photo;

            userViewModel.customerViewModel = model;
            userViewModel.jobs = _context.Job.Where(e => e.customerId.Contains(userId)).ToList();
            userViewModel.proposals = proposals;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult AcceptBid()
        {
            string jobId = Request.Form["jobId"];
            string freelancerId = Request.Form["freelancerId"];
            var job = _context.Job.FirstOrDefault(m => m.jobId == jobId);
            job.status = "OnGoing";
            job.freelancerId = freelancerId;
            _context.Job.Update(job);
            return RedirectToAction("Index", "Customer");
        }
        [HttpGet]
        public async Task<IActionResult> BusinessAnalyst()
        {
            //UserViewModel userViewModel = new UserViewModel();
            //string userId = User.GetUserId();
            //var customer = await _context.Customer
            //      .FirstOrDefaultAsync(m => m.customerId == userId);
            //var user = await _context.Users
            //                 .FirstOrDefaultAsync(m => m.Id == userId);
            //CustomerViewModel model = new CustomerViewModel();
            //model.customerId = customer.customerId;
            //model.phoneNumber = customer.phoneNumber;
            //model.lastName = user.lastName;
            //model.firstName = user.firstName;

            //model.photo = customer.photo;

            //userViewModel.customerViewModel = model;
            //return View(userViewModel);
            return View();
        }
        //[HttpPost]
        //public IActionResult BusinessAnalyst(BusinessAnalyst businessAnalyst)
        //{
        //    return View();
        //}
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Register()
        {

            return View();
        }
      
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Register(CreateCustomerViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(viewmodel);
                Customer customer = new Customer();


                customer.customerId = currentUser;

                customer.englishProficiency = (int)viewmodel.englishProficiency;
                string userId = User.GetUserId();             
                customer.photo = uniqueFileName;
                customer.status = (int)Constants.status.Active;
                customer.phoneNumber = viewmodel.phoneNumber;

                Language lang = new Language();
                lang.userId = currentUser;
                lang.language = (int)viewmodel.language;

                Address address = new Address();
                address.region = viewmodel.region;
                address.city = viewmodel.city;
                address.woreda = viewmodel.woreda;
                address.houseNumber = Convert.ToInt32(viewmodel.houseNumber.ToString());
                address.pobox = Convert.ToInt32(viewmodel.pobox.ToString());
                address.userId = currentUser;
                Wallet wall = new Wallet();
                wall.userId = RegisterModel.registeredUser;
                wall.balance = 0;
                walletRepository.Deposit(wall);

                _context.Add(customer);
                await _context.SaveChangesAsync();
                _context.Add(address);
                await _context.SaveChangesAsync();

                _context.Add(lang);
                await _context.SaveChangesAsync();
           


                return RedirectToAction("Index","Home");
            }
            return View(viewmodel);
        }
        [HttpGet]
        public IActionResult ViewProfile()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult ViewProfile()
        //{
        //    string freelancerId = RegisterModel.registeredUser;
        //    var user = _context.Users.FirstOrDefault(m => m.Id == freelancerId);
        //    var freelancer = _context.Freelancer.FirstOrDefault(m => m.freelancerId == freelancerId);
        //    CustomerViewModel userViewModel = new CustomerViewModel();
        //    userViewModel.firstName = user.firstName;
        //    userViewModel.lastName = user.lastName;
        //    userViewModel.email = user.Email;
        //    userViewModel.phoneNumber = freelancer.phoneNumber;
        //    userViewModel.title = freelancer.title;
        //    userViewModel.category = freelancer.category;
        //    userViewModel.skills = freelancer.skills;
        //}
        private string ProcessUploadedFile(CreateCustomerViewModel model)
        {
            string uniqueFileName = null;
            if (model.photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public async Task<IActionResult> PostJob()
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
            UserViewModel userViewModel = new UserViewModel() { customerViewModel = model };
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> PostJob(Job job)
        {
            if (ModelState.IsValid)
            {
                string userId = RegisterModel.registeredUser;
                Random rnd = new Random();
                job.jobId = rnd.Next(1000, 1000000).ToString();
                job.status = "Not Started";
                job.customerId = userId;
                jobRepository.PostJob(job);
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
                UserViewModel userViewModel = new UserViewModel() { customerViewModel = model };
                RedirectToAction("Index","Customer",userViewModel);
            }
            return View();
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("customerId,photo,legalId,status,language,phoneNumber,englishProficiency")] Customer customer)
        {
            if (id != customer.customerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.customerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }


        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SuccessAsync(Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                string referer = Request.Headers["Referer"].ToString();
                string refer = referer.Substring(54, 3);
                double amount = Convert.ToDouble(refer);
                //wallet.balance = Double.Parse(Request.["TotalAmount"]);
                Wallet newWallet = new Wallet();
                newWallet.userId = RegisterModel.registeredUser;
                newWallet.balance = amount;
                //_context.Wallet.Update(newWallet);
                //await _context.SaveChangesAsync();
                //return RedirectToAction("Index", "Wallets");
                if(walletRepository.Deposit(newWallet) == true)
                {
                    return RedirectToAction("Index", "Wallets");
                }
                return RedirectToAction("Index", "Error");
                
            }
            return RedirectToAction("Index", "Wallets");
        }
        [HttpPost]
        public void CheckoutExpress(CustomerViewModel customer)
        {
            //CustomerViewModel customer = new CustomerViewModel();
            customer.wallet = new Wallet();
            customer.wallet.userId = RegisterModel.registeredUser;
            customer.wallet.balance = Double.Parse(Request.Form["balance"]);
            checkoutoptions.Process = CheckoutType.Express;
            Random rnd = new Random();
            int Id = rnd.Next(1, 10000);
            var itemId = Convert.ToString(Id);
            var itemName = "Deposit To Freelance Tech Wallet";
            var unitPrice = Convert.ToDecimal(customer.wallet.balance);

            CheckoutItem checkoutitem = new CheckoutItem(itemId, itemName, unitPrice, 1, null, null, null, null, null);
            checkoutoptions.OrderId = null; //"YOUR_UNIQUE_ID_FOR_THIS_ORDER";  //can also be set null
            checkoutoptions.ExpiresAfter = 2880; //"NUMBER_OF_MINUTES_BEFORE_THE_ORDER_EXPIRES"; //setting null means it never expires
            var url = CheckoutHelper.GetCheckoutUrl(checkoutoptions, checkoutitem);
            Response.Redirect(url);
            RedirectToAction("DepositToWallet", customer.wallet);
        }
        [HttpGet]
        public async Task<IActionResult> SearchFreelancers()
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
            UserViewModel userViewModel = new UserViewModel() { customerViewModel = model };
            var freelancers =  _context.Freelancer
                .FromSqlRaw<Freelancer>("SELECT * FROM Freelancer")
                .ToList();
            
              List<FreelancerViewModel> freelancerViewModels = new List<FreelancerViewModel>();
            foreach (var userr in freelancers)
            {
                FreelancerViewModel freelancer = new FreelancerViewModel();
                var users = _context.Users.Where(e => e.Id.Contains(userr.freelancerId)).FirstOrDefault();
                var addresses = _context.Address.Where(e => e.userId.Contains(userr.freelancerId)).FirstOrDefault();
                freelancer.freelancerId = users.Id;
                freelancer.firstName = users.firstName;
                freelancer.lastName = users.lastName;
                freelancer.email = users.Email;
                freelancer.phoneNumber = userr.phoneNumber;
                freelancer.englishProficiency = userr.englishProficiency;
                freelancer.rate = userr.rate;
                freelancer.education = userr.education;
                freelancer.photo = userr.photo;
                freelancer.city = addresses.city;
                freelancer.pobox = addresses.pobox;
                freelancerViewModels.Add(freelancer);
                //freelancer = null;
            }
            userViewModel.freelancers = freelancerViewModels;
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SearchFreelancers(string keyword)
        {
            string userId = RegisterModel.registeredUser;
            var customer = await _context.Customer
                  .FirstOrDefaultAsync(m => m.customerId == userId);
            var address = await _context.Address
                 .FirstOrDefaultAsync(m => m.userId == userId);
            var userss = await _context.Users
                             .FirstOrDefaultAsync(m => m.Id == userId);
            CustomerViewModel model = new CustomerViewModel();
            model.customerId = customer.customerId;
            model.phoneNumber = customer.phoneNumber;
            model.lastName = userss.lastName;
            model.firstName = userss.firstName;

            model.photo = customer.photo;
            UserViewModel userViewModel = new UserViewModel() { customerViewModel = model };
            keyword = Request.Form["keyword"].ToString();
            keyword = StringExtensions.FirstCharToUpper(keyword);
            if (keyword != null)
            {
                List<FreelancerViewModel> freelancerViewModels = new List<FreelancerViewModel>();
                FreelancerViewModel freelancer = new FreelancerViewModel();
                var users = _context.Users.Where(e => e.firstName.Contains(keyword) ||
                                e.lastName.Contains(keyword)).ToList();
                List<Freelancer> freelancers = new List<Freelancer>();
                List<Address> addresses = new List<Address>();
                foreach(var user in users)
                {
                    freelancer.freelancerId = user.Id;
                    freelancer.firstName = user.firstName;
                    freelancer.lastName = user.lastName;
                    freelancer.email = user.Email;
                    freelancers.Add(_context.Freelancer.Where(e => e.freelancerId.Contains(user.Id)).FirstOrDefault());
                    foreach (var item in freelancers)
                    {
                        freelancer.phoneNumber = item.phoneNumber;
                        freelancer.englishProficiency = item.englishProficiency;
                        freelancer.rate = item.rate;
                        freelancer.education = item.education;
                        freelancer.photo = item.photo;
                    }
                    addresses.Add(_context.Address.Where(e => e.userId.Contains(user.Id)).FirstOrDefault());
                    foreach (var col in addresses)
                    {
                        freelancer.city = col.city;
                        freelancer.pobox = col.pobox;
                    }
                    freelancerViewModels.Add(freelancer);
                }
                userViewModel.freelancers = freelancerViewModels;
                return View(userViewModel);
            }
            return View(userViewModel);
        }


        [HttpPost]
        public async Task<string> IPNDestination(IPNModel ipnModel)
        {
            var result = string.Empty;
            ipnModel.UseSandbox = checkoutoptions.UseSandbox;
            if (ipnModel != null)
            {
                var isIPNValid = await CheckIPN(ipnModel);

                if (isIPNValid)
                {
                    //This means the payment is completed
                    //You can now mark the order as "Paid" or "Completed" here and start the delivery process
                }
            }
            return result;
        }

        public async Task<ActionResult> PaymentSuccessReturnUrl(IPNModel ipnModel)
        {
            PDTRequestModel model = new PDTRequestModel(pdtToken, ipnModel.TransactionId, ipnModel.MerchantOrderId);
            model.UseSandbox = checkoutoptions.UseSandbox;
            var pdtResponse = await CheckoutHelper.RequestPDT(model);
            if (pdtResponse.Count() > 0)
            {
                if (pdtResponse["Status"] == "Paid")
                {
                    //This means the payment is completed. 
                    //You can extract more information of the transaction from the pdtResponse dictionary
                    //You can now mark the order as "Paid" or "Completed" here and start the delivery process
                }
            }
            else
            {
                //This means the pdt request has failed.
                //possible reasons are 
                //1. the TransactionId is not valid
                //2. the PDT_Key is incorrect
            }
            //return RedirectToAction("DepositToWallet", _wallet);
            return null;
        }

        public async Task<string> PaymentCancelReturnUrl(IPNModel ipnModel)
        {
            PDTRequestModel model = new PDTRequestModel(pdtToken, ipnModel.TransactionId, ipnModel.MerchantOrderId);
            var pdtResponse = await CheckoutHelper.RequestPDT(model);
            if (pdtResponse.Count() > 0)
            {
                if (pdtResponse["Status"] == "Canceled")
                {
                    //This means the payment is canceled. 
                    //You can extract more information of the transaction from the pdtResponse dictionary
                    //You can now mark the order as "Canceled" here.
                }
            }
            else
            {
                //This means the pdt request has failed.
                //possible reasons are 
                //1. the TransactionId is not valid
                //2. the PDT_Key is incorrect
            }
            return string.Empty;
        }
        private async Task<bool> CheckIPN(IPNModel model)
        {
            return await CheckoutHelper.IsIPNAuthentic(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
