﻿using BookingApp.Business.Operations.User;
using BookingApp.Business.Operations.User.Dtos;
using BookingApp.WebApi.Jwt;
using BookingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } // TODO : İlerde Action filter olarak kodlanacak

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };

            var result = await _userService.AddUser(addUserDto);

            if(result.IsSucceed)
                return Ok();

            else return BadRequest(result.Message);

        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var loginDto = new LoginUserDto
            {
                EMail = request.Email,
                Password = request.Password
            };

            var result = _userService.LoginUser(loginDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            
            // jwt 

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName= user.FirstName,
                LastName= user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message ="Giriş Başarılı ile tamalandı",
                Token = token,
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult>  GetMyUser()
        {
            return Ok();
        }

    }
}
