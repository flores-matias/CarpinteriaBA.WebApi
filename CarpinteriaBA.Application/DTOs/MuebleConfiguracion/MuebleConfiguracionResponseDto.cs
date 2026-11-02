namespace CarpinteriaBA.Application.DTOs.MuebleConfiguracion
{
    public class MuebleConfiguracionResponseDto
    {
        public int Id { get; set; }
        public int IdMueble { get; set; }
        public int IdConfiguracion { get; set; }

        // Datos de la configuracion, para mostrar sin buscarla aparte
        public string Clave { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}