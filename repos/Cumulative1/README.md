# Teacher, Student, and Course Management System

## Project Overview
This project is a web application designed to manage and display information related to teachers, students, and courses. It includes functionality for listing, searching, and viewing detailed data through both API endpoints and Razor views. The system is built using ASP.NET Core MVC and integrates MySQL for database operations.

---

## Features

### 1. API Controllers
- **StudentsAPIController**:
  - `ListStudents`: Retrieves a list of all students from the database.
  - `FindStudent`: Retrieves details for a specific student by ID.

- **CoursesAPIController**:
  - `ListCourses`: Retrieves a list of all courses from the database.
  - `FindCourse`: Retrieves details for a specific course by ID.

- **TeachersAPIController**:
  - `ListTeachers`: Retrieves a list of all teachers from the database.
  - `FindTeacher`: Retrieves details for a specific teacher by ID.

### 2. Razor Page Controllers
- **StudentPageController**:
  - `StudentList`: Displays a list of all students in a Razor view.
  - `StudentShow`: Displays detailed information about a specific student.

- **TeacherPageController**:
  - `TeacherList`: Displays a list of all teachers in a Razor view.
  - `Show`: Displays detailed information about a specific teacher, including courses taught by them.

- **Course List View**:
  - Lists all courses with links to their respective detailed pages.

---

## Technologies Used
- **Framework**: ASP.NET Core MVC
- **Language**: C#
- **Database**: MySQL
- **Frontend**: Razor Pages

---

## Setup Instructions
1. Clone the repository to your local machine:
2.**Database Connection Parameters**:
   - Update the database connection string in the `SchoolDbContext` file with the following:
     - **Server**: Your database server (e.g., `localhost` or a remote server IP).
     - **Port**: The MySQL port (default: `3306`).
     - **Database**: The database name (e.g., `SchoolDB`).
     - **Username**: Your database username (e.g., `root`).
     - **Password**: Your database password.
   git clone <repository-url>