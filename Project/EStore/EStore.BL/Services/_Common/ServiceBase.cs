using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoParse;
using EStore.DL.Mapping;
using Microsoft.AspNet.Identity;

namespace EStore.BL.Services._Common
{
    public abstract class ServiceBase
    {
        protected EStoreEntities Db;

        protected ServiceBase(EStoreEntities entities)
        {
            Db = entities;
        }

        
    }
}