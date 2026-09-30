using System.Collections.Generic;

namespace AccesoDatos.Modelos
{
    public class Artista
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        public List<Cancion> Canciones { get; set; } = new List<Cancion>();
    }
}