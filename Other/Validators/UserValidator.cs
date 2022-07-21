using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_Tracker.Models;

namespace Time_Tracker.Validators
{
    public class UserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            List<string> spam = new List<string>();
            spam.Add("@mail.ru");
            spam.Add("@yandex.ru");


            foreach (var mail in spam)
            {
                if (user.Email.ToLower().EndsWith(mail))
                {
                    errors.Add(new IdentityError
                    {
                        Description = "Данный домен находится России. Выберите другой почтовый сервис"
                    });
                }
            }

            //if (user.UserName.Contains("admin"))
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Description = "Ваша почта не должена содержать `admin`"
            //    });
            //}

            if (!user.Email.Contains("@"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Ваша почта должна содержать `@`"
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
