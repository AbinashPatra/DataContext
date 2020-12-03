using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataContext.Web.Data;
using DataContext.Web.Models;

namespace DataContext.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly DataContextWebContext _context;

        public MessageController(DataContextWebContext context)
        {
            _context = context;
        }

        // GET: MessageModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.MessageModel.ToListAsync());
        }

        // GET: MessageModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.MessageModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return View(messageModel);
        }

        // GET: MessageModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,EndDate")] MessageModel messageModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageModel);
        }

        // GET: MessageModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.MessageModel.FindAsync(id);
            if (messageModel == null)
            {
                return NotFound();
            }
            return View(messageModel);
        }

        // POST: MessageModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,EndDate")] MessageModel messageModel)
        {
            if (id != messageModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageModelExists(messageModel.Id))
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
            return View(messageModel);
        }

        // GET: MessageModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.MessageModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return View(messageModel);
        }

        // POST: MessageModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageModel = await _context.MessageModel.FindAsync(id);
            _context.MessageModel.Remove(messageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageModelExists(int id)
        {
            return _context.MessageModel.Any(e => e.Id == id);
        }
    }
}
