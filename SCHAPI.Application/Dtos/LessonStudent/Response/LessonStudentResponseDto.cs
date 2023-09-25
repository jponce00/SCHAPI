namespace SCHAPI.Application.Dtos.LessonStudent.Response
{
    public class LessonStudentResponseDto
    {
        public int LessonStudentId { get; set; }

        public string? LessonCode { get; set; }

        public string? SubjectName { get; set; }

        public string? StartHour { get; set; }

        public string? StudentCode { get; set; }

        public string? StudentName { get; set; }

        public string? ClassroomName { get; set; }

        public int State { get; set; }
    }
}
