﻿using System;
using System.Net;
using System.Threading.Tasks;
using Biplov.Common.Core;
using Biplov.RiskAnalysis;

namespace Biplov.MockRiskAnalysis
{
    /// <summary>
    /// Mock risk analysis
    /// </summary>
    public class MockRiskAnalysisService : IRiskAnalysisService
    {
        public Task<Result<RiskAssessmentResult>> GetRiskAnalysis(IPAddress ipAddress)
        {
            var random = new Random();
            var probability = random.Next(100);

            // Return true with 99 percent probability
            var isRiskFree = probability <= 95;

            if (!isRiskFree) return Task.FromResult(Result.Fail<RiskAssessmentResult>("fraudulent activity detected"));
            var riskAssessmentResult = new RiskAssessmentResult(false, string.Empty);

            return Task.FromResult(Result.Ok(riskAssessmentResult));
        }
    }
}
