using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public interface ITeacherService
{
    Task<List<Teacher>> GetTeachersAsync();

    Task AddTeacherAsync(Teacher teacher);

    Task UpdateTeacherAsync(Teacher teacher);
}
