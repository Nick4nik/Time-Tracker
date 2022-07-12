using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Time_Tracker.Models;

namespace Time_Tracker.Validators
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public int RequiredLength { get; set; }

        public PasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (string.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Минимальная длина пароля равна {RequiredLength}"
                });
            }

            string pattern = "^[A-Za-z0-9]*$";

            if (!Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен состоять из цифр и символов `A-Z`"
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                    IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
