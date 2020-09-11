using NUnit.Framework;
using SimpleZipCode.Core;
using SimpleZipCode.Core.Repos;
using System.Collections.Generic;
using System.Linq;

namespace SimpleZipCode.Core.Tests.Unit
{
    [TestFixture]
    public class ZipCodeRepoTests
    {
        private ZipCodeRepo _zipCodeRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            _zipCodeRepo = new ZipCodeRepo(GetZipCodes());
        }

        [Test]
        public void Should_get_zipCode_by_postalCode()
        {
            var zipCode = _zipCodeRepo.Get("81602");

            Assert.Equals(zipCode.PostalCode, "81602");
            Assert.Equals(zipCode.PlaceName, "Glenwood Springs");
            Assert.Equals(zipCode.State, "Colorado");
            Assert.Equals(zipCode.StateAbbreviation, "CO");
            Assert.Equals(zipCode.County, "Garfield");
            Assert.Equals(zipCode.Latitude, 39.5117);
            Assert.Equals(zipCode.Longitude, -107.3253);
        }

        [Test]
        public void Should_search_using_predicate()
        {
            var searchResults = _zipCodeRepo
                .Search(x => x.PlaceName == "Glenwood Springs")
                .ToList();

            Assert.Equals(searchResults.Count, 1);
            Assert.Equals(searchResults[0].PostalCode, "81602");
            Assert.Equals(searchResults[0].PlaceName, "Glenwood Springs");
            Assert.Equals(searchResults[0].State, "Colorado");
            Assert.Equals(searchResults[0].StateAbbreviation, "CO");
            Assert.Equals(searchResults[0].County, "Garfield");
            Assert.Equals(searchResults[0].Latitude, 39.5117);
            Assert.Equals(searchResults[0].Longitude, -107.3253);
        }

        [Test]
        public void Should_perform_radius_search()
        {
            var zipCode = _zipCodeRepo.Get("81611");
            var searchResults = _zipCodeRepo
                .RadiusSearch(zipCode, 4)
                .ToList();

            Assert.Equals(searchResults.Count, 2);
            Assert.Equals(searchResults[0].PostalCode, "81611");
            Assert.Equals(searchResults[1].PostalCode, "81612");
        }

        [Test]
        public void Should_get_default_value_for_zipCode_by_PostalCode()
        {
            Assert.IsNull(_zipCodeRepo.Get("99999"));
        }

        private List<ZipCode> GetZipCodes()
        {
            return new List<ZipCode>
            {
                new ZipCode("81602","Glenwood Springs","Colorado","CO","Garfield",39.5117,-107.3253),
                new ZipCode("81610","Dinosaur","Colorado","CO","Moffat",40.2566,-108.9652),
                new ZipCode("81611","Aspen","Colorado","CO","Pitkin",39.1951,-106.8236),
                new ZipCode("81612","Aspen","Colorado","CO","Pitkin",39.2234,-106.8828),
                new ZipCode("81615","Snowmass Village","Colorado","CO","Pitkin",39.2212,-106.932)
            };
        }
    }
}
