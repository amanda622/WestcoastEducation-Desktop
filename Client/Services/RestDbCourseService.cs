using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public class RestDbCourseService : ICourseService
{
    private const string Resource = "courses";

    private readonly IHttpService<Course> _httpService;

    public RestDbCourseService(IHttpService<Course> httpService)
    {
        _httpService = httpService;
    }

    public Task<List<Course>> GetCoursesAsync()
    {
        return _httpService.GetValuesAsync(Resource);
    }

    public Task AddCourseAsync(Course course)
    {
        return _httpService.PostAsync(Resource, course);
    }

    public Task UpdateCourseAsync(Course course)
    {
        return _httpService.PutAsync(Resource, GetDbId(course), course);
    }

    public Task DeleteCourseAsync(Course course)
    {
        return _httpService.DeleteAsync(Resource, GetDbId(course));
    }

    private static string GetDbId(Course course)
    {
        return course.DbId ?? throw new InvalidOperationException("The course has no RestDb id.");
    }
}
