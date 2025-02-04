using BackendDotNet.Data;
using BackendDotNet.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace BackendDotNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase {

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = context.Users.ToList();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<AppUser> GetUsers(int id)
    {
        var user = context.Users.Find(id);
        return Ok(user);
    }
}