using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mapper
{
    // Implementation of AutoMapper for class libraries -> https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
    public static class OrderMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
         {
             var config = new MapperConfiguration(cfg =>
              {
                  cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                  cfg.AddProfile<OrderMappingProfile>();
              });
             var mapper = config.CreateMapper();
             return mapper;
         });
        public static IMapper Mapper => Lazy.Value;
    }
}
