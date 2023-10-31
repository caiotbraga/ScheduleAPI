﻿using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.Data;
using ScheduleAPI.Models;

namespace ScheduleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserContext _context;

    public UserController(UserContext userContext)
    {
        _context = userContext;
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, user);
    }

    [HttpGet]
    public IEnumerable<User> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 100)
    {
        return _context.Users.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult? GetUserById(int id)
    {
        var user = _context.Users.FirstOrDefault(user => user.Id == id);

        if (user == null) return NotFound("User doesn't exist!");
        return Ok(user);
    }

}
