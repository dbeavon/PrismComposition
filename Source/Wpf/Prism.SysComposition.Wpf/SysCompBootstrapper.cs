using System;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using Microsoft.Practices.ServiceLocation;

using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System.ComponentModel.Composition.Hosting;
using Prism.SysComposition.Modularity;
using System.Collections.Generic;
using System.Composition.Hosting.Core;
using Prism.SysComposition.Utility;

namespace Prism.SysComposition
{
    public abstract class Mef2Bootstrapper : Bootstrapper
    {
        [Export]
        public CompositionHost Container { get; set; }


        /// <summary>
        /// During the configuration of the container we maintain this member
        /// in derived classes.
        /// </summary>
        protected ContainerConfiguration ContainerConfig { get; set; }

        protected ConventionBuilder Builder { set; get; }

        protected Mef2Bootstrapper()
        {
            ContainerConfig = new ContainerConfiguration();
            ConfigureConventions();

            ContainerConfig.WithParts(new List<Type>()
            {
                //typeof(EmptyLogger),
                //typeof(Prism.SysComposition.Modularity.DebugDkb.ModuleCatalog),

                typeof(Mef2ServiceLocator),
                typeof(RegionAdapterMappings),

                typeof(SelectorRegionAdapter),
                typeof(ItemsControlRegionAdapter),
                typeof(ContentControlRegionAdapter),
                typeof(RegionManager),
                typeof(EventAggregator),
                typeof(RegionNavigationJournalEntry),
                typeof(RegionViewRegistry),
                typeof(RegionNavigationJournal),
                typeof(RegionNavigationContentLoader),
                typeof(RegionBehaviorFactory),

                typeof(SysCompModuleManager),
                typeof(SysCompModuleInitializer),

                typeof(AutoPopulateRegionBehavior),
                typeof(BindRegionContextToDependencyObjectBehavior),
                typeof(ClearChildViewsRegionBehavior),
                typeof(DelayedRegionCreationBehavior),
                typeof(RegionActiveAwareBehavior),
                typeof(RegionManagerRegistrationBehavior),
                typeof(RegionMemberLifetimeBehavior),
                typeof(SelectorItemsSourceSyncBehavior),
                typeof(SyncRegionContextWithHostBehavior),

                typeof(SysCompFileModuleTypeLoader),
                

            }, Builder);

            //ContainerConfig.WithAssemblies(AppDomain.CurrentDomain.GetAssemblies(), Builder);
        }

        protected override void InitializeModules()
        {
            this.Container.GetExport<IModuleManager>().Run();
        }

        protected virtual void ConfigureConventions()
        {
            Builder = new ConventionBuilder();

            //Builder.ForType<EmptyLogger>().ExportInterfaces().Shared();

            //Builder.ForType<Prism.SysComposition.Modularity.DebugDkb.ModuleCatalog>().ExportInterfaces().Shared();

            Builder.ForType<Mef2ServiceLocator>().ExportInterfaces().Shared();
            Builder.ForType<RegionAdapterMappings>().Export().Shared();

            Builder.ForType<SelectorRegionAdapter>().Export().ExportInterfaces().Shared();
            Builder.ForType<ItemsControlRegionAdapter>().Export().ExportInterfaces().Shared();
            Builder.ForType<ContentControlRegionAdapter>().Export().ExportInterfaces().Shared();

            Builder.ForType<RegionManager>().Export().ExportInterfaces().Shared();
            Builder.ForType<EventAggregator>().ExportInterfaces().Shared();
            Builder.ForType<RegionNavigationJournalEntry>().ExportInterfaces().Shared();
            Builder.ForType<RegionViewRegistry>().ExportInterfaces().Shared();
            Builder.ForType<RegionNavigationJournal>().ExportInterfaces().Shared();
            Builder.ForType<RegionNavigationContentLoader>().ExportInterfaces().Shared();
            Builder.ForType<RegionBehaviorFactory>().ExportInterfaces().Shared();

            Builder.ForType<SysCompModuleManager>().ExportInterfaces().Shared();
            Builder.ForType<SysCompModuleInitializer>().ExportInterfaces().Shared();

            
            Builder.ForType<AutoPopulateRegionBehavior>().Export().ExportInterfaces();
            Builder.ForType<BindRegionContextToDependencyObjectBehavior>().Export().ExportInterfaces();
            Builder.ForType<ClearChildViewsRegionBehavior>().Export().ExportInterfaces();
            Builder.ForType<DelayedRegionCreationBehavior>().Export().ExportInterfaces();
            Builder.ForType<RegionActiveAwareBehavior>().Export().ExportInterfaces();
            Builder.ForType<RegionManagerRegistrationBehavior>().Export().ExportInterfaces();
            Builder.ForType<RegionMemberLifetimeBehavior>().Export().ExportInterfaces();
            Builder.ForType<SelectorItemsSourceSyncBehavior>().Export().ExportInterfaces();
            Builder.ForType<SyncRegionContextWithHostBehavior>().Export().ExportInterfaces();

            // Not currently shared
            Builder.ForType<SysCompFileModuleTypeLoader>().Export().ExportInterfaces();

        }

        protected override void ConfigureServiceLocator()
        {
            var serviceLocator = Container.GetExport<IServiceLocator>();
            ServiceLocator.SetLocatorProvider((() => serviceLocator));
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var instance = Container.GetExport<RegionAdapterMappings>();
            instance.RegisterMapping(typeof(Selector), Container.GetExport<SelectorRegionAdapter>());
            instance.RegisterMapping(typeof(ItemsControl), Container.GetExport<ItemsControlRegionAdapter>());
            instance.RegisterMapping(typeof(ContentControl), Container.GetExport<ContentControlRegionAdapter>());
            return instance;
        }


        /// <summary>
        /// Return an instance provider to be used in container configuration.
        /// </summary>
        /// <returns>The provider</returns>
        public static ExportDescriptorProvider NewInstanceProvider<N>(N instance)
        {
            return new BaseInstanceExportDescriptorProvider<N>((object)instance);

        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            // Continue to adjust config
            // (replaces "ConfigureContainer")
            AdjustContainerConfiguration();

            this.ModuleCatalog = this.CreateModuleCatalog();
            this.ConfigureModuleCatalog();



            ConventionBuilder Builder = new ConventionBuilder();

            // Types - wierd
            //Builder.for.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Windows.EditCutSheetWindow>().Export();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.OneTimeProduced.OneTimeProducedWindow.ViewModel>().Export();
            //Builder.ForType<UFP.LumberTrack.ViewModeling.Production.GenScheduling.Sources.ViewerCustomization.ViewModelDependencies>().Export();

            //// Interfaces
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.AppLaunchers.InventoryControlLauncher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Windows.EditCutSheetWindow.Launcher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Master.Windows.EditMasterRunWindow.Launcher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.Windows.ItemEditDialogLauncher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.ReasonableExpectations.Launchers.BlackBoxRunLauncher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.ReasonableExpectations.Launchers.BatchManagementLauncher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.ApplyBurden.Launchers.BatchManagementLauncher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.ApplyBurden.Launchers.BlackBoxRunLauncher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.MiscellaneousDisplay.ExcessiveTimeWarningDialog.Launcher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.Items.Windows.PromptGlobalPlugDialog.Launcher>().ExportInterfaces();
            //Builder.ForType<UFP.LumberTrack.Production.Presentation.BlackBox.MarkupHints.Windows.SequenceHintWindow.Launcher>().ExportInterfaces();


            // Create the configuration.
            ContainerConfig.WithProvider(NewInstanceProvider<IModuleCatalog>(this.ModuleCatalog));


        ///// <summary>
        ///// Helper method for configuring the <see cref="CompositionContainer"/>. 
        ///// Registers all the types direct instantiated by the bootstrapper with the container.
        ///// </summary>
        //protected virtual void RegisterBootstrapperProvidedTypes()
        //{
        //    this.Container.ComposeExportedValue<ILoggerFacade>(this.Logger);
        //    this.Container.ComposeExportedValue<IModuleCatalog>(this.ModuleCatalog);
        //    this.Container.ComposeExportedValue<IServiceLocator>(new MefServiceLocatorAdapter(this.Container));
        //    this.Container.ComposeExportedValue<AggregateCatalog>(this.AggregateCatalog);
        //}



            //this.Logger.Log(Resources.CreatingModuleCatalog, Category.Debug, Priority.Low);
            //this.ModuleCatalog = this.CreateModuleCatalog();
            //if (this.ModuleCatalog == null)
            //{
            //    throw new InvalidOperationException(Resources.NullModuleCatalogException);
            //}






            this.Container = ContainerConfig.CreateContainer();

            Mef2ServiceLocator.Host = Container;

            this.ConfigureServiceLocator();

            this.ConfigureViewModelLocator();
            this.ConfigureRegionAdapterMappings();
            this.ConfigureDefaultRegionBehaviors();
            this.RegisterFrameworkExceptionTypes();

            this.Shell = this.CreateShell();

            if (this.Shell != null)
            {
                RegionManager.SetRegionManager(this.Shell, this.Container.GetExport<IRegionManager>());      
                RegionManager.UpdateRegions();
                this.InitializeShell();
            }

            // Test the 
            IModuleManager ManagerObj = Container.GetExport<IModuleManager>();


            // Not working.  Not resolving "ImportedModules" property.
            Container.SatisfyImports(Container.GetExport<IModuleInitializer>());

            // Not working either.  _mefFileModuleTypeLoader wasn't resolved.
            Container.SatisfyImports(Container.GetExport<IModuleManager>() );

            var exports = Container.GetExports<IModuleManager>();
            if (exports != null && exports.Any())
            {
                this.InitializeModules();
            }

            //var modules = Container.GetExports<IModule>();
            //foreach (var module in modules)
            //{
            //    module.Initialize();
            //}
        }










        /// <summary>
        /// Configures the <see cref="AggregateCatalog"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new AggregateCatalog.
        /// </remarks>
        /// <returns>An <see cref="AggregateCatalog"/> to be used by the bootstrapper.</returns>
        [Obsolete(message: "Not used in MEF2", error: false)]
        protected virtual AggregateCatalog CreateAggregateCatalog()
        {
            return null;
        }

        /// <summary>
        /// Configures the <see cref="AggregateCatalog"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>
        [Obsolete(message: "Not used in MEF2", error: false)]
        protected virtual void ConfigureAggregateCatalog()
        {
        }

        [Obsolete(message: "Not used in MEF2", error: false)]
        public AggregateCatalog AggregateCatalog { get {  return null; } }

        /// <summary>
        /// Configures the <see cref="CompositionContainer"/>. 
        /// May be overwritten in a derived class to add specific type mappings required by the application.
        /// </summary>
        /// <remarks>
        /// The base implementation registers all the types direct instantiated by the bootstrapper with the container.
        /// If the method is overwritten, the new implementation should call the base class version.
        /// </remarks>
        [Obsolete(message: "Not used in MEF2. See AdjustContainerConfiguration()", error: false)]
        protected virtual void ConfigureContainer()
        {
            this.RegisterBootstrapperProvidedTypes();
        }

        /// <summary>
        /// As part of the running of the bootstrapper, 
        /// we can update the configuration.
        /// </summary>
        protected virtual void AdjustContainerConfiguration()
        {
        }

        /// <summary>
        /// Helper method for configuring the <see cref="CompositionContainer"/>. 
        /// Registers all the types direct instantiated by the bootstrapper with the container.
        /// </summary>
        [Obsolete(message: "Not used in MEF2", error: false)]
        protected virtual void RegisterBootstrapperProvidedTypes()
        {
            
        }
    }
}
