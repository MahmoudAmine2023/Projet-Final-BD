using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
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
        public async Task<IActionResult> AfficherImageEtDetailsMaillotss(int id)
        {
            var maillotWithPhoto = await _context.Maillots
                .Where(x => x.MaillotId == id && x.Photo != null) 
                .ToListAsync();

            var ivm = new AfficherImageEtDetailsMaillotss
            {
                idMaillot = id,
                ImageUrl = maillotWithPhoto
                    .Select(x => $"data:image/png;base64,{Convert.ToBase64String(x.Photo)}")
                    .ToList()
            };

            return View("DetailsMaillotsAvecImages", ivm);
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
        public IActionResult AjouterImage()
        {
            return View("AjouterImage");
        }
        [HttpPost]
        public async Task<IActionResult> AjouterImage(ImageUploadVM iuvm)
        {
            
            if (_context.Maillots == null)
            {
                return Problem("Entity set  is null.");
            }

            if (ModelState.IsValid)
            {
                

                Maillot maillot = await _context.Maillots.FirstOrDefaultAsync(x => x.MaillotId == iuvm.idMaillot);
                if (maillot == null)
                {
                    ModelState.AddModelError("Maillot", "Ce maillot n'existe pas.");
                    return View();
                }

                // Récupérer l'image dans iuvm.FormFile
                // Remplir la propriété fruit.Photo

                if (iuvm.FormFile != null && iuvm.FormFile.Length >= 0)
                {
                    MemoryStream stream = new MemoryStream();
                    await iuvm.FormFile.CopyToAsync(stream);
                    byte[] photo = stream.ToArray();
                    maillot.Photo = photo;
                    
                   // iuvm. = photo;
                }

                await _context.SaveChangesAsync();
                ViewData["message"] = "Image ajoutée pour "  + " !";
                return View("Details",maillot);
            }
            ModelState.AddModelError("", "Il y a un problème avec le fichier fourni");
            return View();
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
        public async Task<IActionResult> FilteredMaillots(List<string> selectedTeams, List<int> selectedYears)
        {
            var allTeams = await _context.Maillots.Select(m => m.NomEquipe).Distinct().ToListAsync();
            var allYears = await _context.Maillots.Select(m => m.Annee).Distinct().ToListAsync();

            IQueryable<Maillot> query = _context.Maillots;

            if (selectedTeams != null && selectedTeams.Any())
            {
                query = query.Where(m => selectedTeams.Contains(m.NomEquipe));
            }

            if (selectedYears != null && selectedYears.Any())
            {
                query = query.Where(m => selectedYears.Contains(m.Annee));
            }

            // Execute the query and fetch the data only after all filters have been applied
            var maillots = await query.ToListAsync();

            var viewModel = new MaillotFiltre
            {
                AllTeams = allTeams,
                AllYears = allYears,
                SelectedTeams = selectedTeams,
                SelectedYears = selectedYears,
                Maillots = maillots
            };

            return View(viewModel);
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
