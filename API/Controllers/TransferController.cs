#nullable disable
using Microsoft.AspNetCore.Mvc;
using API.Services;
using FirebaseAdmin.Messaging;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferController(ITransferService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([Bind("from,to,content")] Transfer msg)
        {
            if(_service.AddMessage(msg.From, msg.To, msg.Content))
            {
                // Firebase
                string token = "";
                if(UsersController._firebase.TryGetValue(msg.To, out token))
                {
                    var message = new FirebaseAdmin.Messaging.Message()
                    {
                        Token = token,
                        Notification = new Notification()
                        {
                            Title = msg.From,
                            Body = msg.Content
                        }
                    };
                    string response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                    Console.WriteLine(response);
                }

                return Created(string.Format("/api/Contacts/{0}", msg.From), msg.From);

            }
            return BadRequest();
        }
    }
}
