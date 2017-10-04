using System;


using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;

using Prism.Modularity;


namespace Prism.SysComposition.Modularity
{
    /// <summary>
    /// An attribute that is applied to describe the Managed Extensibility Framework export of an IModule.
    /// </summary>    
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleExportAttribute : ExportAttribute, IModuleExport
    {


        /// <summary>
        /// Module export for System.Composition metadata views.
        /// </summary>
        public ModuleExportAttribute(IDictionary<string, object> metadata) : base(typeof(IModule))
        {
            if(!metadata.ContainsKey("ModuleType"))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if(!(metadata["ModuleType"] is Type))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if (!metadata.ContainsKey("ModuleName"))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if (!(metadata["ModuleName"] is string))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            // Initialize the module export attribute
            Type moduleType = (Type)metadata["ModuleType"];
            this.ModuleName = metadata["ModuleName"].ToString();
            this.ModuleType = moduleType;
            this.InitializationMode = InitializationMode.WhenAvailable;

            // Get the initialization mode if available.
            if (metadata.ContainsKey("InitializationMode"))
            {
                this.InitializationMode = (InitializationMode)metadata["InitializationMode"];
            }



        }

        /// <summary>
        /// Module export for System.Composition metadata views.
        /// </summary>
        public ModuleExportAttribute() : base(typeof(IModule)) { }
         
          

        /// <summary>
        /// Gets the contract name of the module.
        /// </summary>
        public string ModuleName { get;  set; }

        /// <summary>
        /// Gets concrete type of the module being exported. Not typeof(IModule).
        /// </summary>
        public Type ModuleType { get;  set; }

        /// <summary>
        /// Gets or sets when the module should have Initialize() called.
        /// </summary>
        public InitializationMode InitializationMode { get; set; }

        /// <summary>
        /// Gets or sets the contract names of modules this module depends upon.
        /// </summary>
        [CLSCompliant(false)] // Arrays as attribute arguments is not CLS-compliant.
        [System.ComponentModel.DefaultValue(new string[0])]
        public string[] DependsOnModuleNames { get; set; }

    }
}

