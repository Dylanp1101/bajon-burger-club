namespace BajonBurgerClub.LogicaNegocio.Excepciones
{
    public class PuntajeInvalidoException : DominioException
    {
        public PuntajeInvalidoException(string mensaje) : base(mensaje)
        {
        }
    }
}
