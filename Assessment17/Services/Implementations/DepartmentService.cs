using AutoMapper;
using Assessment17.DTOs;
using Assessment17.Models;
using Assessment17.Repositories.Interfaces;
using Assessment17.Services.Interfaces;

namespace Assessment17.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<DepartmentReadDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return _mapper.Map<List<DepartmentReadDto>>(list);
    }

    public async Task<DepartmentReadDto?> GetByIdAsync(int id)
    {
        var dept = await _repo.GetByIdAsync(id);
        return dept is null ? null : _mapper.Map<DepartmentReadDto>(dept);
    }

    public async Task<DepartmentReadDto> CreateAsync(DepartmentCreateDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        await _repo.AddAsync(entity);
        await _repo.SaveAsync();
        return _mapper.Map<DepartmentReadDto>(entity);
    }

    public async Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto)
    {
        var dept = await _repo.GetByIdAsync(id);
        if (dept is null) return false;

        _mapper.Map(dto, dept);
        _repo.Update(dept);
        return await _repo.SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dept = await _repo.GetByIdAsync(id);
        if (dept is null) return false;

        _repo.Delete(dept);
        return await _repo.SaveAsync();
    }
}