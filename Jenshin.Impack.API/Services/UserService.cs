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
using Jenshin.Impack.API.Helper;
using Binus.WS.Pattern.Entities;
using System.Linq;
using System.Net.Http;

namespace Jenshin.Impack.API.Services
{
    [ApiController]
    [Route("user")]
    public class UserService : BaseService
    {
        public UserService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserOutput), StatusCodes.Status200OK)]
        public IActionResult GetAllUser()
        {
            try
            {
                var objJSON = new UserOutput();
                objJSON.Data = UserHelper.GetAllUser();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        [HttpGet]
        [Route("specific")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SpecificUserOutput), StatusCodes.Status200OK)]
        public IActionResult GetSpecificUser([FromQuery] MsUser data)
        {
            try
            {
                // ERROR HANDLERS
                if (data.UserEmail == null && data.UserName == null) 
                    throw new Exception("400-Parameter must be filled!");
                else if (data.UserEmail != null && data.UserName != null) 
                    throw new Exception("500-Parameter can only have one attribute");

                var objJSON = new SpecificUserOutput();
                objJSON.Data = UserHelper.GetSpecificUser(data.UserEmail, data.UserName);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                var exc = new OutputBase(ex);
                // Jika Exception berasal dari Error Handler
                if (ex.Message.Contains("Account") || ex.Message.Contains("Parameter"))
                {
                    // Ambil Status Code
                    exc.ResultCode = int.Parse(ex.Message[..3]); 
                    // Ambil Error Message
                    exc.ErrorMessage = ex.Message.Remove(0, 4);
                }
                return StatusCode(exc.ResultCode, exc);
            }
        }
    }
}