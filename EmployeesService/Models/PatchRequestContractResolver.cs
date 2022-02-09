﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace EmployeesService.Models
{
    /// <summary>
    /// Class that plugs in to Newtonsoft deserialization pipeline for classes descending from <see cref="PatchDtoBase"/>.
    /// For all properties, that are present in JSON it calls <see cref="PatchDtoBase.SetHasProperty"/>.`
    /// </summary>
    public class PatchRequestContractResolver: DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop= base.CreateProperty(member, memberSerialization);

            prop.SetIsSpecified += (o, o1) =>
              {
                  if (o is PatchDtoBase patchDtoBase)
                  {
                      patchDtoBase.SetHasProp(prop.PropertyName);
                  }
              };
            
            return prop;
        }
    }
}
