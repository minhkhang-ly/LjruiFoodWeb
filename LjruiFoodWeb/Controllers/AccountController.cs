using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.ViewModels;

namespace LjruiFoodWeb.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<NguoiDung> _userManager;
    private readonly SignInManager<NguoiDung> _signInManager;

    public AccountController(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult DangNhap(string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginVM());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DangNhap(LoginVM model, string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View(model);

        var userName = model.UserNameOrEmail;

        // Check if input is email
        if (model.UserNameOrEmail.Contains('@'))
        {
            var userByEmail = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            if (userByEmail != null)
                userName = userByEmail.UserName ?? model.UserNameOrEmail;
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            model.MatKhau,
            model.GhiNho,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            TempData["DangNhapThanhCong"] = $"Chào mừng {user.HoTen}!";
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Tài khoản đã bị khóa tạm thời.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Mật khẩu không đúng.");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult DangKy(string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        ViewData["ReturnUrl"] = returnUrl;
        return View(new RegisterVM());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DangKy(RegisterVM model, string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View(model);

        var existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null)
        {
            ModelState.AddModelError("UserName", "Tên đăng nhập đã được sử dụng.");
            return View(model);
        }

        var existingEmail = await _userManager.FindByEmailAsync(model.Email);
        if (existingEmail != null)
        {
            ModelState.AddModelError("Email", "Email đã được sử dụng.");
            return View(model);
        }

        var user = new NguoiDung
        {
            HoTen = model.HoTen,
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.SoDienThoai,
            DiaChi = model.DiaChi,
            NgayTao = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, model.MatKhau);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        await _userManager.AddToRoleAsync(user, "KhachHang");

        await _signInManager.SignInAsync(user, isPersistent: false);

        TempData["DangKyThanhCong"] = $"Tài khoản {model.HoTen} đã được tạo thành công!";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DangXuat()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult TruyCapBiTuChoi()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult ExternalLogin(string provider, string? returnUrl = null)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
    {
        returnUrl ??= Url.Content("~/");
        if (remoteError != null)
        {
            ModelState.AddModelError(string.Empty, $"Lỗi từ nhà cung cấp ngoài: {remoteError}");
            return RedirectToAction("DangNhap", new { ReturnUrl = returnUrl });
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            ModelState.AddModelError(string.Empty, "Lỗi khi tải thông tin đăng nhập ngoài.");
            return RedirectToAction("DangNhap", new { ReturnUrl = returnUrl });
        }

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        if (signInResult.IsLockedOut)
        {
            return RedirectToAction("TruyCapBiTuChoi");
        }
        else
        {
            // Get email claim from Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "Người dùng Google";

            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new NguoiDung
                    {
                        UserName = email,
                        Email = email,
                        HoTen = name,
                        NgayTao = DateTime.Now
                    };
                    var createResult = await _userManager.CreateAsync(user);
                    if (createResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "KhachHang");
                    }
                }
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                if (addLoginResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Lỗi khi tạo/liên kết tài khoản Google.");
            return RedirectToAction("DangNhap", new { ReturnUrl = returnUrl });
        }
    }
}
