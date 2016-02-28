using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using EStore.BL.Models._Common;
using ValidationException = EStore.BL.Exceptions.ValidationException;

namespace EStore.BL.Extensions
{
    public static class ValidationExtensions
    {
        public static List<DbValidationError> Validate<T>(this T entity,
            params Expression<Func<T, IEnumerable<ViewModelBase>>>[] childEntitiesAction) where T : ViewModelBase
        {
            var validationErrors = new List<DbValidationError>();
            validationErrors.AddRange(entity.GetValidationErrors());

            foreach (var action in childEntitiesAction)
            {
                var childErrors = action.Compile()
                    .Invoke(entity)
                    .SelectMany(x => x.Validate());
                validationErrors.AddRange(childErrors);
            }
            return validationErrors;
        }

        public static List<DbValidationError> GetValidationErrors<T>(this T entityBase, params Expression<Func<T, object>>[] propertiesToValidate)
            where T : ViewModelBase
        {
            return (propertiesToValidate.Length != 0
                ? GetErrorsForProperties(entityBase, propertiesToValidate)
                : GetErrorsForObject(entityBase)).ToList();
        }

        private static IEnumerable<DbValidationError> GetErrorsForObject<T>(this T entityBase) where T : class
        {
            var validationContext = new ValidationContext(entityBase, null, null);
            var errors = new List<ValidationResult>();
            Validator.TryValidateObject(entityBase, validationContext, errors, true);
            return errors.SelectMany(x => x.MemberNames.Select(m => new DbValidationError(m, x.ErrorMessage)));
        }

        private static IEnumerable<DbValidationError> GetErrorsForProperties<T>(this T entityBase,
            params Expression<Func<T, object>>[] propertiesToValidate) where T : ViewModelBase
        {
            var validationErrors = new List<DbValidationError>();
            foreach (var action in propertiesToValidate)
            {
                var propertyName = ((MemberExpression) action.Body).Member.Name;
                var propertyValue = action.Compile().Invoke(entityBase);
                var validationContext = new ValidationContext(entityBase)
                {
                    MemberName = propertyName
                };
                var errors = new List<ValidationResult>();
                Validator.TryValidateProperty(propertyValue, validationContext, errors);
                validationErrors.AddRange(errors.Select(x => new DbValidationError(propertyName, x.ErrorMessage)));
            }
            return validationErrors;
        }

        public static void ThrowIfHasErrors(this IEnumerable<DbValidationError> errors)
        {
            var arrorsList = errors.ToList();
            if (arrorsList.Any())
                throw new ValidationException(arrorsList);
        }
    }
}