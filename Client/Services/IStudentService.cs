using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public interface IStudentService
{
    Task<List<Student>> GetStudentsAsync();

    Task AddStudentAsync(Student student);
}
