using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public class RestDbStudentService : IStudentService
{
    private const string Resource = "students";

    private readonly IHttpService<Student> _httpService;

    public RestDbStudentService(IHttpService<Student> httpService)
    {
        _httpService = httpService;
    }

    public Task<List<Student>> GetStudentsAsync()
    {
        return _httpService.GetValuesAsync(Resource);
    }

    public Task AddStudentAsync(Student student)
    {
        return _httpService.PostAsync(Resource, student);
    }
}
