using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleApplication.BAL.Services;
using SampleApplication.Models;
using SampleApplication.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleUserController : ControllerBase
    {
        private readonly ISampleUserService _sampleUserService;
        private readonly IConfiguration _configuration;

        public SampleUserController(ISampleUserService sampleUserService, IConfiguration configuration)
        {
            _sampleUserService = sampleUserService;
            _configuration = configuration;
        }




        [HttpPost]
        [Route("CreateSampleUser")]
        public ApiResponse CreateSample(SampleUser user)
        {

            return _sampleUserService.CreateUsers(user); 
        }


        [HttpGet]
        [Route("GetAllSampleUsers")]
        public ApiResponse GetAllSampleUsers(string? SearchText)
        {
            return _sampleUserService.GetAllSampleUsers(SearchText);
        }






        [HttpGet]
        [Route("GetSampleUserById")]
       // [Route("{id}")]
        public IActionResult GetSampleUserById(int id)
        {
            var result = _sampleUserService.GetUserById(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateSampleUser")]
        //[Route("{id}")]
        public IActionResult UpdateSampleUser(int id, [FromBody] SampleUser user)
        {
            
                var result = _sampleUserService.UpdateUsers(user);
                return Ok(result);
             
        }



        [HttpDelete]
        [Route("DeleteSampleUser")]
      //  [Route("{id}")]
        public IActionResult DeleteSampleUser(int id)
        {
            if (id <= 0)
            {
                return NotFound("Please enter a valid entity id!!");
            }

            var result = _sampleUserService.DeleteUsers(id);
            return Ok(result);
        }






        [HttpPost]
        [Route("UserLogin")]
        [Route("Login")]
        [AllowAnonymous]
        
        public IActionResult Login([FromBody]  LoginPost model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter UserName and Password");
            }
            // LoginResponseDTO response = new() { Username = model.Username };

            var response = _sampleUserService.LoginSampleUser(model);

            if (response.UserMasterId > 0 && model.EmailId != null)
            {
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretKey"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                                        {
                                            new Claim(ClaimTypes.Name, model.EmailId),
                                            //Role
                                             new Claim(ClaimTypes.Role, "Admin" )

                                        }),
                    Expires = DateTime.Now.AddHours(24),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Token = tokenHandler.WriteToken(token);
            }
            else
            {
                return Ok("Invalid UserName or Password");
            }
            return Ok(response);
        }




    }
}
