# Westcoast Education

A desktop admin app for Westcoast Education, built with **Avalonia UI** and **.NET 10**.
It manages courses, students and teachers, and stores all data in a **RestDb.io** database.

## Features

**Courses**
- List all courses
- Add a course (course number, title, length in weeks, start date, type: Klassrum / Distans)
- Update a course: click it in the list, edit the form, press *Save changes*
- Delete a course

**Students**
- List all students
- Add a student (personal number, first name, last name, email, phone)

**Teachers**
- List all teachers
- Add a teacher
- Manage a teacher's knowledge area and update teacher info: click the teacher, edit, press *Save changes*

All forms validate required fields and check for duplicates (course number / personal number).
A status line under each form shows the result or the error message.

## How to run

### 1. Create `Client/appsettings.json`

The file is **not** in the repository, because it contains the API key (it is listed in `.gitignore`).
Create it with this content:

```json
{
  "RestDb": {
    "BaseUrl": "https://<your-database>.restdb.io/rest/",
    "ApiKey": "<your-api-key>"
  }
}
```

### 2. RestDb collections

The database needs three collections. All fields are of type *text* unless noted.

| Collection | Fields |
|---|---|
| `courses` | `courseNumber`, `title`, `lengthInWeeks` (number), `startDate` (date), `courseType` |
| `students` | `personNumber`, `firstName`, `lastName`, `email`, `phone` |
| `teachers` | `personNumber`, `firstName`, `lastName`, `email`, `phone`, `knowledgeArea` |

### 3. Start the app

```bash
dotnet run --project Client
```

## Project structure

| Project | Content |
|---|---|
| `Core/` | Class library with the domain model: `Course`, `Person`, `Student`, `Teacher`, `CourseType`, ... (reused from the first, console version of the system) |
| `Client/` | The Avalonia desktop app |

Inside `Client/`:

| Folder | Content |
|---|---|
| `Views/` | XAML pages: `CoursesView`, `StudentsView`, `TeachersView` |
| `ViewModels/` | One ViewModel per page, plus `MainWindowViewModel` for navigation |
| `Services/` | `IHttpService<T>` / `HttpService<T>` and one service per entity (`ICourseService` / `RestDbCourseService`, ...) |
| `App.axaml` | Colors and styles as resources |
| `App.axaml.cs` | Composition root: reads the configuration and registers all services |

## Architecture

### MVVM

Views contain only XAML and bindings. All logic lives in the ViewModels, built with **CommunityToolkit.Mvvm**:

- `[ObservableProperty]` creates properties that notify the UI when they change
- `[RelayCommand]` turns a method into a command that a button can bind to
- Form fields use **two-way binding**: what the user types goes straight into the ViewModel
- Selecting an item in a list runs `OnSelectedCourseChanged` / `OnSelectedTeacherChanged`, which copies the item into the form

Navigation: `MainWindowViewModel.CurrentView` holds the current page's ViewModel. A `ContentControl` shows it, and `DataTemplates` in `MainWindow.axaml` pick the matching View.

### Dependency Injection

`App.axaml.cs` uses **Microsoft.Extensions.DependencyInjection**. It registers the configuration, the HTTP services, the entity services and `MainWindowViewModel`. Every class gets what it needs through its constructor, and depends on **interfaces**, not concrete classes.

### HTTP communication

All HTTP code is split into an interface and an implementation:

```
ViewModel → ICourseService (RestDbCourseService) → IHttpService<Course> (HttpService<Course>) → RestDb
```

- `HttpService<T>` is generic and handles `GET`, `POST`, `PUT` and `DELETE`, JSON (camelCase, enums as strings) and the `x-apikey` header
- The entity services only know *which* collection to use (`courses`, `students`, `teachers`)
- RestDb gives every record an `_id`. It is mapped to `DbId` in the model and used for update and delete

Because the ViewModels only see `ICourseService`, the data source can be swapped. `InMemoryCourseService` is an example that keeps courses in memory, useful for testing the UI without RestDb.

### Design

Colors, font size and corner radius are defined once in `App.axaml` and used everywhere with `{DynamicResource ...}`. Changing one value there changes the whole app. Button and text styles are shared through style classes (`primary`, `danger`, `nav`, `pageTitle`, `status`).

## Class design (Core)

```
Person
├── Student
└── Teacher
    └── EducationLeader
        └── Administrator
```

- **Inheritance:** common data (name, personal number, contact details) lives in `Person`; `Teacher` adds `KnowledgeArea`
- **Composition:** `Person` → `Address`
- **Aggregation:** `Teacher` → `List<Course>`
- `CourseType` is an enum (`Klassrum`, `Distans`), so a course can only have a valid type
- `EndDate` is calculated from `StartDate` + `LengthInWeeks`, so the dates can never contradict each other
