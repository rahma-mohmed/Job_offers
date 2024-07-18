using Job_offers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Job_offers.Controllers
{
    public class RolesController : Controller
    {
        private readonly JobContext context;

        public RolesController(JobContext _context)
        {
              context = _context;
        }
        // GET: RolesController
        public ActionResult Index()
        {
            List<RoleViewModel> roles = context.Roles.ToList();
            return View(roles);
        }

        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            var rol = context.Roles.Find(id);
            if(rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel role)
        {
                if (ModelState.IsValid) { 
                    context.Roles.Add(role);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
        }

        // GET: RolesController/Edit/5
        public IActionResult Edit(int id)
        {
            RoleViewModel role = context.Roles.FirstOrDefault(x => x.Id == id);
            return View(role);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                RoleViewModel rl = context.Roles.FirstOrDefault(x => x.Id == id);
                rl.Name = role.Name;
                context.Roles.Update(rl);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
