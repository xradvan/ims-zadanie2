using System;

namespace zadanie2_webapp.API.Models
{
    public class Novinka
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Nazov { get; set; }
        public string Text { get; set; }
    }
}