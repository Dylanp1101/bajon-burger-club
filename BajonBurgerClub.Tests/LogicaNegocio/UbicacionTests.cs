using BajonBurgerClub.LogicaNegocio.Excepciones;
using BajonBurgerClub.LogicaNegocio.ValueObjects;

namespace BajonBurgerClub.Tests.LogicaNegocio
{
    public class UbicacionTests
    {
        [Fact]
        public void Crear_EnMontevideo_GuardaLosDatos()
        {
            Ubicacion ubicacion = new Ubicacion("  Av. Brasil 2500 ", "Pocitos", "Montevideo", -34.911, -56.150);

            Assert.Equal("Av. Brasil 2500", ubicacion.Direccion);
            Assert.Equal("Montevideo", ubicacion.Departamento);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Crear_SinDireccion_LanzaExcepcion(string direccion)
        {
            Assert.Throws<UbicacionInvalidaException>(() => new Ubicacion(direccion, "Pocitos", "Montevideo", -34.911, -56.150));
        }

        [Fact]
        public void Crear_ConDepartamentoQueNoExiste_LanzaExcepcion()
        {
            Assert.Throws<UbicacionInvalidaException>(() => new Ubicacion("Corrientes 1234", "Centro", "Buenos Aires", -34.911, -56.150));
        }

        [Fact]
        public void Crear_ConCoordenadasFueraDeUruguay_LanzaExcepcion()
        {
            // Coordenadas de Madrid.
            Assert.Throws<UbicacionInvalidaException>(() => new Ubicacion("Gran Vía 1", "Centro", "Montevideo", 40.42, -3.70));
        }
    }
}
