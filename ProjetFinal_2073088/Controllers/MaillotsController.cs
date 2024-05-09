using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProjetFinal_2073088.Data;
using ProjetFinal_2073088.Models;
using ProjetFinal_2073088.ViewModels;

namespace ProjetFinal_2073088.Controllers
{
    public class MaillotsController : Controller
    {
        private readonly ProjetFinal_MaillotsContext _context;

        public MaillotsController(ProjetFinal_MaillotsContext context)
        {
            _context = context;
        }

        // GET: Maillots
        public async Task<IActionResult> Index()
        {
            var projetFinal_MaillotsContext = _context.Maillots.Include(m => m.Promotion);
            return View(await projetFinal_MaillotsContext.ToListAsync());
        }
        public async Task<IActionResult> MaillotsPourUnClub(string nomEquipe)
        {
            string query = "EXEC Maillots.usp_MaillotsDisponiblePourUneCertaineEquipe @NomEquipe";
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter{ ParameterName = "NomEquipe",Value = nomEquipe }
             };
            return View("MaillotsEquipe", new MaillotEquipeViewModels()
            {
                maillots = await _context.Maillots.FromSqlRaw(query, parameters.ToArray()).ToListAsync(),
                
            });
        }
       
        
        public async Task<IActionResult> VueComplexeMaillots()
        {
           
            return View(await _context.VwMaillotsAvecPromotions.ToListAsync());

        }
        // GET: Maillots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Maillots == null)
            {
                return NotFound();
            }

            var maillot = await _context.Maillots
                .Include(m => m.Promotion)
                .FirstOrDefaultAsync(m => m.MaillotId == id);
            if (maillot == null)
            {
                return NotFound();
            }

            return View(maillot);
        }

        // GET: Maillots/Create
        public IActionResult Create()
        {
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId");
            return View();
        }

        // POST: Maillots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaillotId,NomEquipe,Taille,Couleur,Annee,EstPremium,Prix,PromotionId")] Maillot maillot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maillot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId", maillot.PromotionId);
            return View(maillot);
        }

        // GET: Maillots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Maillots == null)
            {
                return NotFound();
            }

            var maillot = await _context.Maillots.FindAsync(id);
            if (maillot == null)
            {
                return NotFound();
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId", maillot.PromotionId);
            return View(maillot);
        }

        // POST: Maillots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaillotId,NomEquipe,Taille,Couleur,Annee,EstPremium,Prix,PromotionId")] Maillot maillot)
        {
            if (id != maillot.MaillotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maillot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaillotExists(maillot.MaillotId))
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
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId", maillot.PromotionId);
            return View(maillot);
        }

        // GET: Maillots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Maillots == null)
            {
                return NotFound();
            }

            var maillot = await _context.Maillots
                .Include(m => m.Promotion)
                .FirstOrDefaultAsync(m => m.MaillotId == id);
            if (maillot == null)
            {
                return NotFound();
            }

            return View(maillot);
        }

        // POST: Maillots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Maillots == null)
            {
                return Problem("Entity set 'ProjetFinal_MaillotsContext.Maillots'  is null.");
            }
            var maillot = await _context.Maillots.FindAsync(id);
            if (maillot != null)
            {
                _context.Maillots.Remove(maillot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaillotExists(int id)
        {
          return (_context.Maillots?.Any(e => e.MaillotId == id)).GetValueOrDefault();
        }
    }
}
