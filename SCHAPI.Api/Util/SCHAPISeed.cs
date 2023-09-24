using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Api.Util
{
    public static class SCHAPISeed
    {
        public static async Task<WebApplication> Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<SCHAPIContext>();
            try
            {
                if (await context.Database.EnsureCreatedAsync())
                {
                    await AddTeachers(context);
                    await AddStudents(context);
                    await AddSubjects(context);
                    await AddClassrooms(context);
                    await AddSchedules(context);

                    await AddLessons(context);
                    await AddLessonStudents(context);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return app;
        }

        private static async Task AddTeachers(SCHAPIContext context)
        {
            await context.Teachers.AddRangeAsync(
                new Teacher { Name = "Ricardo Gabriel Canales", Phone = "90000000", Email = "ricardo@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Teacher { Name = "Emilio Arturo Izaguirre Girón", Phone = "90000001", Email = "emilio@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Teacher { Name = "Victor Salvador Bernárdez Blanco", Phone = "90000002", Email = "victor@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }

        private static async Task AddStudents(SCHAPIContext context)
        {
            await context.Students.AddRangeAsync(
                new Student { Name = "Johnny Eulogio Palacios Suazo", Phone = "90000003", Email = "johnny@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Student { Name = "Ramón Núñez Reyes", Phone = "90000004", Email = "ramon@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Student { Name = "Óscar García Ramírez", Phone = "90000005", Email = "oscar@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Student { Name = "Noel Eduardo Valladares Bonilla", Phone = "90000006", Email = "noel@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Student { Name = "Elvis Danilo Turcios", Phone = "90000007", Email = "danilo@mail.com", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }

        private static async Task AddSubjects(SCHAPIContext context)
        {
            await context.Subjects.AddRangeAsync(
                new Subject { Name = "Matemáticas", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Subject { Name = "Español", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Subject { Name = "Ciencias Sociales", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Subject { Name = "Ciencias Naturales", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Subject { Name = "Física Elemental", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Subject { Name = "Química", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }

        private static async Task AddClassrooms(SCHAPIContext context)
        {
            await context.Classrooms.AddRangeAsync(
                new Classroom { Name = "Aula 1", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Classroom { Name = "Aula 2", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Classroom { Name = "Aula 3", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Classroom { Name = "Aula 4", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Classroom { Name = "Aula 5", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Classroom { Name = "Aula 6", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }

        private static async Task AddSchedules(SCHAPIContext context)
        {
            await context.Schedules.AddRangeAsync(
                new Schedule { StartHour = new TimeSpan(8, 0, 0), EndHour = new TimeSpan(9, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(10, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(10, 0, 0), EndHour = new TimeSpan(11, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(11, 0, 0), EndHour = new TimeSpan(12, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(12, 0, 0), EndHour = new TimeSpan(13, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(13, 0, 0), EndHour = new TimeSpan(14, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(14, 0, 0), EndHour = new TimeSpan(15, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(15, 0, 0), EndHour = new TimeSpan(16, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Schedule { StartHour = new TimeSpan(16, 0, 0), EndHour = new TimeSpan(17, 0, 0), AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }

        private static async Task AddLessons(SCHAPIContext context)
        {
            await context.Lessons.AddRangeAsync(
                new Lesson { ScheduleId = 1, TeacherId = 1, SubjectId = 1, ClassroomId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Lesson { ScheduleId = 2, TeacherId = 1, SubjectId = 2, ClassroomId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Lesson { ScheduleId = 3, TeacherId = 1, SubjectId = 3, ClassroomId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Lesson { ScheduleId = 4, TeacherId = 1, SubjectId = 4, ClassroomId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new Lesson { ScheduleId = 5, TeacherId = 2, SubjectId = 5, ClassroomId = 2, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }

        private static async Task AddLessonStudents(SCHAPIContext context)
        {
            await context.LessonStudents.AddRangeAsync(
                new LessonStudent { LessonId = 1, StudentId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new LessonStudent { LessonId = 2, StudentId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new LessonStudent { LessonId = 3, StudentId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new LessonStudent { LessonId = 4, StudentId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
                new LessonStudent { LessonId = 5, StudentId = 1, AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 });

            await context.SaveChangesAsync();
        }
    }
}
