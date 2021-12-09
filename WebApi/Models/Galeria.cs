using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Galeria
    {
        public int Id { get; set; }
        public int? PaquetesTuristicoId { get; set; }
        public string Url { get; set; }

        public virtual PaquetesTuristico PaquetesTuristico { get; set; }
    }
}
