using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EStore.Web.Code
{
    public class CustomModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(
            ModelMetadata metadata,
            ControllerContext context,
            IEnumerable<Attribute> attributes)
        {
            return Enumerable.Empty<ModelValidator>();
        }
    }
}