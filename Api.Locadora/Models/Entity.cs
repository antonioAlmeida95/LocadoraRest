using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Locadora.Models.Interfaces;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Entity : IEntity
    {
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

        public void Atualizar(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-ddTH:mm:ss.fffK",
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            JsonConvert.PopulateObject(JsonConvert.SerializeObject(obj, settings), this);
        }
    }
}
