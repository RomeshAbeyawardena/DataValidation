using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace DataValidation.Mvc
{
    [ValidateModel]
    [ModelErrorOnArgumentException]
    public class BaseController : Controller
    {
        protected BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        protected IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        private readonly IMapper _mapper;
    }
}