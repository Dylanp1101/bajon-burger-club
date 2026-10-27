using BajonBurgerClub.LogicaNegocio.Excepciones;

namespace BajonBurgerClub.LogicaNegocio.ValueObjects
{
    // Nota de un criterio (pan, carne, papas...). Siempre entre 1 y 10.
    public record Puntaje
    {
        public const int Minimo = 1;
        public const int Maximo = 10;

        public int Valor { get; }

        public Puntaje(int valor)
        {
            if (valor < Minimo || valor > Maximo)
            {
                throw new PuntajeInvalidoException($"El puntaje debe estar entre {Minimo} y {Maximo}. Se recibió {valor}.");
            }
            Valor = valor;
        }
    }
}
