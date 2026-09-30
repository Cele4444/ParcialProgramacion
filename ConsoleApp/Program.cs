using System;
using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Datos;
using AccesoDatos.Modelos;
using AccesoDatos.Repositorios;

using ApplicationDbContext contexto = new ApplicationDbContext();

GenericRepository<Artista> artistaRepo =
    new GenericRepository<Artista>(contexto);

GenericRepository<Cancion> cancionRepo =
    new GenericRepository<Cancion>(contexto);

string opcion = "";

while (opcion != "0")
{
    Console.WriteLine("\n--- PLATAFORMA DE MUSICA ---");
    Console.WriteLine("1. Alta artista");
    Console.WriteLine("2. Alta canción");
    Console.WriteLine("3. Ver canciones");
    Console.WriteLine("4. Mostrar canciones más largas");
    Console.WriteLine("5. Cantidad total de canciones");
    Console.WriteLine("6. Mostrar canciones ordenadas alfabéticamente por título");
    Console.WriteLine("7. Verificar si existen canciones registradas");
    Console.WriteLine("0. Salir");
    Console.Write("Elegí una opción: ");

    opcion = Console.ReadLine() ?? "";

    switch (opcion)
    {
        case "1":
            {
                Console.Write("Nombre del artista: ");
                string nombre = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.WriteLine("El nombre no puede estar vacío.");
                    break;
                }

                Artista artista = new Artista();
                artista.Nombre = nombre.Trim();

                artistaRepo.Agregar(artista);

                Console.WriteLine("Artista registrado.");
                break;
            }

        case "2":
            {
                List<Artista> artistas = artistaRepo.ObtenerTodos();

                if (artistas.Count == 0)
                {
                    Console.WriteLine("Primero tenés que registrar un artista.");
                    break;
                }

                Console.WriteLine("\nARTISTAS REGISTRADOS");

                foreach (Artista artista in artistas)
                {
                    Console.WriteLine($"{artista.Id} - {artista.Nombre}");
                }

                Console.Write("Id del artista: ");

                if (!int.TryParse(Console.ReadLine(), out int artistaId))
                {
                    Console.WriteLine("Ingresá un número válido.");
                    break;
                }

                Artista? artistaSeleccionado =
                    artistaRepo.ObtenerPorId(artistaId);

                if (artistaSeleccionado == null)
                {
                    Console.WriteLine("No existe un artista con ese Id.");
                    break;
                }

                Console.Write("Título de la canción: ");
                string titulo = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(titulo))
                {
                    Console.WriteLine("El título no puede estar vacío.");
                    break;
                }

                Console.Write("Duración en segundos: ");

                if (!int.TryParse(Console.ReadLine(), out int duracion)
                    || duracion <= 0)
                {
                    Console.WriteLine("La duración debe ser un número mayor a cero.");
                    break;
                }

                Cancion cancion = new Cancion();
                cancion.Titulo = titulo.Trim();
                cancion.Duracion = duracion;
                cancion.ArtistaId = artistaSeleccionado.Id;

                cancionRepo.Agregar(cancion);

                Console.WriteLine("Canción registrada.");
                break;
            }

        case "3":
            {
                List<Cancion> canciones =
                    cancionRepo.ObtenerTodosCon("Artista");

                if (canciones.Count == 0)
                {
                    Console.WriteLine("No hay canciones registradas.");
                    break;
                }

                foreach (Cancion cancion in canciones)
                {
                    Console.WriteLine(
                        $"{cancion.Id} - {cancion.Titulo} | " +
                        $"{cancion.Duracion} segundos | " +
                        $"Artista: {cancion.Artista.Nombre}");
                }

                break;
            }

        case "4":
            {
                List<Cancion> canciones = contexto.Canciones
                    .OrderByDescending(c => c.Duracion)
                    .ToList();

                if (canciones.Count == 0)
                {
                    Console.WriteLine("No hay canciones registradas.");
                    break;
                }

                Console.WriteLine("\nCANCIONES DE MAYOR A MENOR DURACIÓN");

                foreach (Cancion cancion in canciones)
                {
                    Console.WriteLine(
                        $"{cancion.Titulo} - {cancion.Duracion} segundos");
                }

                break;
            }

        case "5":
            {
                int cantidad = contexto.Canciones.Count();

                Console.WriteLine($"Cantidad total de canciones: {cantidad}");
                break;
            }

        case "6":
            {
                List<Cancion> canciones = contexto.Canciones
                    .OrderBy(c => c.Titulo)
                    .ToList();

                if (canciones.Count == 0)
                {
                    Console.WriteLine("No hay canciones registradas.");
                    break;
                }

                foreach (Cancion cancion in canciones)
                {
                    Console.WriteLine(
                        $"{cancion.Titulo} - {cancion.Duracion} segundos");
                }

                break;
            }

        case "7":
            {
                bool existen = contexto.Canciones.Any();

                if (existen)
                {
                    Console.WriteLine("Sí, existen canciones registradas.");
                }
                else
                {
                    Console.WriteLine("No hay canciones registradas.");
                }

                break;
            }

        case "0":
            {
                Console.WriteLine("Programa finalizado.");
                break;
            }

        default:
            {
                Console.WriteLine("Opción incorrecta.");
                break;
            }
    }
}