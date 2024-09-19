using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;

    public StartProjectCommandHandler(DevFreelaDbContext dbContext)
    {
        _projectRepository = _projectRepository;
    }

    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        project.Start();

        await _projectRepository.StartAsync(project);

        return Unit.Value;
    }
}