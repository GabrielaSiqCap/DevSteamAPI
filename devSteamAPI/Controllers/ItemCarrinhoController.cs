﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using devSteamAPI.Data;
using devSteamAPI.Migrations;

namespace devSteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCarrinhoController : ControllerBase
    {
        private readonly DevSteamAPIContext _context;

        public ItemCarrinhoController(DevSteamAPIContext context)
        {
            _context = context;
        }

        // GET: api/ItemCarrinho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCarrinho>>> GetItemCarrinho()
        {
            return await _context.ItemCarrinho.ToListAsync();
        }

        // GET: api/ItemCarrinho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCarrinho>> GetItemCarrinho(Guid id)
        {
            var itemCarrinho = await _context.ItemCarrinho.FindAsync(id);

            if (itemCarrinho == null)
            {
                return NotFound();
            }

            return itemCarrinho;
        }

        // PUT: api/ItemCarrinho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCarrinho(Guid id, ItemCarrinho itemCarrinho)
        {
            if (id != itemCarrinho.ItemCarrinhoId)
            {
                return BadRequest();
            }

            _context.Entry(itemCarrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCarrinhoExists(id))
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

        // POST: api/ItemCarrinho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCarrinho>> PostItemCarrinho(ItemCarrinho itemCarrinho)
        {
            _context.ItemCarrinho.Add(itemCarrinho);

            // Atualizar o valor total do carrinho
            var carrinho = await _context.Carrinho.FindAsync(itemCarrinho.CarrinhoId);
            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            carrinho.Total = (carrinho.Total ?? 0) + (itemCarrinho.Quantidade * itemCarrinho.Valor);
            _context.Entry(carrinho).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCarrinho", new { id = itemCarrinho.ItemCarrinhoId }, itemCarrinho);
        }

        // DELETE: api/ItemCarrinho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCarrinho(Guid id)
        {
            var itemCarrinho = await _context.ItemCarrinho.FindAsync(id);
            if (itemCarrinho == null)
            {
                return NotFound();
            }

            // Atualizar o valor total do carrinho
            var carrinho = await _context.Carrinho.FindAsync(itemCarrinho.CarrinhoId);
            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            carrinho.Total = (carrinho.Total ?? 0) - (itemCarrinho.Quantidade * itemCarrinho.Valor);
            _context.Entry(carrinho).State = EntityState.Modified;

            _context.ItemCarrinho.Remove(itemCarrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCarrinhoExists(Guid id)
        {
            return _context.ItemCarrinho.Any(e => e.ItemCarrinhoId == id);
        }
    }
}
