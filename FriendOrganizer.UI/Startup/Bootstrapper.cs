using Autofac;
using FriendOrganizer.Infra.DataAccess;
using FriendOrganizer.Infra.DataAccess.DataAccess.Lookps;
using FriendOrganizer.Infra.DataAccess.DataAccess.Repositories;
using FriendOrganizer.UI.Services;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Startup
{
    /// <summary>
    /// Autofac Bootstrapper, to set up services
    /// </summary>
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
          
            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            //This is the scoped.
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
