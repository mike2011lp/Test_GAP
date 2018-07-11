namespace Clinica.Services.Models
{
    /// <summary>
    /// Modelo que aplica para la entidad parámetro
    /// </summary>
    public class ParametroModel
    {
        #region Construction
        public ParametroModel() { }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Categoria { get; set; }

        public string Codigo { get; set; }

        public string ValorPrincipal { get; set; }

        public string ValorSecundario { get; set; }
        #endregion
    }
}
