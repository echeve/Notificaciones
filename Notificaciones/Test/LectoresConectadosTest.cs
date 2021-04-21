using Notificaciones.SignalRHub.Code;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class LectoresConectadosTest
    {
        private LectoresConectados _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LectoresConectados();
        }

        [Test]
        public void DADO_un_lectos_CUANDO_lo_inserto_ENTONCES_se_inserta_correctamente()
        {
            //Dado
            var lector = "lector1";

            //Cuando
            _sut.AnadirLector(lector);

            //Entonces
            Assert.That(_sut.ObtenerLectores().Contains(lector));
        }

        [Test]
        public void DADO_un_lector_existente_CUANDO_lo_inserto_ENTONCES_no_inserto_nada()
        {
            //Dado
            var lector1 = "lector1";

            _sut.AnadirLector(lector1);

            //Cuando
            _sut.AnadirLector(lector1);

            //Entonces
            Assert.AreEqual(_sut.ObtenerLectores().Count, 1);
            Assert.That(_sut.ObtenerLectores().Contains(lector1));
        }

        [Test]
        public void DADO_un_lector_nuevo_CUANDO_intento_eliminarlo_ENTONCES_no_elimino()
        {
            //Dado
            var lector1 = "lector1";
            var lector2 = "lector2";

            _sut.AnadirLector(lector1);

            //Cuando
            _sut.EliminarLector(lector2);

            //Entonces
            Assert.AreEqual(_sut.ObtenerLectores().Count, 1);
            Assert.That(_sut.ObtenerLectores().Contains(lector1));
        }

        [Test]
        public void DADO_un_lector_existente_CUANDO_intento_eliminarlo_ENTONCES_lo_elimino()
        {
            //Dado
            var lector1 = "lector1";

            _sut.AnadirLector(lector1);

            //Cuando
            _sut.EliminarLector(lector1);

            //Entonces
            Assert.IsEmpty(_sut.ObtenerLectores());

        }

        [Test]
        public void CUANDO_imprimo_una_lista_vacia_ENTONCES_devuelvo_un_string_vacio()
        {
            //Cuando
            var resultado = _sut.ObtenerLectoresConectados();

            //Entonces
            Assert.IsEmpty(_sut.ObtenerLectores());
            Assert.IsEmpty(resultado);
        }

        [Test]
        public void DADA_una_lista_con_usuarios_CUANDO_la_imprimo_ENTONCES_devuelvo_los_usuarios()
        {
            //Dado
            var lector1 = "lector1";
            _sut.AnadirLector(lector1);

            //Cuando
            var resultado = _sut.ObtenerLectoresConectados();

            //Entonces
            Assert.IsNotEmpty(resultado);
            Assert.IsTrue(resultado.Contains(lector1));

        }
    }
}