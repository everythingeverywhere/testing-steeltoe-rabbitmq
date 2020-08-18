using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Core;

namespace WriteTo_RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "value";
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }

        public class WriteMessageQueueController : ControllerBase
	{
			public const string RECEIVE_AND_CONVERT_QUEUE = "steeltoe_message_queue";
			private readonly ILogger<WriteMessageQueueController> _logger;
			private readonly RabbitTemplate _rabbitTemplate;
			private readonly RabbitAdmin _rabbitAdmin;

			public WriteMessageQueueController(ILogger<WriteMessageQueueController> logger, RabbitTemplate rabbitTemplate, RabbitAdmin rabbitAdmin)
			{
					_logger = logger;
					_rabbitTemplate = rabbitTemplate;
					_rabbitAdmin = rabbitAdmin;
			}

			[HttpGet()]
			public ActionResult<string> Index()
			{
					var msg = "Hi there from over here.";

					_rabbitTemplate.ConvertAndSend(RECEIVE_AND_CONVERT_QUEUE, msg);

					_logger.LogInformation($"Sending message '{msg}' to queue '{RECEIVE_AND_CONVERT_QUEUE}'");
            
					return "Message sent to queue.";
			}
	}

}
