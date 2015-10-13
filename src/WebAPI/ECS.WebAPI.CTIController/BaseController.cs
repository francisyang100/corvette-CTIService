using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using log4net;
using ECS.Common.Logging;
using System.Reflection;
using ECS.Common.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ECS.WebAPI.CTIController
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {

        }
    }
}
