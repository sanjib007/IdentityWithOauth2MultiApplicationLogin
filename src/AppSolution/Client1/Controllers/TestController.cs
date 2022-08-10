using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Client1.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestController()
    {
        
    }   
    
    [HttpGet("message")]
    public async Task<IActionResult> GetMessage()
    {
        return Content($"successfully no authenticated.");
    }
    
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("getAccess")]
    public async Task<IActionResult> getAccess()
    {
        return Content($"successfully authenticated.");
    }
    
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "admin")]
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmin()
    {
        return Content($"Only admin access role this.");
    }
    
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "customer")]
    [HttpGet("customer")]
    public async Task<IActionResult> GetCustomer()
    {
        return Content($"Only customer access role this.");
    }
    
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "business")]
    [HttpGet("business")]
    public async Task<IActionResult> GetBusiness()
    {
        return Content($" Only business role access this.");
    }
    
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "admin, customer, business")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Content($"All role access this.");
    }
}