using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
{
    private readonly DevFreelaDbContext _dbContext;

    public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

        var projectDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt
        );

        return projectDetailsViewModel;
    }
}