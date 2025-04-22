// code: fatih.unal date: 2025-04-22
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FthAdmin.Application.Features.Users.Commands;
using FthAdmin.Application.Features.Users.Queries;
using System.Collections.Generic;
using FthAdmin.Application.Abstractions;

namespace FthAdmin.Application.Features.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUserAsync(request.UserName, request.Email, request.Password);
        }
    }

    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
    {
        private readonly IUserService _userService;
        public AssignRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AssignRoleAsync(request.UserId, request.RoleName);
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService _userService;
        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DeleteUserAsync(request.UserId);
        }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserService _userService;
        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersAsync();
        }
    }
}
