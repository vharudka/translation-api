// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using System.Linq;

namespace Harudka.Translation.Api.Extentions
{
    public static class MappingExtentions
    {
        public static TDestination Map<TDestination>(this IMapper mapper, params object[] sources) where TDestination : new()
        {
            return Map(mapper, new TDestination(), sources);
        }

        private static TDestination Map<TDestination>(this IMapper mapper, TDestination destination, params object[] sources)
            where TDestination : new()
        {
            if(!sources.Any())
            {
                return destination;
            }

            foreach(var src in sources)
            {
                destination = mapper.Map(src, destination);
            }

            return destination;
        }
    }
}
