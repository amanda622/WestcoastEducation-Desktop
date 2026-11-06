using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public class RestDbTeacherService : ITeacherService
{
    private const string Resource = "teachers";

    private readonly IHttpService<Teacher> _httpService;

    public RestDbTeacherService(IHttpService<Teacher> httpService)
    {
        _httpService = httpService;
    }

    public Task<List<Teacher>> GetTeachersAsync()
    {
        return _httpService.GetValuesAsync(Resource);
    }

    public Task AddTeacherAsync(Teacher teacher)
    {
        return _httpService.PostAsync(Resource, teacher);
    }

    public Task UpdateTeacherAsync(Teacher teacher)
    {
        string id = teacher.DbId ?? throw new InvalidOperationException("The teacher has no RestDb id.");
        return _httpService.PutAsync(Resource, id, teacher);
    }
}
