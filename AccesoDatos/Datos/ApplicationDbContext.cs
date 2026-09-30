using System;
using System.IO;
using AccesoDatos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Cancion> Canciones { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            string carpeta = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);

            string ruta = Path.Combine(
                carpeta, "MusicaParcial2026.db");

            optionsBuilder.UseSqlite($"Data Source={ruta}");
        }
    }
}