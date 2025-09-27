interface IClassroomManager {
    void AddClassroom(string name);
    void RemoveClassroom(string name);
    void ListClassrooms();
}

interface IStudentManager {
    void AddStudent(string studentId, string className);
    void ListStudents(string className);
}

interface IAssignmentManager {
    void ScheduleAssignment(string className, string assignment);
    void SubmitAssignment(string studentId, string className, string assignment);
}

class Student {
    public string Id { get; }
    public List<string> SubmittedAssignments { get; }

    public Student(string id) {
        Id = id;
        SubmittedAssignments = new List<string>();
    }
}

class Classroom {
    public string Name { get; }
    public List<Student> Students { get; }
    public List<string> Assignments { get; }

    public Classroom(string name) {
        Name = name;
        Students = new List<Student>();
        Assignments = new List<string>();
    }
}

class VirtualClassroomManager : IClassroomManager, IStudentManager, IAssignmentManager {
    private Dictionary<string, Classroom> classrooms = new();

    public void AddClassroom(string name) {
        if (classrooms.ContainsKey(name)) {
            Console.WriteLine($"Classroom {name} already exists.");
            return;
        }
        classrooms[name] = new Classroom(name);
        Console.WriteLine($"Classroom {name} has been created.");
    }

    public void RemoveClassroom(string name) {
        if (!classrooms.Remove(name)) {
            Console.WriteLine($"Classroom {name} not found.");
            return;
        }
        Console.WriteLine($"Classroom {name} removed.");
    }

    public void ListClassrooms() {
        if (!classrooms.Any()) {
            Console.WriteLine("No classrooms available.");
            return;
        }

        Console.WriteLine("Available Classrooms:");
        foreach (var room in classrooms.Values)
            Console.WriteLine($"- {room.Name}");
    }

    public void AddStudent(string studentId, string className) {
        if (!classrooms.TryGetValue(className, out var classroom)) {
            Console.WriteLine($"Classroom {className} not found.");
            return;
        }

        if (classroom.Students.Any(s => s.Id == studentId)) {
            Console.WriteLine($"Student {studentId} already enrolled in {className}.");
            return;
        }

        classroom.Students.Add(new Student(studentId));
        Console.WriteLine($"Student {studentId} has been enrolled in {className}.");
    }

    public void ListStudents(string className) {
        if (!classrooms.TryGetValue(className, out var classroom)) {
            Console.WriteLine($"Classroom {className} not found.");
            return;
        }

        if (!classroom.Students.Any()) {
            Console.WriteLine($"No students in {className}.");
            return;
        }

        Console.WriteLine($"Students in {className}:");
        foreach (var student in classroom.Students)
            Console.WriteLine($"- {student.Id}");
    }

    public void ScheduleAssignment(string className, string assignment) {
        if (!classrooms.TryGetValue(className, out var classroom)) {
            Console.WriteLine($"Classroom {className} not found.");
            return;
        }

        classroom.Assignments.Add(assignment);
        Console.WriteLine($"Assignment for {className} has been scheduled.");
    }

    public void SubmitAssignment(string studentId, string className, string assignment) {
        if (!classrooms.TryGetValue(className, out var classroom)) {
            Console.WriteLine($"Classroom {className} not found.");
            return;
        }

        var student = classroom.Students.FirstOrDefault(s => s.Id == studentId);
        if (student == null) {
            Console.WriteLine($"Student {studentId} not found in {className}.");
            return;
        }

        if (!classroom.Assignments.Contains(assignment)) {
            Console.WriteLine($"Assignment \"{assignment}\" not scheduled in {className}.");
            return;
        }

        student.SubmittedAssignments.Add(assignment);
        Console.WriteLine($"Assignment submitted by Student {studentId} in {className}.");
    }
}

class Program {
    static void Main(string[] args) {
        var manager = new VirtualClassroomManager();
        Console.WriteLine("=== Virtual Classroom Manager ===");

        while (true) {
            Console.Write("\nCommand: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var parts = input.Split(' ', 4);
            var command = parts[0].ToLower();

            try {
                switch (command) {
                    case "add_classroom":
                        manager.AddClassroom(parts[1]);
                        break;
                    case "remove_classroom":
                        manager.RemoveClassroom(parts[1]);
                        break;
                    case "list_classrooms":
                        manager.ListClassrooms();
                        break;
                    case "add_student":
                        manager.AddStudent(parts[1], parts[2]);
                        break;
                    case "list_students":
                        manager.ListStudents(parts[1]);
                        break;
                    case "schedule_assignment":
                        manager.ScheduleAssignment(parts[1], parts[2]);
                        break;
                    case "submit_assignment":
                        manager.SubmitAssignment(parts[1], parts[2], parts[3]);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            } catch (Exception ex) {
                Console.WriteLine($"[ERROR]: {ex.Message}");
            }
        }
    }
}
