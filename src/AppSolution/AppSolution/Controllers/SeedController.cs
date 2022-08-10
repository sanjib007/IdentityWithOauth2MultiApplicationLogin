using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation.AspNetCore;

namespace AppSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly RoleManager<UserRoles> _roleManager;
        public SeedController(RoleManager<UserRoles> roleManager)
        {
            _roleManager = roleManager;
        }
        
        [HttpGet("RoleInsert/{name}")]
        public async Task<IActionResult> RoleInsert(string name)
        {
            var isHave = await _roleManager.RoleExistsAsync(name);
            if (isHave)
            {
                throw new ApplicationException("This role is already exist.");
            }

            var newRole = new UserRoles()
            {
                Name = name
            };
            await _roleManager.CreateAsync(newRole);
            return Ok("Role Insert Successful.");
        }
        
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "admin, customer, business")]
        [HttpGet("ClientInsert")]
        public async Task<IActionResult> ClientInsert()
        {
            return Ok("this is access");
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpGet("ClientAdmin")]
        public async Task<IActionResult> ClientAdmin()
        {
            return Ok("this is admin access");
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "customer")]
        [HttpGet("ClientCustomer")]
        public async Task<IActionResult> ClientCustomer()
        {
            return Ok("this is Customer access");
        }
    }
}