using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Models.DTOs;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet()]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        [HttpGet("{Id}")]
        public ItemDto GetById(Guid Id)
        {
            var item = items.Where(item => item.Id == Id).SingleOrDefault();
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Create(ItemDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var item = new ItemDto(Guid.NewGuid(), model.Name, model.Description, model.Price, DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id }, item);
        }
    }
}