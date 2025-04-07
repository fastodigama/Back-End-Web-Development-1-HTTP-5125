# Teacher, Student, and Course Management System

## Project Overview
This project is a web application designed to manage and display information related to teachers, students, and courses. It includes functionality for listing, searching, viewing, adding, and deleting data through both API endpoints, Razor views, and AJAX functionalities. The system is built using ASP.NET Core MVC and integrates MySQL for database operations.

---

## Features

### 1. API Controllers
- **StudentsAPIController**:
  - `ListStudents`: Retrieves a list of all students from the database.
  - `FindStudent`: Retrieves details for a specific student by ID.
  - `AddStudent`: Adds a new student to the database.
  - `DeleteStudent`: Deletes a student by ID from the database.

- **CoursesAPIController**:
  - `ListCourses`: Retrieves a list of all courses from the database.
  - `FindCourse`: Retrieves details for a specific course by ID.
  - `AddCourse`: Adds a new course to the database.
  - `DeleteCourse`: Deletes a course by ID from the database.

- **TeachersAPIController**:
  - `ListTeachers`: Retrieves a list of all teachers from the database.
  - `FindTeacher`: Retrieves details for a specific teacher by ID.
  - `AddTeacher`: Adds a new teacher to the database.
  - `DeleteTeacher`: Deletes a teacher by ID from the database.

### 2. AJAX Functionalities for Teachers
- **List Teachers**:
  - Dynamically fetches and displays a list of teachers without refreshing the page.
  - Integrates search and pagination for enhanced usability.
- **Add Teacher**:
  - Provides an interactive form to add a new teacher using AJAX.
  - Updates the teacher list seamlessly upon successful addition.
- **Delete Teacher**:
  - Allows deletion of a teacher via AJAX without reloading the page.
  - Ensures instant feedback and updates the displayed list automatically.

### 3. Razor Page Controllers
- **StudentPageController**:
  - `StudentList`: Displays a list of all students in a Razor view.
  - `StudentShow`: Displays detailed information about a specific student.
  - `AddStudentPage`: Provides a form to add a new student and displays confirmation.
  - `DeleteStudentPage`: Allows deletion of a student via ID.

- **TeacherPageController**:
  - `TeacherList`: Displays a list of all teachers in a Razor view.
  - `Show`: Displays detailed information about a specific teacher, including courses taught by them.
  - `AddTeacherPage`: Provides a form to add a new teacher and displays confirmation.
  - `DeleteTeacherPage`: Allows deletion of a teacher via ID.

- **CoursePageController**:
  - `CourseList`: Displays a list of all courses in a Razor view.
  - `CourseShow`: Displays detailed information about a specific course.
  - `AddCoursePage`: Provides a form to add a new course and displays confirmation.
  - `DeleteCoursePage`: Allows deletion of a course via ID.

### 4. Razor Views
- **Student Management**:
  - List all students with pagination and search functionality.
  - View detailed information for a student, including enrolled courses.
  - Add a new student using a form.
  - Delete a student with a confirmation dialog.

- **Teacher Management**:
  - List all teachers, including basic details and courses they teach.
  - View detailed information for a specific teacher.
  - Add a new teacher using a form.
  - Delete a teacher with a confirmation dialog.

- **Course Management**:
  - List all courses with basic course details.
  - View detailed information for a specific course, including students and teachers linked to it.
  - Add a new course using a form.
  - Delete a course with a confirmation dialog.

---

## Technologies Used
- **Framework**: ASP.NET Core MVC
- **Language**: C#
- **Database**: MySQL
- **Frontend**: Razor Pages
- **CSS Frameworks**: Custom styling
- **AJAX**: For dynamic, asynchronous functionalities
- **Tools**: Visual Studio, MySQL PHP admin.

---

## Setup Instructions
1. Clone the repository to your local machine:
   ```bash
   git clone <repository-url>