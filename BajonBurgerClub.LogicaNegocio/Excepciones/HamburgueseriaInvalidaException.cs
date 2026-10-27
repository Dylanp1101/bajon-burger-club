namespace BajonBurgerClub.LogicaNegocio.Excepciones
{
    public class HamburgueseriaInvalidaException : DominioException
    {
        public HamburgueseriaInvalidaException(string mensaje) : base(mensaje)
        {
        }
    }
}
