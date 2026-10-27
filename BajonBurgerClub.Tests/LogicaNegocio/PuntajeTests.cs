using BajonBurgerClub.LogicaNegocio.Excepciones;
using BajonBurgerClub.LogicaNegocio.ValueObjects;

namespace BajonBurgerClub.Tests.LogicaNegocio
{
    public class PuntajeTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(10)]
        public void Crear_ConValorEntre1y10_GuardaElValor(int valor)
        {
            Puntaje puntaje = new Puntaje(valor);

            Assert.Equal(valor, puntaje.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(-3)]
        public void Crear_ConValorFueraDeRango_LanzaExcepcion(int valor)
        {
            Assert.Throws<PuntajeInvalidoException>(() => new Puntaje(valor));
        }

        [Fact]
        public void DosPuntajesConElMismoValor_SonIguales()
        {
            Assert.Equal(new Puntaje(8), new Puntaje(8));
        }
    }
}
