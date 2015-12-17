﻿using IdentityServer4.Core.Extensions;
using IdentityServer4.Core.Logging;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer4.Core.Validation
{
    internal class SecretValidator
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<ISecretValidator> _validators;

        public SecretValidator(IEnumerable<ISecretValidator> validators, ILogger<SecretValidator> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<SecretValidationResult> ValidateAsync(ParsedSecret parsedSecret, IEnumerable<Secret> secrets)
        {
            var expiredSecrets = secrets.Where(s => s.Expiration.HasExpired());
            if (expiredSecrets.Any())
            {
                expiredSecrets.ToList().ForEach(
                    ex => _logger.LogInformation("Secret [{description}] is expired", ex.Description ?? "no description"));
            }

            var currentSecrets = secrets.Where(s => !s.Expiration.HasExpired());

            // see if a registered validator can validate the secret
            foreach (var validator in _validators)
            {
                var secretValidationResult = await validator.ValidateAsync(currentSecrets, parsedSecret);

                if (secretValidationResult.Success)
                {
                    _logger.LogVerbose("Secret validator success: {0}", validator.GetType().Name);
                    return secretValidationResult;
                }
            }

            _logger.LogInformation("Secret validators could not validate secret");
            return new SecretValidationResult { Success = false };
        }
    }
}