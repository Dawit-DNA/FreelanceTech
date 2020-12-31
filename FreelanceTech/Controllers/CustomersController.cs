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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YenePaySdk;

namespace FreelanceTech.Controllers
{
    public class CustomersController : Controller
    {
        string currentUser = RegisterModel.registeredUser;
        private IJobRepository jobRepository;
        private ApplicationDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;
        private IWalletRepository walletRepository;
        private CheckoutOptions checkoutoptions;
        private string pdtToken = "APnMhGcBqU8Nfw";
        private ILogger<HomeController> _logger;
        public Wallet _wallet;

        public CustomersController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, IJobRepository jobRepository = null, ILogger<HomeController> logger = null, IProposalRepository proposalRepository = null, IWalletRepository walletRepository = null)
        {
            this.walletRepository = walletRepository;
            _logger = logger;
            this.jobRepository = jobRepository;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            string sellerCode = "0778";
            string successUrlReturn = "https://localhost:44346/Freelancer/Deposittowallet"; //"YOUR_SUCCESS_URL";
            string ipnUrlReturn = "https://localhost:44346/Freelancer/IPNDestination"; //"YOUR_IPN_URL";
            string cancelUrlReturn = "https://localhost:44346/Freelancer/PaymentCancelReturnUrl"; //"YOUR_CANCEL_URL";
            string failureUrlReturn = ""; //"YOUR_FAILURE_URL";
            bool useSandBox = true;
            checkoutoptions = new CheckoutOptions(sellerCode, string.Empty, CheckoutType.Express, useSandBox, null, successUrlReturn, cancelUrlReturn, ipnUrlReturn, failureUrlReturn);
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            string userId = User.GetUserId();
            var user = await _context.Users
             .FirstOrDefaultAsync(m => m.Id == userId);
            var cutomer = await _context.Customer
             .FirstOrDefaultAsync(m => m.customerId == userId);

            CustomerViewModel model = new CustomerViewModel();

            model.lastName = user.lastName;
            model.firstName = user.firstName;

            return View(model);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
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


                _context.Add(customer);
                await _context.SaveChangesAsync();
                _context.Add(address);
                await _context.SaveChangesAsync();

                _context.Add(lang);
                await _context.SaveChangesAsync();
           


                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }

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
        public IActionResult PostJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostJob(Job job)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                job.jobId = rnd.Next(1000, 1000000).ToString();
                jobRepository.PostJob(job);
                return View();
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
        public IActionResult DepositToWallet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DepositToWallet(Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                //CheckoutExpress(wallet);
                walletRepository.Deposit(wallet);
                return View();
            }
            return View();
        }
        [HttpPost]
        public void CheckoutExpress(Wallet wallet)
        {
            Random random = new Random();
            int num = random.Next(1, 30000);
            _wallet = new Wallet();
            _wallet.userId = num.ToString();
            _wallet.balance = Double.Parse(Request.Form["balance"]);
            checkoutoptions.Process = CheckoutType.Express;
            Random rnd = new Random();
            int Id = rnd.Next(1, 10000);
            var itemId = Convert.ToString(Id);
            var itemName = "Deposit To Freelance Tech Wallet";
            var unitPrice = decimal.Parse(Request.Form["balance"]);

            CheckoutItem checkoutitem = new CheckoutItem(itemId, itemName, unitPrice, 1, null, null, null, null, null);
            checkoutoptions.OrderId = null; //"YOUR_UNIQUE_ID_FOR_THIS_ORDER";  //can also be set null
            checkoutoptions.ExpiresAfter = 2880; //"NUMBER_OF_MINUTES_BEFORE_THE_ORDER_EXPIRES"; //setting null means it never expires
            var url = CheckoutHelper.GetCheckoutUrl(checkoutoptions, checkoutitem);
            Response.Redirect(url);
            RedirectToAction("DepositToWallet", _wallet);
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
            return RedirectToAction("DepositToWallet", _wallet);
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
