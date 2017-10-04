using System;
using System.Collections.Generic;
using System.Composition.Hosting;

using System.Reflection;

namespace Prism.SysComposition
{
    ///<summary>
    /// DefaultPrismServiceRegistrationAgent allows the Prism required types to be registered if necessary.
    ///</summary>
    public static class DefaultPrismServiceRegistrar
    {
        /// <summary>
        /// Registers the required Prism types that are not already registered in the <see cref="ContainerConfiguration"/>.
        /// </summary>
        ///<param name="containerConfiguration">The <see cref="ContainerConfiguration"/> to register the required types in, if they are not already registered.</param>
        public static void RegisterRequiredPrismServicesIfMissing(ContainerConfiguration containerConfiguration) 
        {
            containerConfiguration.WithAssembly(Assembly.GetExecutingAssembly());
        }
         
    }
}


