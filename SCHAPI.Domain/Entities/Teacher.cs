﻿namespace SCHAPI.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public string TeacherCode { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
