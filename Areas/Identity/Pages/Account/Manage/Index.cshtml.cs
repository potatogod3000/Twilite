// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Twilite.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using Twilite.Models;

namespace Twilite.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Upload User Avatar")]
            public byte[] UserAvatar { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var ProfilePictureBytes = _db.UserProfiles.FirstOrDefault(x => x.UserName == userName).ProfilePictureBytes;

            Username = userName;

            Input = new InputModel
            {
                UserAvatar = ProfilePictureBytes,
                PhoneNumber = phoneNumber
            };
        }

        private void SetProfilePicture(string profilePictureBase64) {
            UserProfileModel userProfile = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
            try {
                // Remove unwanted padding text in-front of the base64 string
                Regex regex = new(@"^[\w/\:.-]+;base64,");
                profilePictureBase64 = regex.Replace(profilePictureBase64, string.Empty);

                byte[] bytes = Convert.FromBase64String(profilePictureBase64);

                userProfile.ProfilePictureBytes = bytes;
                
                if(ModelState.IsValid) {
                    _db.Update(userProfile);
                    _db.SaveChanges();
                }
            }
            catch(Exception exception) {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string profilePictureBase64)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(Input.PhoneNumber == null && profilePictureBase64 == null) {
                ModelState.AddModelError(string.Empty, "You have not modified anything.");
            }

            if(profilePictureBase64 != null) {
                SetProfilePicture(profilePictureBase64);
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
