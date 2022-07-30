using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4.Areas.Identity.Data;
using Task4.Domain.Interfaces;
using Task4.Models;

namespace Task4.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AdminController(
        IAdminService adminService,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _adminService = adminService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["UserEmail"] = await _userManager.GetEmailAsync(await _userManager.GetUserAsync(User));
        ViewBag.Users = await _adminService.GetUsers();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUserStatus(int[] userIds, bool isActive)
    {
        await _adminService.ChangeUserStatus(userIds, isActive);
        if (!isActive)
        {
            await SignOutIfCurrentUserChanged(userIds);
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUsers(int[] userIds)
    {
        await _adminService.DeleteUsers(userIds);
        await SignOutIfCurrentUserChanged(userIds);
        return Ok();
    }

    private async Task SignOutIfCurrentUserChanged(int[] userIds)
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = int.Parse(await _userManager.GetUserIdAsync(user));
        if (userIds.Contains(userId))
        {
            await _signInManager.SignOutAsync();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}