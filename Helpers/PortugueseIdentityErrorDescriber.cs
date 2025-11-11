using Microsoft.AspNetCore.Identity;

public class PortugueseIdentityErrorDescriber : IdentityErrorDescriber
{
    // ================================================================
    // SENHAS
    // ================================================================

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError { Code = nameof(PasswordTooShort), Description = $"A senha deve ter pelo menos {length} caracteres." };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve ter pelo menos um caractere não alfanumérico (ex: !, @, #)." };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "A senha deve ter pelo menos um dígito ('0'-'9')." };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "A senha deve ter pelo menos uma letra minúscula ('a'-'z')." };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "A senha deve ter pelo menos uma letra maiúscula ('A'-'Z')." };
    }


    // ================================================================
    // LOGIN / LOGOUT
    // ================================================================

    public override IdentityError PasswordMismatch()
    {
        // Mensagem genérica para falha de login
        return new IdentityError { Code = nameof(PasswordMismatch), Description = "Email ou senha inválidos." };
    }

    public override IdentityError InvalidUserName(string userName)
    {
        // Mensagem genérica para falha de login
        return new IdentityError { Code = nameof(InvalidUserName), Description = "Email ou senha inválidos." };
    }

    // ================================================================
    // USUÁRIO / EMAIL
    // ================================================================

    public override IdentityError DuplicateUserName(string userName)
    {
        // Como estamos usando email como username
        return new IdentityError { Code = nameof(DuplicateUserName), Description = $"O email '{userName}' já está em uso." };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = nameof(DuplicateEmail), Description = $"O email '{email}' já está em uso." };
    }

    public override IdentityError InvalidEmail(string email)
    {
        return new IdentityError { Code = nameof(InvalidEmail), Description = $"O email '{email}' é inválido." };
    }
}