using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interfaces;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Administrator;

public class UserModel : PageModel
{
    private readonly IUserService _userService;

    public UserModel(IUserService userService)
    {
        _userService = userService;
    }

    public List<UserAdminDto> Users { get; private set; } = new();

    public IReadOnlyList<AccountRole> RoleOptions { get; private set; } = Array.Empty<AccountRole>();

    public IReadOnlyList<AccountMajor> MajorOptions { get; private set; } = Array.Empty<AccountMajor>();

    public Guid? EditingUserId { get; private set; }

    public string? LegacyMajorLabel { get; private set; }

    public string? LegacyRoleLabel { get; private set; }

    [BindProperty]
    public CreateUserRequest CreateInput { get; set; } = new();

    [BindProperty]
    public UpdateUserRequest UpdateInput { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid? editId)
    {
        await LoadPageDataAsync();

        if (editId.HasValue)
        {
            await PrepareUpdateAsync(editId.Value);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        await LoadPageDataAsync();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _userService.CreateAsync(CreateInput);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Không thể tạo người dùng.");
            return Page();
        }

        TempData["Message"] = "Tạo người dùng thành công.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        await LoadPageDataAsync();
        EditingUserId = UpdateInput.Id;
        SetLegacyLabels(UpdateInput.Id);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _userService.UpdateAsync(UpdateInput);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Không thể cập nhật người dùng.");
            return Page();
        }

        TempData["Message"] = "Cập nhật người dùng thành công.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeactivateAsync(Guid userId)
    {
        var result = await _userService.DeactivateAsync(userId);
        if (result.Success)
        {
            TempData["Message"] = "Đã vô hiệu hoá người dùng.";
        }
        else
        {
            TempData["Code"] = "ERROR";
            TempData["Message"] = result.Error ?? "Không thể vô hiệu hoá người dùng.";
        }

        return RedirectToPage();
    }

    private async Task LoadPageDataAsync()
    {
        Users = await _userService.GetAllAsync();
        RoleOptions = Enum.GetValues<AccountRole>();
        MajorOptions = Enum.GetValues<AccountMajor>();
    }

    private async Task PrepareUpdateAsync(Guid editId)
    {
        var user = await _userService.GetByIdAsync(editId);
        if (user == null)
        {
            TempData["Code"] = "NOT_FOUND";
            TempData["Message"] = "Không tìm thấy người dùng.";
            return;
        }

        EditingUserId = editId;
        LegacyMajorLabel = null;
        LegacyRoleLabel = null;

        var updateModel = new UpdateUserRequest
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            IsSuspended = user.IsSuspended,
            SuspendedUntil = user.SuspendedUntil
        };

        if (Enum.TryParse<AccountRole>(user.Role, out var parsedRole))
        {
            updateModel.Role = parsedRole;
        }
        else
        {
            updateModel.Role = AccountRole.Student;
            LegacyRoleLabel = user.Role;
        }

        if (Enum.TryParse<AccountMajor>(user.Major, out var parsedMajor))
        {
            updateModel.Major = parsedMajor;
        }
        else
        {
            updateModel.Major = AccountMajor.SoftwareEngineering;
            LegacyMajorLabel = user.Major;
        }

        UpdateInput = updateModel;
    }

    private void SetLegacyLabels(Guid userId)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            LegacyMajorLabel = null;
            LegacyRoleLabel = null;
            return;
        }

        LegacyRoleLabel = Enum.TryParse<AccountRole>(user.Role, out _) ? null : user.Role;
        LegacyMajorLabel = Enum.TryParse<AccountMajor>(user.Major, out _) ? null : user.Major;
    }
}
