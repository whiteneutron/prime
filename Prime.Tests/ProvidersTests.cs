﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.AsyncEx;
using Prime.Common;

namespace Prime.Tests
{
    [TestClass]
    public class ProvidersTests
    {
        [TestMethod]
        public void GetPricesFromProvidersTest()
        {
            var ctx = new PublicPriceContext("BTC_USD".ToAssetPairRaw());

            foreach (var provider in Networks.I.Providers.OfType<IPublicPricingProvider>())
            {
                try
                {
                    var r = AsyncContext.Run(() => provider.GetPricingAsync(ctx)).FirstPrice;

                    Trace.WriteLine($"{r.QuoteAsset}: {r.Price.Display} - {provider.Network}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"{provider.Network} failed: {e.Message}");
                }
            }
        }
    }
}
