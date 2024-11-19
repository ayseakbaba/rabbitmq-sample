using Microsoft.AspNetCore.Mvc;

namespace rabbitmq_sample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private readonly IMessageQueueService _messageQueueService;

        public RabbitMQController(IMessageQueueService messageQueueService)
        {
            _messageQueueService = messageQueueService;
        }


        [HttpGet("GetMessage")]
        public IActionResult GetMessage()
        {
            var message = _messageQueueService.GetMessage("mesajkuyrugu");
            if (string.IsNullOrEmpty(message))
            {
                return NotFound("Kuyrukta mesaj bulunamadı.");
            }

            return Ok(new { Message = message });
        }


        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromBody] string message)
        {
            _messageQueueService.PublishMessage("mesajkuyrugu", message);
            return Ok($"Mesaj kuyruğa gönderildi: {message}");
        }

    }
}
