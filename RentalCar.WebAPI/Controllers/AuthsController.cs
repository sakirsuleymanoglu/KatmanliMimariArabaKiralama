﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Business.Abstract;
using RentalCar.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var user = userToLogin.Data;

            var result = _authService.CreateAccessToken(user);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.CheckIfUserAlreadyExists(userForRegisterDto.Email);

            if (userExists.Success)
            {
                return BadRequest();
            }

            var register = _authService.Register(userForRegisterDto);

            if (!register.Success)
            {
                return BadRequest();
            }

            var registeredUser = register.Data;

            var result = _authService.CreateAccessToken(registeredUser);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}