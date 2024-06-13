﻿namespace Common.DTO
{
    public class BookingModel
    {
        public List<BookingDetailDTO> Details { get; set; } = null!;
        public int Price { get; set; }
        public int CustomerId { get; set; }
    }
}