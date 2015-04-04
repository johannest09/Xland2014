using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Xland.Services;
using Xland.Controllers;
using AutoMapper;
using Xland.Models;
using Xland.ViewModels;
using Xland.UnitOfWork;
using Xland.Repository;
using System.Configuration;

namespace Xland
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["XlandContext"].ConnectionString;
            var container = BuildUnityContainer(connectionstring);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer(string connectionstring)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>(); 



            container.RegisterType<IUnitOfWork, Xland.UnitOfWork.UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor(connectionstring));
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IStudioService, StudioService>();
            container.RegisterType<IPhotoService, PhotoService>();
            container.RegisterType<IPhotoGalleryService, PhotoGalleryService>();
            container.RegisterType<IVideoService, VideoService>();
            container.RegisterType<IVideoGalleryService, VideoGalleryService>();



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


            // CreateMap<Foo, Bar>().ForMember(x => x.Blarg, opt => opt.Ignore());
            Mapper.CreateMap<ProjectEditViewModel, Project>()
                .ForMember(x => x.Studios, opt => opt.Ignore())
                .ForMember(x => x.ProjectType, opt => opt.Ignore()
                );

            
        
        }
            

    }
}