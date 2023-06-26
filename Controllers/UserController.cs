using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagementASPDotNet.Data;
using StudentManagementASPDotNet.Models;

namespace StudentManagementASPDotNet.Controllers;

public class UserController : Controller
{
    private readonly UserDataDBContext _dbContext;

    public UserController(UserDataDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IActionResult UserDashboard()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Logout()
    {
        return Redirect("/Home/Index");
    }

    public async Task<IActionResult> UserList()
    {
        List<UserDetails> userDetails = await _dbContext
            .userdetails
            .AsNoTracking()
            .Where(x => x.isdelete == false)
            .ToListAsync();
        return View(userDetails);
    }

    public async Task<IActionResult> AddUser(UserDetails userDetails)
    {
        try
        {
            var userCount = await _dbContext.userdetails.CountAsync();
            userDetails.id = "USR00" + (userCount + 1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return View(userDetails);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserByAdmin(UserDetails userDetails)
    {
        try
        {
            await _dbContext.userdetails.AddAsync(userDetails);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Redirect("UserList");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        ResponseModel model = new ResponseModel();
        try
        {
           var user = await _dbContext.userdetails.FirstOrDefaultAsync(x => x.id == userId);
           if (user is null)
           {
               model.RespCode = "999";
               model.RespDesp = "User doesn't exist";
               model.RespType = EnumRespType.Error;
               goto result;
           }

           user.isdelete = true;
           var result = await _dbContext.SaveChangesAsync();
           bool isSuccess = result > 0;
           model.RespCode = isSuccess ? "000" : "999";
           model.RespDesp = isSuccess ? "Deleting Successful." : "Deleting Failed";
           model.RespType = isSuccess ? EnumRespType.Success : EnumRespType.Error;
        }
        catch (Exception ex)
        {
            model.RespCode = "999";
            model.RespDesp = ex.ToString();
            model.RespType = EnumRespType.Error;
        }

        result:
        return Json(model);
    }

    public async Task<IActionResult> UserTable()
    {
        List<UserDetails> userDetails = await _dbContext
            .userdetails
            .AsNoTracking()
            .Where(x => x.isdelete == false)
            .ToListAsync();
        return View(userDetails);
    }

    [ActionName("Edit")]
    public async Task<IActionResult> EditUser(string userId)
    {
        var user = await _dbContext.userdetails.FirstOrDefaultAsync(x=> x.id == userId);
        if (user is null)
        {
            return Redirect("/User/UserList");
        }

        return View("EditUser", user);
    }
    
    [HttpPost]
    [ActionName("Update")]
    public async Task<IActionResult> UpdateUser(string userId, UserDetails reqModel)
    {
        ResponseModel model = new ResponseModel();
        try
        {
            var user = await _dbContext.userdetails.FirstOrDefaultAsync(x => x.id == userId);
            if (user is null)
            {
                model.RespCode = "999";
                model.RespDesp = "User doesn't exist";
                model.RespType = EnumRespType.Error;
                goto result;
            }
            user.email = reqModel.email;
            user.username = reqModel.username;
            user.role = reqModel.role;
            
            var result = await _dbContext.SaveChangesAsync();
            
            bool isSuccess = result > 0;
            model.RespCode = isSuccess ? "000" : "999";
            model.RespDesp = isSuccess ? "Updating Successful." : "Updating Failed";
            model.RespType = isSuccess ? EnumRespType.Success : EnumRespType.Error;
        }
        catch (Exception ex)
        {
            model.RespCode = "999";
            model.RespDesp = ex.ToString();
            model.RespType = EnumRespType.Error;
        }

        result:
        return Json(model);
    }
}

public class ResponseModel
{
    public string RespCode { get; set; }
    public string RespDesp { get; set; }
    public EnumRespType RespType { get; set; }
}

public enum EnumRespType
{
    Success,
    Error
}