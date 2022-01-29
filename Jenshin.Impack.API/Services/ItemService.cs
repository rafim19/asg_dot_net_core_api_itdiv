using System;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Jenshin.Impack.API.Model;
using Jenshin.Impack.API.Output;

using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using Jenshin.Impack.API.Model.Request;

namespace Jenshin.Impack.API.Services
{
    [ApiController]
    [Route("item")]
    public class ItemService : BaseService
    {
        public ItemService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemOutput), StatusCodes.Status200OK)]
        public IActionResult AddNewItem([FromBody] MsShopItem data)
        {
            try
            {
                var objJSON = new ItemOutput();
                objJSON.Success = Helper.ItemHelper.AddNewItem(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemOutput), StatusCodes.Status200OK)]
        public IActionResult UpdateItem([FromBody] MsShopItem data, [FromHeader] string NIM)
        {
            try
            {
                // ERROR HANDLERS
                if (NIM == null) throw new Exception("Please input your NIM");
                if (data.ItemID == new Guid()) throw new Exception("404-Item ID must be provided!");

                var objJSON = new ItemOutput();
                objJSON.Success = Helper.ItemHelper.UpdateItem(data, NIM);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                var exc = new OutputBase(ex);
                if (ex.Message.Contains("404"))
                {
                    exc.ResultCode = int.Parse(ex.Message[..3]);
                    exc.ErrorMessage = ex.Message.Remove(0, 4);
                }
                return StatusCode(exc.ResultCode, exc);
            }
        }
    }
}