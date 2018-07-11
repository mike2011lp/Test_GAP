namespace Clinica.Services.Models
{
    /// <summary>
    /// Modelo que aplica para la entidad ubicación
    /// </summary>
    public class UbicacionModel
    {
        #region Construction
        public UbicacionModel() { }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; }
        #endregion
    }
}
