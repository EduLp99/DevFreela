using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Utilizar o mesmo algoritmo para criar o hash dessa senha
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            // Buscar no banco de dados um User que tenha meu email e senha em formato hash
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

            // Se nao existir, erro no login
            if (user == null)
            {
                return null;
            }

            // Se existir, gero o token usando os dados do usuario
            var token = _authService.GenerateJwtToken(user.Email, user.Role);
            return new LoginUserViewModel(user.Email, token);
        }
    }
}
