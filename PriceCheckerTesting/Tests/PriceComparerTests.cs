using System.Runtime.CompilerServices;
using PriceChecker.Helpers;
using PriceChecker.Models;
using PriceCheckerTesting.TestHelpers;
using Xunit;

namespace PriceCheckerTesting.Tests
{
    public class PriceComparerTests
    {
        PriceComparer? _comparer;
        const int _numberOfRecords=15;

        [Fact]
        public async Task ValuesParsedProperly()
        {
            _comparer = new PriceComparer(new TestHttpSnatcher(), new TestWebResourceFactory().GetDummyData(_numberOfRecords), new TestConsoleLogger());
            var determinedResources=(await _comparer.FetchAndComparePrices()).ToArray();
            Assert.Equal(_numberOfRecords, determinedResources.Count());
            for (int i = 0; i < determinedResources.Length; i++)
            {
                Assert.Equal(determinedResources[i].actualPrice, i);
                Assert.True(determinedResources[i].wasThePriceGoalHit);
                Assert.True(determinedResources[i] is ItemWebResource);
            }
        }

        [Fact]
        public async Task ValuesParsedPoorly()
        {
            _comparer = new PriceComparer(new TestHttpSnatcher(), new TestWebResourceFactory().GetFailedDummyData(_numberOfRecords), new TestConsoleLogger());
            var determinedResources = (await _comparer.FetchAndComparePrices()).ToArray();
            Assert.Equal(_numberOfRecords, determinedResources.Count());
            for (int i = 0; i < determinedResources.Length; i++)
            {
                Assert.Equal(0, determinedResources[i].actualPrice);
                Assert.False(determinedResources[i].wasThePriceGoalHit);
                Assert.True(determinedResources[i] is FailedItemWebResource);
            }

        }


    }
}
