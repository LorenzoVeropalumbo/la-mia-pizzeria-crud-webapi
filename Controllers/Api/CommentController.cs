﻿using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private PizzaDbContext db;

        public CommentController(PizzaDbContext _db)
        {
            db = _db;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Comment comment)
        {

            try
            {
                db.Comments.Add(comment);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }

            return Ok(comment);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Comment comment = db.Comments.Where(p => p.Id == id).FirstOrDefault();

            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();
            return Ok();
        }

    }
}
