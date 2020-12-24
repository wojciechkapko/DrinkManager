using BLL;
using DrinkManagerWeb.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DrinkManagerWeb.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResource> _localizer;


        public EmailModel(
            UserManager<AppUser> userManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;

        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "TheEmailFieldIsRequired")]
            [EmailAddress(ErrorMessage = "TheEmailFieldIsNotAValidE-mailAddress")]
            [Display(Name = "NewEmail")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_localizer["UnableToLoadUserWithID"] + $" '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_localizer["UnableToLoadUserWithID"] + $" '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                user.Email = Input.NewEmail;
                user.UserName = Input.NewEmail;
                await _userManager.UpdateAsync(user);
                return RedirectToPage();
            }

            StatusMessage = _localizer["YourEmailIsUnchanged"];

            return RedirectToPage();
        }
    }
}
