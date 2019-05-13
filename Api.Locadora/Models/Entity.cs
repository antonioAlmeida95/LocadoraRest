using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Locadora.Models.Interfaces;

namespace Api.Locadora.Models
{
    public class Entity : IEntity
    {
        public int Versao { get; set; } = 0;

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
    }
}
