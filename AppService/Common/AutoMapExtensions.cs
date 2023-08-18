using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AppService.Common
{
    public static class AutoMapExtensions
    {
        private static IMapper _mapper;
        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }
        public static TDestination MapTo<TDestination>(this object source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}
