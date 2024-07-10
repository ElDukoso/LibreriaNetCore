namespace Libreria.Models;

public class Libro
{
    public int Id { get; set; }
    public string Título { get; set; }
    public string Autor { get; set; }
    public string Descripción { get; set; }
    public decimal Precio { get; set; }
    public string ImagenUrl { get; set; }
    public DateTime FechaPublicacion { get; set; }
}
