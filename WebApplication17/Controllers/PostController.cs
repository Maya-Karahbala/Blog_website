using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.Models;

using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace WebApplication17.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogContext _context;

        public PostController(BlogContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index(int id)
        {
            var authunticatedUserId = HttpContext.Session.GetInt32("UserId");
            var blogContext = _context.Posts. Include(p => p.User).
                Include(p => p.PostCategories).ThenInclude(postCategory => postCategory.Category)
                .Where(p => p.UserId == authunticatedUserId).OrderBy(p => p.CreatingTime);
            
            // order by passed ıd i selcted from dropdown
            switch (id)
            {
                case 1:
                    blogContext = blogContext.OrderByDescending(p => p.CreatingTime);
                    break;
                case 2:
                    blogContext =blogContext.OrderBy(p => p.CreatingTime);
                    break;

                default:
                    blogContext = blogContext.OrderBy(p => p.CreatingTime);
                    break;
            }

            return View((List<Post>)blogContext.ToList());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
  
        // GET: Post/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            //ViewBag.categories =  _context.Categories.ToList();
            ViewBag.categories = _context.Categories.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Title }).ToList();
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,Title,Content,Tags,UserId")] Post post)
        public async Task<IActionResult> Create(PostViewModel postViewModel)
      
        {
            List<PostCategory> postCategories = new List<PostCategory>();
            if (postViewModel.categories != null)
            {
            foreach (int categoryId in postViewModel.categories)
            {
                postCategories.Add(new PostCategory(categoryId));
            }
            }
            
            Post post = new Post
            {
            
                Title = postViewModel.Title,
                Content= postViewModel.Content,
                Tags= postViewModel.Tags,
                UserId = (int)HttpContext.Session.GetInt32("UserId"),
                PostCategories= postCategories

            };
            if (ModelState.IsValid)
            {
                post.CreatingTime = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Tags,CreatingTime,UserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.UserId = (int)HttpContext.Session.GetInt32("UserId");
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
