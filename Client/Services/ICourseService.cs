using WestcoastEducation.Models;
using System.Collections.Generic;

namespace Client.Services;

public interface ICourseService
{
    List<Course> GetCourses();

}
