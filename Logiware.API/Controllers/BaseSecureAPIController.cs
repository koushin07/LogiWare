using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseSecureAPIController : ControllerBase
{
    
}