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
        public async Task<IActionResult> Index()
        {
            return View(_unitOfWork.PostRepository.Get());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(string id)
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

            return View(_unitOfWork.PostRepository.GetById(id));
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(post);
                //await _context.SaveChangesAsync();
                _unitOfWork.PostRepository.Insert(post);
                _unitOfWork.save();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(string id)
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
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Content")] Post post)
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
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var post = await _context.Posts.SingleOrDefaultAsync(m => m.ID == id);
            //_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();
            _unitOfWork.PostRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(string id)
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
    }
}
