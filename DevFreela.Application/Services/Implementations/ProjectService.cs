using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly string _connectionString;
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }

    // public List<ProjectViewModel> GetAll(string query)
    // {
    //     var projects = _dbContext.Projects;
    //
    //     var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
    //         .ToList();
    //
    //     return projectsViewModel;
    // }

    /*public ProjectDetailsViewModel GetById(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

        if (project == null)
            return null;

        var projectDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt
        );

        return projectDetailsViewModel;
    }*/

    /*public void Start(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

        //project.Start();

        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            sqlConnection.Open();

            var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

            sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, id });
        }
    }*/

    /*public void Finish(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);


        project.Finish();
    }*/
}