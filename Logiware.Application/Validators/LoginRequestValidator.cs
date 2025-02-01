using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Logiware.Domain.Contracts;
using LogiWare.Contracts.DTOs;

namespace Logiware.Application.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        private readonly IUserRepository _userRepository;
        public LoginRequestValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Email).Must(email => _userRepository.IsUserExistByEmail(email)).WithMessage("Email is not registered");

            RuleFor(x => x.Password).NotEmpty().Length(4, 30);

        }

    }
}
