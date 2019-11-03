using JdCat.Basketball.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdCat.Basketball.App.Controllers
{
    public abstract class BaseController<T>: ControllerBase where T: IBaseService
    {
        public T Service { get; }
        public BaseController(T service)
        {
            Service = service;
        }

    }
}
