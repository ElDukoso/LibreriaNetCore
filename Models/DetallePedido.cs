namespace Libreria.Models;

public class DetallePedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int LibroId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public Pedido Pedido { get; set; }
    public Libro Libro { get; set; }
}
