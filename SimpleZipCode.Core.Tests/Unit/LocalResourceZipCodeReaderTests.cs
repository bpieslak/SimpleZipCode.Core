using NUnit.Framework;
using SimpleZipCode.Core;
using SimpleZipCode.Core.Sources;
using System.Collections.Generic;
using System.Linq;

namespace SimpleZipCode.Core.Tests.Unit
{
    [TestFixture]
    public class LocalResourceZipCodeReaderTests
    {
        private static readonly string _localResourceCsv = @"Postal Code,Place Name,State,State Abbreviation,County,Latitude,Longitude,
                                                     12345,Springfield,TV Land,ZZ,Cook,41.8868,-87.6386,
                                                     ,,,,,,,
                                                     
                                                     ,No Postal Code,TV Land,ZZ,Cook,41.8864,-87.6382,";

        private LocalResourceZipCodeLoader _localResourceZipCodeReader;

        [OneTimeSetUp]
        public void Setup()
        {
            _localResourceZipCodeReader = new LocalResourceZipCodeLoader(_localResourceCsv, true);
        }

        [Test]
        public void Should_skip_header_line()
        {
            Assert.IsFalse(LoadZipCodes()
                .Any(z => z.PostalCode == "Postal Code"));
        }

        [Test]
        public void Should_skip_empty_lines()
        {
            Assert.IsFalse(LoadZipCodes()
                .Any(z => string.IsNullOrEmpty(z.PostalCode)));
        }

        [Test]
        public void Should_skip_lines_with_no_postal_code()
        {
            Assert.IsFalse(LoadZipCodes()
                .Any(z => z.PlaceName == "No Postal Code"));
        }

        [Test]
        public void Should_load_zip_codes()
        {
            var zipCodes = LoadZipCodes();
            var result = zipCodes.First();

            Assert.Equals(result.PostalCode, "12345");
            Assert.Equals(result.PlaceName, "Springfield");
            Assert.Equals(result.State, "TV Land");
            Assert.Equals(result.StateAbbreviation, "ZZ");
            Assert.Equals(result.County, "Cook");
            Assert.Equals(result.Latitude, 41.8868);
            Assert.Equals(result.Longitude, -87.6386);
        }

        private List<ZipCode> LoadZipCodes()
        {
            return _localResourceZipCodeReader.LoadZipCodes().ToList();
        }
    }
}
