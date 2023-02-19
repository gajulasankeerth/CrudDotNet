using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Contacts : Controller
    {
        private readonly ContactsDbContext dbContext;
        public Contacts(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(AddContact addContact)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContact.Address,
                Email = addContact.Email,
                Name = addContact.Name,
                Phone = addContact.Phone,
            };

            await dbContext.contacts.AddAsync(contact);

            await dbContext.SaveChangesAsync();
            return Ok("Save Sucess");
        }
        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContact updateContact)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact!=null)
            {
                contact.Address = updateContact.Address;
                contact.Email = updateContact.Email;
                contact.Name = updateContact.Name;
                contact.Phone = updateContact.Phone;
                await dbContext.SaveChangesAsync();
                return Ok("Update Sucess");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContactByID([FromRoute] Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if(contact!=null)
            {
                return Ok(contact);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok("Delete Sucess");
            }
            else
            {
                return NotFound();
            }

        }
    }
}
