using FluentValidation;

namespace CarWash.BL.DTOs
{
    public class RegisterDto
    {
        public required string Email { get; set; } // Required email for registration
        public required string Password { get; set; } // Required password for registration
        public string? FullName { get; set; } // Optional full name
        public string? ContactNumber { get; set; } // Optional contact number
    }

    public class LoginDto
    {
        public required string Email { get; set; } // Required email for login
        public required string Password { get; set; } // Required password for login
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.FullName).MaximumLength(100).WithMessage("FullName cannot exceed 100 characters.");
            RuleFor(x => x.ContactNumber).Matches("^[0-9]+$").WithMessage("ContactNumber must contain only digits.").When(x => !string.IsNullOrEmpty(x.ContactNumber));
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}