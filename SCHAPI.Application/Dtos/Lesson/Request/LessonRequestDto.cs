namespace SCHAPI.Application.Dtos.Lesson.Request
{
    public class LessonRequestDto
    {
        public int ScheduleId { get; set; }

        public int SubjectId { get; set; }

        public int ClassroomId { get; set; }

        public int TeacherId { get; set; }

        public int State { get; set; }
    }
}
