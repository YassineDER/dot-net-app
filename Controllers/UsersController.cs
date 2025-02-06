using BackendDotNet.Data;
using BackendDotNet.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Controllers;

public class UsersController(DataContext context) : BaseApiController {

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound("User does not exist with this id");
        
        return Ok(user);
    }
}