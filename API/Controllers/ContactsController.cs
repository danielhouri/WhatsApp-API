#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API;
using API.Data;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Index()
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);
            return await _service.GetAll(username);
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);
            var contact = _service.GetSpecific(id, username);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,name,server")] Contact contact)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);

            if(await _service.Add(contact, username))
            {
                return Created(string.Format("/api/Contacts/{0}", contact.Id), contact.Id);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("name,server")] Contact contact)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);

            if (await _service.Edit(id, contact.Name, contact.Server, username))
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);

            _service.Delete(id, username);

            return NoContent();
        }

        [HttpGet("{id}/messages")]
        public IActionResult GetAllMessages(string id)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);
            if(username != null)
            {
                return Ok(_service.GetAllMessages(id, username));
            }
            return BadRequest();
        }

        [HttpGet("{id}/messages/{idMessage}")]
        public IActionResult GetSpecificMessage(string id, int idMessage)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);
            var message = _service.GetSpecificMessage(id, idMessage, username);
            if (message != null)
            {
                return Ok(message);
            }
            return NotFound();
        }

        [HttpDelete("{id}/messages/{idMessage}")]
        public IActionResult DeleteMessage(string id, int idMessage)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);
            _service.DeleteMessage(id, idMessage, username);
            return NoContent();
        }

        [HttpPost("{id}/messages/")]
        public async Task<IActionResult> AddMessage(string id, [Bind("Content")] Message msg)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);

            if (await _service.AddMessage(id, msg.Content, username))
            {
                return Created(string.Format("/api/Contacts/{0}", id), id);
            }
            return BadRequest();
        }

        [HttpPut("{id}/messages/{idmessage}")]
        public IActionResult EditMessage(string id, int idmessage, [Bind("Content")] Message msg)
        {
            var username = Tools.getUsername(Request.Headers["Authorization"]);

            _service.EditMessage(id, idmessage, msg.Content, username);
            return NoContent();
        }
    }
}
