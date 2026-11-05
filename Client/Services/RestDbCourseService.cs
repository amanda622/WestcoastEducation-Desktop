using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public class RestDbCourseService : ICourseService
{
    private readonly IHttpService<Course> _httpService;

    public RestDbCourseService(IHttpService<Course> httpService)
    {
        _httpService = httpService;
    }

    public Task<List<Course>> GetCoursesAsync()
    {
        return _httpService.GetValuesAsync("courses");
    }
}
