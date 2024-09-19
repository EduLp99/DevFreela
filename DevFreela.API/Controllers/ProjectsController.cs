using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService, IMediator mediator)
    {
        _projectService = projectService;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> Get(string? query)
    {
        //var projects = _projectService.GetAll(query);
        var getAllProjectsQuery = new GetAllProjectsQuery(query);

        var projects = await _mediator.Send(getAllProjectsQuery);


        return Ok(projects);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> GetById(int id)
    {
        /*
            var project = _projectService.GetById(id);

            if (project == null) return NotFound();
            return Ok();*/

        var getProjectByIdQuery = new GetProjectByIdQuery(id);

        var project = await _mediator.Send(getProjectByIdQuery);


        return Ok(project);
    }

    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
        //var id = _projectService.Create(inputModel);
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
    {
        if (command.Description.Length > 200) return BadRequest();

        //_projectService.Update(inputModel);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProjectCommand(id);

        //_projectService.Delete(id);

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
    {
        //_projectService.CreateComment(inputModel);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/start")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Start(int id)
    {
        //_projectService.Start(id);
        //return NoContent();
        var project = new StartProjectCommand(id);

        await _mediator.Send(project);
        return NoContent();
    }

    [HttpPut("{id}/finish")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
    {
        command.Id = id;


        var result = await _mediator.Send(command);

        if(!result)
        {
            return BadRequest("O pagamento não pôde ser processado.");
        }

        return NoContent();
    }
}