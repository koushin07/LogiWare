using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Logiware.Application.DTOs;
using Logiware.Domain.Contracts;

namespace Logiware.Application.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IUserRepository _userRepository;
     
        public RegisterValidator(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;

          RuleFor(reg => reg.Email)
    .NotEmpty()
    .WithMessage("Email is required.")
    .EmailAddress()
    .WithMessage("Email must be a valid email address.")
    .Must(email =>  !_userRepository.IsUserExistByEmail(email))
    .WithMessage("This email is already taken.");



            RuleFor(reg => reg.Password)
            .NotEmpty()
            .Length(4, 10)
            .WithMessage("PASSWORD is required and must be between 4 and 10 characters");

            RuleFor(reg => reg.username)
                .NotEmpty()
                .WithMessage("User name is required");
        }
    }
}