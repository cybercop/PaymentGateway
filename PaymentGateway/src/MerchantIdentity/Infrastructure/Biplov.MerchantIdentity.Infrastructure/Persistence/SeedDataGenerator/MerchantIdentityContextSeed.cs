﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biplov.MerchantIdentity.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Biplov.MerchantIdentity.Infrastructure.Persistence.SeedDataGenerator
{
    public class MerchantIdentityContextSeed
    {
        public async Task Seed(MerchantIdentityContext context, ILogger<MerchantIdentityContextSeed> logger, int? retry = 5)
        {
            var retryForAvailability = retry ?? 0;
            try
            {
                context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                await Task.Delay(2000);
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    logger.LogError(e, "EXCEPTION ERROR while migrating {DbContextName}", nameof(MerchantIdentityContext));
                    await Seed(context, logger, retryForAvailability);
                }
            }
        }

        private Merchant[] GetMerchants()
        {
            var merchant = new Merchant("Test Gmbh", "test@test.com", new List<string>
            {
                "EUR"
            });
            return new[] {merchant};
        }
    }
}
