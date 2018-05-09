using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp1.Data;
using Tp1.Models;

namespace Tp1.Controllers
{
    public class UsersController : Controller
    {
        //private readonly Tp1Context _context;
        private readonly UnitOfWork _unitOfWork;

        /*public UsersController(Tp1Context context)
        {
            _context = context;
        }*/

        public UsersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: Users
        public ViewResult Index()
        {
            return View(_unitOfWork.UserRepository.Get());
        }

        
        // GET: Users/Details/5

        public ViewResult Details(int id)
        {
            return View(_unitOfWork.UserRepository.GetById(id));
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(user);
                    //await _context.SaveChangesAsync();
                    _unitOfWork.UserRepository.Insert(user);
                    _unitOfWork.save();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException  /* ex */ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(user);
        }
        
        //GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _unitOfWork.UserRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(user);
                    //await _context.SaveChangesAsync();
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.save();
                }
                catch (DbUpdateConcurrencyException /* dex */)
                {
                    //if (!UserExists(user.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    ModelState.AddModelError("", "Unable to update");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        
        // GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //   return NotFound();
            //}

            //var user = await _context.Users
            //    .SingleOrDefaultAsync(m => m.ID == id);
            //            var user = _unitOfWork.UserRepository.GetById(id);
            //           if (user == null)
            //         {
            //           return NotFound();
            //     }

            //   return View(user);
            return View(_unitOfWork.UserRepository.GetById(id));
        }
        
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

    }
}
