using DevFreela.Core.DTOs;
using DevFreela.Core.Payments;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly IProjectRepository _projectRepository;
    private readonly IPaymentService _paymentService;

    public FinishProjectCommandHandler(
        DevFreelaDbContext dbContext,
        IProjectRepository projectRepository,
        IPaymentService paymentService)
    {
        _dbContext = dbContext;
        _projectRepository = projectRepository;
        _paymentService = paymentService;
    }

    public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, 10000);

        _paymentService.ProcessPayment(paymentInfoDto);

        project.SetPaymentPending();


        await _projectRepository.SaveChangesAsync();

        return true;
    }
}