using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;

using System.Linq;


using Microsoft.Practices.ServiceLocation;

using Prism.SysComposition.Utility;

using Prism.Logging;
using Prism.Modularity;
using Prism.Events;
using Prism.Regions;
using Prism.Regions.Behaviors;

using Prism.SysComposition.Properties;
using Prism.SysComposition;
using Prism.SysComposition.Modularity;
using System.Reflection;


namespace Prism.SysComposition
{
    /// <summary>
    /// Base class that provides a basic bootstrapping sequence that registers most of the Prism Library assets in a System.Composition <see cref="CompositionHost"/>.
    /// </summary>
    /// <remarks>
    /// This class must be overridden to provide application specific configuration.
    /// </remarks>
    public abstract class SysCompBootstrapper : Bootstrapper
    {
        /// <summary>
        /// Gets or sets the default <see cref="CompositionHost"/> used while bootstrapping.
        /// This is not likely to be exposed as the application's IServiceLocator since it 
        /// contains only a minimal set of composable contracts. 
        /// </summary>
        /// <value>The default <see cref="CompositionHost"/> instance.</value>
        protected CompositionHost BootstrapperCompositionHost { get; set; }
         

        /// <summary>
        /// Construction of bootstrapper (config, etc).
        /// </summary>
        protected SysCompBootstrapper()
        {
            BootstrapperCompositionHost = null;

        }
         

         
         

        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        /// <param name="runWithDefaultConfiguration">If <see langword="true"/>, registers default 
        /// Prism Library services in the container. This is the default behavior.</param>
        public override void Run(bool runWithDefaultConfiguration)
        {
            this.Logger = this.CreateLogger();

            if (this.Logger == null)
            {
                throw new InvalidOperationException(Resources.NullLoggerFacadeException);
            }

            this.Logger.Log(Resources.LoggerWasCreatedSuccessfully, Category.Debug, Priority.Low);



            // Module catalog
            this.Logger.Log(Resources.CreatingModuleCatalog, Category.Debug, Priority.Low);
            this.ModuleCatalog = this.CreateModuleCatalog();
            if (this.ModuleCatalog == null)
            {
                throw new InvalidOperationException(Resources.NullModuleCatalogException);
            }

            // Configure the module catalog.
            this.Logger.Log(Resources.ConfiguringModuleCatalog, Category.Debug, Priority.Low);
            this.ConfigureModuleCatalog();



             


            // Create the container config now!
            ContainerConfiguration bootstrapperContainerConfig;
            this.Logger.Log(Resources.CreatingConfigurationForSysComp, Category.Debug, Priority.Low);
            bootstrapperContainerConfig = CreateSystemCompositionConfiguration();



            // Adjust configuration already
            // created in the constructor
            ConfigureSystemCompositionContainer(bootstrapperContainerConfig);
            this.Logger.Log(Resources.ContainerConfigurationWasAdjusted, Category.Debug, Priority.Low);

            // Register Prism default types if missing
            // (this ensures that all the "SysComp" exports are declared )
            this.RegisterDefaultTypesIfMissing(bootstrapperContainerConfig);

            // Create the container now!
            this.Logger.Log(Resources.CreatingContainerForSysComp, Category.Debug, Priority.Low);
            this.BootstrapperCompositionHost = this.CreateCompositionHost(bootstrapperContainerConfig);
            if (this.BootstrapperCompositionHost == null)
            {
                throw new InvalidOperationException(Resources.NullCompositionContainerException);
            }
                                                 


            this.Logger.Log(Resources.ConfiguringServiceLocatorSingleton, Category.Debug, Priority.Low);
            this.ConfigureServiceLocator();


            this.Logger.Log(Resources.ConfiguringViewModelLocator, Category.Debug, Priority.Low);
            this.ConfigureViewModelLocator();

            this.Logger.Log(Resources.ConfiguringRegionAdapters, Category.Debug, Priority.Low);
            this.ConfigureRegionAdapterMappings();

            this.Logger.Log(Resources.ConfiguringDefaultRegionBehaviors, Category.Debug, Priority.Low);
            this.ConfigureDefaultRegionBehaviors();

            this.Logger.Log(Resources.RegisteringFrameworkExceptionTypes, Category.Debug, Priority.Low);
            this.RegisterFrameworkExceptionTypes();

            this.Logger.Log(Resources.CreatingShell, Category.Debug, Priority.Low);
            this.Shell = this.CreateShell();
            if (this.Shell != null)
            {
                this.Logger.Log(Resources.SettingTheRegionManager, Category.Debug, Priority.Low);
                RegionManager.SetRegionManager(this.Shell, this.BootstrapperCompositionHost.GetExport<IRegionManager>());

                this.Logger.Log(Resources.UpdatingRegions, Category.Debug, Priority.Low);
                RegionManager.UpdateRegions();

                this.Logger.Log(Resources.InitializingShell, Category.Debug, Priority.Low);
                this.InitializeShell();
            }

                                     
            // Simple/default module manager support for now.
            IModuleManager managerExported = this.BootstrapperCompositionHost.GetExport<IModuleManager>();
            if ((managerExported != null) )
            {
                this.Logger.Log(Resources.InitializingModules, Category.Debug, Priority.Low);
                this.InitializeModules();
            }

            // Successfully completed bootstrapper
            this.Logger.Log(Resources.BootstrapperSequenceCompleted, Category.Debug, Priority.Low);
        }


        /// <summary>
        /// Configures the <see cref="ContainerConfiguration"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new ContainerConfiguration.
        /// </remarks>
        /// <returns>An <see cref="ContainerConfiguration"/> to be used by the bootstrapper.</returns>
        protected virtual ContainerConfiguration CreateSystemCompositionConfiguration()
        {
            return new ContainerConfiguration();
        }

        /// <summary>
        /// Helper method for configuring the <see cref="CompositionContainer"/>. 
        /// Registers defaults for all the types necessary for Prism to work, if they are not already registered.
        /// </summary>
        public virtual void RegisterDefaultTypesIfMissing(ContainerConfiguration containerConfiguration)
        {
            DefaultPrismServiceRegistrar.RegisterRequiredPrismServicesIfMissing(containerConfiguration);
        }


        /// <summary>
        /// Helper method for configuring the <see cref="CompositionContainer"/>. 
        /// Registers all the types direct instantiated by the bootstrapper with the container.
        /// </summary>
        protected virtual void RegisterBootstrapperProvidedTypes(ContainerConfiguration systemCompositionContainerConfig)
        {
            // Logger and module catalog should be ready by now.
            systemCompositionContainerConfig.WithExportInstance<ILoggerFacade>(this.Logger);
            systemCompositionContainerConfig.WithExportInstance<IModuleCatalog>(this.ModuleCatalog);

            // Define the service locator.  Will need composition host to be assigned at later time
            systemCompositionContainerConfig.WithExportInstance<IServiceLocator>(new SysCompServiceLocator());
        }

        /// <summary>
        /// As part of the running of the bootstrapper, 
        /// we can update the configuration.
        /// </summary>
        protected virtual void ConfigureSystemCompositionContainer(ContainerConfiguration systemCompositionContainerConfig)
        {
            RegisterBootstrapperProvidedTypes(systemCompositionContainerConfig);
        }

        /// <summary>
        /// Creates the <see cref="CompositionContainer"/> that will be used as the default container.
        /// </summary>
        /// <returns>A new instance of <see cref="CompositionContainer"/>.</returns>
        /// <remarks>
        /// The base implementation registers a default MEF catalog of exports of key Prism types.
        /// Exporting your own types will replace these defaults.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "The default export provider is in the container and disposed by MEF.")]
        protected virtual CompositionHost CreateCompositionHost(ContainerConfiguration systemCompositionContainerConfig)
        {
            return systemCompositionContainerConfig.CreateContainer(); 
        }

                   
        /// <summary>
        /// Configures the LocatorProvider for the <see cref="Microsoft.Practices.ServiceLocation.ServiceLocator" />.
        /// </summary>
        /// <remarks>
        /// The base implementation also sets the ServiceLocator provider singleton.
        /// </remarks> 
        protected override void ConfigureServiceLocator()
        {
            SysCompServiceLocator locatorObj = BootstrapperCompositionHost.GetExport<IServiceLocator>() as SysCompServiceLocator;

            locatorObj?.AssignCompositionHost(BootstrapperCompositionHost);

            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => { return BootstrapperCompositionHost.GetExport<IServiceLocator>(); }));
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell() {  }

        /// <summary>
        /// Initializes the modules. May be overwritten in a derived class to use a custom Modules Catalog
        /// </summary>
        protected override void InitializeModules()
        {
            IModuleManager manager = this.BootstrapperCompositionHost.GetExport<IModuleManager>();
            manager.Run();
        }
    }
}