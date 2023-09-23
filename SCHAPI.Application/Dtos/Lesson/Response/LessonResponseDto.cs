namespace SCHAPI.Application.Dtos.Lesson.Response
{
    public class LessonResponseDto
    {
        public int LessonId { get; set; }

        public string? LessonCode { get; set; }

        public int ScheduleId { get; set; }

        public string? StartHour { get; set; }

        public int SubjectId { get; set; }

        public string? SubjectName { get; set; }

        public int TeacherId { get; set; }

        public string? TeacherName { get; set; }

        public int ClassroomId { get; set; }

        public string? ClassroomName { get; set; }

        public int State { get; set; }
    }
}
