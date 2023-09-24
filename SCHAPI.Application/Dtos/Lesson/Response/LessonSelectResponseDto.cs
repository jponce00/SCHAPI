namespace SCHAPI.Application.Dtos.Lesson.Response
{
    public class LessonSelectResponseDto
    {
        public int LessonId { get; set; }

        public string? LessonCode { get; set; }

        public string? SubjectName { get; set; }

        public string? StartHour { get; set; }
    }
}
