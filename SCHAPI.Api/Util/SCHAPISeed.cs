using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Api.Util
{
    public static class SCHAPISeed
    {
        public async static Task<WebApplication> Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<SCHAPIContext>();
            try
            {
                if (await context.Database.EnsureCreatedAsync())
                {
                    await context.Teachers.AddRangeAsync(
                        new Teacher { Name = "Ricardo Gabriel Canales", Phone = "90000000", Email = "ricardo@mail.com" },
                        new Teacher { Name = "Emilio Arturo Izaguirre Girón", Phone = "90000001", Email = "emilio@mail.com" },
                        new Teacher { Name = "Victor Salvador Bernárdez Blanco", Phone = "90000002", Email = "victor@mail.com" });

                    await context.Students.AddRangeAsync(
                        new Student { Name = "Johnny Eulogio Palacios Suazo", Phone = "90000003", Email = "johnny@mail.com" },
                        new Student { Name = "Ramón Núñez Reyes", Phone = "90000004", Email = "ramon@mail.com" },
                        new Student { Name = "Óscar García Ramírez", Phone = "90000005", Email = "oscar@mail.com" },
                        new Student { Name = "Noel Eduardo Valladares Bonilla", Phone = "90000006", Email = "noel@mail.com" },
                        new Student { Name = "Elvis Danilo Turcios", Phone = "90000007", Email = "danilo@mail.com" });

                    await context.Subjects.AddRangeAsync(
                        new Subject { Name = "Matemáticas" },
                        new Subject { Name = "Español" },
                        new Subject { Name = "Ciencias Sociales" },
                        new Subject { Name = "Ciencias Naturales" },
                        new Subject { Name = "Física Elemental" },
                        new Subject { Name = "Química" });

                    await context.Classrooms.AddRangeAsync(
                        new Classroom { Name = "Aula 1" },
                        new Classroom { Name = "Aula 2" },
                        new Classroom { Name = "Aula 3" },
                        new Classroom { Name = "Aula 4" },
                        new Classroom { Name = "Aula 5" },
                        new Classroom { Name = "Aula 6" });

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return app;
        }
    }
}
