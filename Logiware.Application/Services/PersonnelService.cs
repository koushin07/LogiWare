using System.Collections;
using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using Logiware.Application.Interfaces;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Logiware.Application.Services;

public class PersonnelService : IPersonnelService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    public PersonnelService(IUnitOfWork uow, IDefaultMapper mapper, ILogger<PersonnelService> logger)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<List<PersonnelDto>> GetAll()
    {
        var personnel = await _uow.PersonnelRepository.GetAll();
        return _mapper.Map<List<PersonnelDto>>(personnel);
    }

    public async Task<PersonnelDto> GetPersonnelById(int id)
    {
        return _mapper.Map<PersonnelDto>(await _uow.PersonnelRepository.GetById(id));
    }

    public async Task<List<PersonnelDto>> GetBySite(int siteId)
    {
        var personnel = await _uow.PersonnelRepository.GetBySiteId(siteId);
        return _mapper.Map<List<PersonnelDto>>(personnel);
    }

    public async Task<PersonnelDto> CreatePersonnel(CreatePersonnelDto createPersonnelDto)
    {

        var site = await _uow.SiteRepository.GetById(createPersonnelDto.SiteId);
        if (site == null) throw new NotFoundException("site not found");
        var existPersonnel =
            await _uow.PersonnelRepository.GetByFullname(createPersonnelDto.FirstName, createPersonnelDto.LastName);
        if (existPersonnel != null) throw new BadRequestException("This personnel is already registered");
        var personnel = _mapper.Map<Personnel>(createPersonnelDto);
        personnel.Site = site;
        var newPersonnel = await _uow.PersonnelRepository.Insert(personnel);
        if (!await _uow.Complete()) throw new BadRequestException("error in saving personel");

        return _mapper.Map<PersonnelDto>(newPersonnel);
    }

    public async Task<List<PersonnelDto>> GetByRole(string role, int siteId)
    {
        var personnel = new List<Personnel>();
        if (role == "manager") personnel = await _uow.PersonnelRepository.GetManagerPersonnel(siteId);
        if (role == "driver") personnel = await _uow.PersonnelRepository.GetDriverPersonnel(siteId);
        if (role == "staff") personnel = await _uow.PersonnelRepository.GetManagerPersonnel(siteId);

        return _mapper.Map<List<PersonnelDto>>(personnel);

    }

    public async Task<PersonnelDto> GetByCode(string code)
    {

        var personnel = await _uow.PersonnelRepository.GetPersonnelByCode(code);
        if (personnel is null) throw new NotFoundException("incorrect authorization");
        return _mapper.Map<PersonnelDto>(personnel);
    }


}
