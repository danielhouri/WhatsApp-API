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

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitaionService _service;

        public InvitationsController(IInvitaionService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([Bind("from,to,server")] Invitation inv)
        {
            if(_service.AddContact(inv.From, inv.To, inv.Server))
            {
                return Created(string.Format("/api/Contacts/{0}", inv.From), inv.From);
            }
            return BadRequest();
        }
    }
}