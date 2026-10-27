namespace BajonBurgerClub.LogicaNegocio.Excepciones
{
    public class ResenaInvalidaException : DominioException
    {
        public ResenaInvalidaException(string mensaje) : base(mensaje)
        {
        }
    }
}
