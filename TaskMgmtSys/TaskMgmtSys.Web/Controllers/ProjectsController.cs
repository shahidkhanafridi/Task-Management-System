using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMgmtSys.Web.Data;
using TaskMgmtSys.Web.Entities;

namespace TaskMgmtSys.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProjectsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _dbContext.Projects
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(projects);
        }

        // GET: /Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Project model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.CreatedAt = DateTime.UtcNow;
            _dbContext.Projects.Add(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Projects/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // POST: /Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Project model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dbProject = await _dbContext.Projects.FindAsync(model.Id);
            if (dbProject == null) return NotFound();

            dbProject.ProjectName = model.ProjectName;
            dbProject.Description = model.Description;
            dbProject.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Projects/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _dbContext.Projects
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return Json(projects);
        }
    }
}
