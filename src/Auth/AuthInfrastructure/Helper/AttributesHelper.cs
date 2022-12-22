using AuthInfrastructure.DTOS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthInfrastructure.Helper
{
    public static class AttributesHelper
    {
        public static Dictionary<string, string> ExtractUserAttributes(string attributes) => JsonConvert.DeserializeObject<Dictionary<string, string>>(attributes);

        public static string SetAttributes(AddAttributeDTOS dtos, string attributes)
        {
            var json = JObject.Parse(attributes);

            foreach (var attribute in dtos.Attributes)
            {
                if (json.ContainsKey(attribute.Name))
                    json[attribute.Name] = attribute.Value;
                else 
                    json.Add(attribute.Name, attribute.Value);
            }

            return json.ToString();
        }

        public static string RemoveAttributes(RemoveAttributesDTOS dtos, string attributes)
        {
            string[] _attributesToRemove;
            var json = JObject.Parse(attributes);

            if (dtos.Attributes.Contains(','))
                _attributesToRemove = dtos.Attributes.Split(',');
            else
                _attributesToRemove = new[] { dtos.Attributes };

            foreach (var attribute in _attributesToRemove)
            {
                json.Remove(attribute);
            }

            return json.ToString();
        }
    }
}
