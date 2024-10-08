using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cafe.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(
            UserManager<IdentityUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kullanıcı kimliğiyle yüklenemiyor '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"Kimliği olan kullanıcı için 2FA devre dışı bırakılamıyor '{_userManager.GetUserId(User)}' as it's not currently enabled.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kullanıcı kimliğiyle yüklenemiyor '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Kimliği olan kullanıcı için 2FA devre dışı bırakılırken beklenmeyen bir hata oluştu '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("'{UserId}' kimliğine sahip kullanıcı 2fa'yı devre dışı bıraktı.", _userManager.GetUserId(User));
            StatusMessage = "2fa devre dışı bırakıldı. Bir kimlik doğrulama uygulaması kurduğunuzda 2fa'yı yeniden etkinleştirebilirsiniz";
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}