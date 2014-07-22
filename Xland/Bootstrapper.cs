using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Xland.Services;
using Xland.Controllers;
using AutoMapper;
using Xland.Models;
using Xland.ViewModels;

namespace Xland
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {

            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>(); 
      
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IStudioService, StudioService>();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
    
        }

        public static void ConfigureAutoMapper()
        {


            Mapper.CreateMap<Project, ProjectIndexViewModel>()
                .ForMember(x => x.Title, o => o.MapFrom(s => s.Title))
                .ForMember(x => x.ID, o => o.MapFrom(s => s.ID));

            Mapper.CreateMap<ProjectEditViewModel, Project>();
        
        }
            

    }
}