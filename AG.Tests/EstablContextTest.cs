using SQLiteRepository;

namespace AG.Tests
{
    public class EstablContextTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreatingEstablishmentsContext()
        {
            EstablishmentContext ctx = new EstablishmentContext();
            Assert.Pass();
        }
    }
}