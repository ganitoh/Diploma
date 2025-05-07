using AutoMapper;
using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Identity.ApplicatinContract.Requests;
using Identity.Application.Common.Auth;
using Identity.Application.Common.Persistance;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Domain.Models;

namespace Identity.Application.CQRS.Users.Commands;

/// <summary>
/// Создание пользователя
/// </summary>
public record class CreateUserCommand(CreateUserRequest RequestData) : ICommand<Guid>;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsUserExistsByEmail(request.RequestData.Email, cancellationToken))
            throw new ApplicationException("Пользователь с такой почтой уже существует");
        
        var passwordHash = _passwordHasher.Generate(request.RequestData.Password);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = passwordHash;
        
        _userRepository.Create(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return user.Id;
    }
}