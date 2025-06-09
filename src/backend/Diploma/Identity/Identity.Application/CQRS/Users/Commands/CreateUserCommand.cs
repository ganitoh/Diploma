using AutoMapper;
using Common.Application;
using Identity.ApplicatinContract.Requests;
using Identity.Domain.Models;
using Identity.Infrastructure.Auth.Abstractions;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Role = Identity.Infrastructure.Auth.Jwt.Role;

namespace Identity.Application.CQRS.Users.Commands;

/// <summary>
/// Создание пользователя
/// </summary>
public record class CreateUserCommand(CreateUserRequest RequestData) : ICommand<Guid>;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IdentityDbContext context,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await IsUserEmailExist(request.RequestData.Email))
            throw new ApplicationException("Пользователь с такой почтой уже существует");
        
        var passwordHash = _passwordHasher.Generate(request.RequestData.Password);

        var user = _mapper.Map<User>(request.RequestData);
        user.PasswordHash = passwordHash;
        user.RoleId = (int)Role.User;

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    private async Task<bool> IsUserEmailExist(string email) =>
       await  _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email) is not null;
}