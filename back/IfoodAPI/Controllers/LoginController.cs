using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IfoodAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IfoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ValidacaoServices _validacaoExterna;
        public LoginController(IConfiguration config, ValidacaoServices validacaoExterna)
        {
            _validacaoExterna = validacaoExterna;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetToken([FromHeader] string Authorization = null)
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken(Authorization);

            Response.Headers.Add("Authorization", token);

            return Ok();
            

        }
    }
}

