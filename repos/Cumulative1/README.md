# Teacher, Student, and Course Management System

## Project Overview
This project is a web application designed to manage and display information related to teachers, students, and courses. It includes functionality for listing, searching, viewing, adding, and deleting data through both API endpoints and Razor views. The system is built using ASP.NET Core MVC and integrates MySQL for database operations.

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

- **TeachersAPIController**:
  - `ListTeachers`: Retrieves a list of all teachers from the database.
  - `FindTeacher`: Retrieves details for a specific teacher by ID.
  - `AddTeacher`: Adds a new teacher to the database.
  - `DeleteTeacher`: Deletes a teacher by ID from the database.

### 2. Razor Page Controllers
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
   ```bash
   git clone <repository-url>