using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly string _connectionString;
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _dbContext.Projects.ToListAsync();
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task StartAsync(Project project)
    {
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            sqlConnection.Open();

            var script = "UPDATE Project SET Status = @status, StartedAt = @startedat WHERE Id = @id";

            await sqlConnection.ExecuteAsync(script,
                new { status = project.Status, startedat = project.StartedAt, project.Id });
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}