using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OwodaApp.Data;
using OwodaApp.Models;

namespace OwodaApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly OwodaAppContext _context;

        public PaymentsController(OwodaAppContext context)
        {
            _context = context;
           
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {  
            return View();
        }

        public async Task<IActionResult> ShowAllMembers()
        {
            var owodaAppContext = _context.Payment.Include(p => p.Member).Include(p => p.Vehicle);
            return View(await owodaAppContext.ToListAsync());
        }

        public async Task<IActionResult> SearchShowFrom()
        {
            //Post show list
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> SearchResult(string SearchPhrase)
        {
            var owodaAppContext = _context.Payment.Include(p => p.Member).Include(p => p.Vehicle).Where(x=>x.MemberId == Int32.Parse(SearchPhrase));
            return View(await owodaAppContext.ToListAsync());
        }


        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Member)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "MemberId");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "VehicleId");
            return View();
        }

       

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,MemberId,VehicleId,Amount")] Payment payment, string NoOfTrips)
        {
            //if (ModelState.IsValid)
            //   {
            if (payment.Amount == 0)
            {
                var vType = _context.Vehicle.Where(x => x.VehicleId == payment.VehicleId).Select(x => x.VehicleType).First();
                if(vType.ToLower() == "bus")
                {
                    var amount = 200 * Int32.Parse(NoOfTrips) * 0.9;
                    ViewBag.Amount = amount.ToString();
                    ViewBag.VId = payment.VehicleId;
                    ViewBag.MId = payment.MemberId;
                    return View();
                }
            }

            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "MemberId");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "VehicleId");
            payment.PaymentDate = DateTime.UtcNow.AddHours(1);
            if (_context.Member.Where(x=>x.MemberId==payment.MemberId).SelectMany(x => x.Vehicles).Where(x => x.VehicleId==payment.VehicleId).Any())
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.InvalidId = "No Vehicle registered to Member";
            return View();
         //   }
         //   ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", payment.MemberId);
        //    ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "VehicleMake", payment.VehicleId);
        //    return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", payment.MemberId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "VehicleMake", payment.VehicleId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,MemberId,VehicleId,Amount,PaymentDate")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email", payment.MemberId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "VehicleMake", payment.VehicleId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Member)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'OwodaAppContext.Payment'  is null.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payment?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }
    }
}
