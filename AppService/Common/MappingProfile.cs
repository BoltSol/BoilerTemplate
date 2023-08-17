using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(t => t.GetCustomAttributes(typeof(AutoMapAttribute), false).Any()).ToList();

            foreach (var type in types)
            {
                var autoMapAttributes = type.GetCustomAttributes(typeof(AutoMapAttribute), false);
                foreach (var attribute in autoMapAttributes)
                {
                    var sourceType = (attribute as AutoMapAttribute)?.SourceType;
                    if (sourceType != null)
                    {
                        CreateMap(sourceType, type).ReverseMap(); // Use CreateMap from Profile
                    }
                }
            }
        }
    }
}
