﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Transfer
{
    public class PaquetesTuristicodt
    {
        public int Id { get; set; }
        public string PaqueteTuristico { get; set; }
        public string Descripcion { get; set; }
        public string Moneda { get; set; }
        public string Simbolo { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? TiempoDuracion { get; set; }
        public string UnidadDuracion { get; set; }
        public int? favo { get; set; }
        public ICollection<Favoritodt> Favoritos { get; set; }
        public int IdCategoria { get; set; }
        public int IdPais { get; set; }
        public virtual string Unaimagen { get; set; }
        public virtual double Raiting { get; set; }
        public virtual int? Esfavorito { get; set; }
        //public virtual ICollection<Galeriadt> Galeria { get; set; }
    }
}
