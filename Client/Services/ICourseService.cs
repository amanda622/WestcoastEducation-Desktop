using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Models;

namespace Client.Services;

public interface ICourseService
{
    Task<List<Course>> GetCoursesAsync();
}
