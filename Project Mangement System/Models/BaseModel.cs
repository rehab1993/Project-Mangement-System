﻿namespace Project_Mangement_System.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
