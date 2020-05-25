using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalAssistance.Data;
using PersonalAssistance.Model;

namespace PersonalAssistance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ItemsDbContext _itemsDbContext ;//= new ItemsDbContext();

        public ItemController(ItemsDbContext itemsDbContext)
        {
            _itemsDbContext = itemsDbContext;

        }
       
        // GET: api/Item
        [HttpGet]
        [Route("[action]")]
        [ResponseCache(Duration = 30)]
        public IActionResult GetAll(string sort)
        {
            IQueryable<Item> items;
            switch (sort)
            {
                case "desc":
                    items = _itemsDbContext.Items.OrderByDescending(i => i.DueDate);
                    break;
                case "asc":
                    items = _itemsDbContext.Items.OrderBy(i => i.DueDate);
                    break;
                default:
                    items = _itemsDbContext.Items;
                    break;
                
            }
           // var items= _itemsDbContext.Items;
            return Ok(items);
        }

        // GET: api/Item/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet]
        //[Route("{id}")]
        public IActionResult Get(int id)
        {
            var item = _itemsDbContext.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }

        // POST: api/Item
        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            _itemsDbContext.Items.Add(item);
            _itemsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
             var itemToBeUpdated =_itemsDbContext.Items.Find(id);
             if (itemToBeUpdated == null)
             {
                 return NotFound("No record found at this id.");
             }
             else
             {
                itemToBeUpdated.Amount = item.Amount;
                itemToBeUpdated.Description = item.Description;
                itemToBeUpdated.DueDate = item.DueDate;
                itemToBeUpdated.Status = item.Status;
                _itemsDbContext.SaveChanges();
                return Ok("Record updated at this id.");
             }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var itemToDelete = _itemsDbContext.Items.Find(id);
            if (itemToDelete == null)
            {
                return NotFound("Item at the requested id not found.");
            }
            else
            {
                _itemsDbContext.Items.Remove(itemToDelete);
                _itemsDbContext.SaveChanges();
                return Ok("item successfully deleted at this id.");
            }

        }
    }
}
