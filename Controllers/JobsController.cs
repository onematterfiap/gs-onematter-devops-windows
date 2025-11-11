using Microsoft.AspNetCore.Mvc;
using OneMatter.Data;
using OneMatter.Models;
using OneMatter.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OneMatter.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. LISTAR (INDEX) ---
        // GET: /Jobs/
        public async Task<IActionResult> Index()
        {
            var vagas = await _context.Jobs.ToListAsync();
            return View(vagas);
        }

        // --- 2. CRIAR (GET) ---
        // GET: /Jobs/Create
        public IActionResult Create()
        {
            var viewModel = new CreateJobViewModel();
            return View(viewModel);
        }

        // --- 3. CRIAR (POST) ---
        // POST: /Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJobViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var novaVaga = new Job(
                    viewModel.Title,
                    viewModel.Description,
                    viewModel.Location
                );
                _context.Jobs.Add(novaVaga);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // --- 4. EDITAR (GET) ---
        // GET: /Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Jobs.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            var viewModel = new CreateJobViewModel
            {
                Title = vaga.Title,
                Description = vaga.Description,
                Location = vaga.Location
            };

            return View(viewModel);
        }

        // --- 5. EDITAR (POST) ---
        // POST: /Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateJobViewModel viewModel)
        {
            var vagaParaAtualizar = await _context.Jobs.FindAsync(id);
            if (vagaParaAtualizar == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vagaParaAtualizar.UpdateDetails(
                        viewModel.Title,
                        viewModel.Description,
                        viewModel.Location
                    );

                    _context.Update(vagaParaAtualizar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Jobs.Any(e => e.Id == vagaParaAtualizar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // --- 6. DETALHES (GET) ---
        // GET: /Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // --- 7. EXCLUIR (GET) ---
        // GET: /Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // --- 8. EXCLUIR (POST) ---
        // POST: /Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaga = await _context.Jobs.FindAsync(id);
            if (vaga != null)
            {
                _context.Jobs.Remove(vaga);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}