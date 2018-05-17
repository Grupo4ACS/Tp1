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
    public class PostsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public PostsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Posts
        //public async Task<IActionResult> Index()
        public ViewResult Index()
        {
            return View(_unitOfWork.PostRepository.Get());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts
            //    .SingleOrDefaultAsync(m => m.ID == id);
            //if (post == null)
            //{
            //    return NotFound();
            //}

            return View(_unitOfWork.PostRepository.Get(p=>p.ID.Equals(id),null,"User").FirstOrDefault());
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            LoadUsers();
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User,Content,UserID")] Post post)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(post);
                //await _context.SaveChangesAsync();
                _unitOfWork.PostRepository.Insert(post);
                _unitOfWork.save();
                return RedirectToAction(nameof(Index));
            }
            LoadUsers();
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts.SingleOrDefaultAsync(m => m.ID == id);
            var post = _unitOfWork.PostRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            LoadUsers();
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Content")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(post);
                    //await _context.SaveChangesAsync();
                    _unitOfWork.PostRepository.Update(post);
                    _unitOfWork.save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts
            //    .SingleOrDefaultAsync(m => m.ID == id);
            var post = _unitOfWork.PostRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var post = await _context.Posts.SingleOrDefaultAsync(m => m.ID == id);
            //_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();
            _unitOfWork.PostRepository.Delete(id);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            //    return _context.Posts.Any(e => e.ID == id);

            if (_unitOfWork.PostRepository.GetById(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        private void LoadUsers()
        {
            ViewBag.User = new SelectList(_unitOfWork.UserRepository.Get(),"UserID", "LastName");
        }
    }
}
