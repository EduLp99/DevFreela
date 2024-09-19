using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillDTO>>
{
    private readonly ISkillRepository _skillRepository;

    public GetAllSkillQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<SkillDTO>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        return await _skillRepository.GetAll();
    }
}