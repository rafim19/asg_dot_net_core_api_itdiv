using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Jenshin.Impack.API.Output;
using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using Jenshin.Impack.API.Model.Request;

namespace Jenshin.Impack.API.Services
{
    [ApiController]
    [Route("purchase")]
    public class PurchaseService : BaseService
    {
        public PurchaseService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemOutput), StatusCodes.Status200OK)]
        public IActionResult PurchaseItem([FromBody] PurchaseRequestDTO data, [FromHeader] string authorization)
        {
            try
            {
                var NIM = "2440112891";
                // Header Error Handler
                if (authorization != $"Basic {NIM}" || authorization == null)
                    throw new Exception("Invalid credentials");

                var objJSON = new TopUpOutput();
                objJSON.Message = Helper.PurchaseHelper.PurchaseItem(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
    }
}