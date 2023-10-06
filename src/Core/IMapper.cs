﻿namespace Core;
public interface IMapper<in TSource, out TDestination>
{
    public TDestination Map(TSource source);
}
