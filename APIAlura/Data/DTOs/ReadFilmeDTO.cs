﻿namespace APIAlura.Data.DTOs
{
    public class ReadFilmeDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }
        public DateTime DataConsulta { get; set; } = DateTime.Now;
    }
}
