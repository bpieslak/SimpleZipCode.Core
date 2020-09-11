using NUnit.Framework;
using SimpleZipCode.Core;
using System.Linq;

namespace SimpleZipCode.Core.Tests.Integration
{
    [TestFixture]
    public class ZipCodeSourceTests
    {
        private IZipCodeRepository _zipCodeRepo;

        [SetUp]
        public void Setup()
        {
            _zipCodeRepo = ZipCodeSource.FromMemory().GetRepository();
        }

        [Test]
        public void Should_search_within_radius()
        {
            var lisle = _zipCodeRepo.Search(x => x.PlaceName.Contains("Lisle"));
            var zipCode = _zipCodeRepo.Get("60606");
            var results = _zipCodeRepo.RadiusSearch(zipCode, 1).ToList();

            Assert.AreEqual(results.Count, 9);
        }
    }
}
