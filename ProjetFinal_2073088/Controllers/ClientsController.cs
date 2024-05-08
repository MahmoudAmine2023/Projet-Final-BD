using System;
using System.Collections.Generic;
using System.Data;
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
    public class ClientsController : Controller
    {
        private readonly ProjetFinal_MaillotsContext _context;

        public ClientsController(ProjetFinal_MaillotsContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
              return _context.Clients != null ? 
                          View(await _context.Clients.ToListAsync()) :
                          Problem("Entity set 'ProjetFinal_MaillotsContext.Clients'  is null.");
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);

            ProfileClientViewModel profileClientViewModel = new ProfileClientViewModel()
            {
                Client = client,
                Courriel  = client.CourrielEncrypt.ToString(),
               
            };
            if (client == null)
            {
                return NotFound();
            }
            //await DechiffrementCourriel(id);
            return View(profileClientViewModel);
        }
        public async Task<ViewResult> DechiffrementCourriel(int clientID)
        {
   
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("Clients.USP_DeChiffrementAdresseCourrielClient", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ClientID", clientID));

                        var decryptedEmailResult = string.Empty;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                decryptedEmailResult = reader["DecryptedEmail"].ToString();
                            }
                        }
                        var client = await _context.Clients
                            .FirstOrDefaultAsync(m => m.ClientId == clientID);

                        ViewData["DecryptedEmail"] = decryptedEmailResult;
                        ProfileClientViewModel clientView = new ProfileClientViewModel()
                        {
                            Courriel = decryptedEmailResult,
                            Client = client
                        };
                        return View("Details", clientView);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                ModelState.AddModelError("", $"Une erreur est survenue : {ex.Message}");
                return View();
            }
            
        }
        //public async Task<ViewResult> DeChiffrementCourriel(int clientID)
        //{
        //    string query = "EXEC Clients.USP_DeChiffrementAdresseCourrielClient @ClientID";
        //    SqlParameter parameter = new SqlParameter { ParameterName = "ClientID", Value = clientID };
        //    try
        //    {
                

        //        string[] decryptedEmail =  _context.Clients.FromSqlRaw(query, parameter).ToArrayAsync();
        //        string decryptedEmail1 = decryptedEmail.ToString();
                

        //    }
        //    catch (Exception)
        //    {

        //        ModelState.AddModelError("", "Une erreur est survenue");
        //    }
        //    return View();

        //}
        

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,Nom,Adresse,Telephone,CourrielEncrypt")] Client client)
        {

            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
          
            return View(client);
        }
       

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Nom,Adresse,Telephone,CourrielEncrypt")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ProjetFinal_MaillotsContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
