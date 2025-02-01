using System.Linq.Expressions;
using AutoMapper;
using Logiware.Application.Helpers.Profiles;

namespace Logiware.Application.Helpers.Mapper;

public class ShipmentMapper : IShipmentMapper
{
    private readonly IMapper _mapper;

    public ShipmentMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
   public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, sourceType, destinationType);
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, destination, sourceType, destinationType);
    }

    public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
    {
        return _mapper.Map(source, opts);
    }

    public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        return _mapper.Map(source, opts);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        return _mapper.Map(source, destination, opts);
    }

    public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
    {
        return _mapper.Map(source, sourceType, destinationType, opts);
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
    {
        return _mapper.Map(source, destination, sourceType, destinationType, opts);
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
    {
        return _mapper.ProjectTo(source, parameters, membersToExpand);
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
    {
        return _mapper.ProjectTo<TDestination>(source, parameters, membersToExpand);
    }

    public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object> parameters = null, params string[] membersToExpand)
    {
        return _mapper.ProjectTo(source, destinationType, parameters, membersToExpand);
    }

    public IConfigurationProvider ConfigurationProvider => _mapper.ConfigurationProvider;
}