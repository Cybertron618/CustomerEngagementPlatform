using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerEngagementPlatform.Api.src.Models;
using CustomerEngagementPlatform.Api.src.Services;

namespace CustomerEngagementPlatform.Api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController(IMessageService messageService) : ControllerBase
    {
        private readonly IMessageService _messageService = messageService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult> AddMessage(Message message)
        {
            await _messageService.AddMessageAsync(message);
            return CreatedAtAction(nameof(GetMessageById), new { id = message.MessageId }, message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(int id, Message message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            var existingMessage = await _messageService.GetMessageByIdAsync(id);
            if (existingMessage == null)
            {
                return NotFound();
            }

            await _messageService.UpdateMessageAsync(message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var existingMessage = await _messageService.GetMessageByIdAsync(id);
            if (existingMessage == null)
            {
                return NotFound();
            }

            await _messageService.DeleteMessageAsync(id);
            return NoContent();
        }
    }
}
