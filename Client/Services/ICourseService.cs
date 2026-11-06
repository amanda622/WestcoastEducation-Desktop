using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public interface ICourseService
{
    Task<List<Course>> GetCoursesAsync();

    Task AddCourseAsync(Course course);

    Task UpdateCourseAsync(Course course);

    Task DeleteCourseAsync(Course course);
}
