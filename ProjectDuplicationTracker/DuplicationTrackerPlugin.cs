// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DuplicationTrackerPlugin.cs" company="">
//   
// </copyright>
// <summary>
//   The duplication tracker plugin.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ProjectDuplicationTracker
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using System.Windows.Controls;

    using ExtensionTypes;

    using VSSonarPlugins;

    /// <summary>
    /// The duplication tracker plugin.
    /// </summary>
    [Export(typeof(IPlugin))]
    public class DuplicationTrackerPlugin : IMenuCommandPlugin
    {
        /// <summary>
        /// The model.
        /// </summary>
        private ProjectDuplicationTrackerModel model;

        /// <summary>
        /// The get header.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHeader()
        {
            return "Duplications";
        }

        /// <summary>
        /// The get user control.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="vshelper">
        /// The vshelper.
        /// </param>
        /// <returns>
        /// The <see cref="UserControl"/>.
        /// </returns>
        public UserControl GetUserControl(ISonarConfiguration configuration, Resource project, IVsEnvironmentHelper vshelper)
        {
            this.InitModel(configuration, project, vshelper);
            
            var view = new DuplicationUserControl(this.model);
            return view;
        }

        /// <summary>
        /// The update configuration.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="vshelper">
        /// The vshelper.
        /// </param>
        public void UpdateConfiguration(ISonarConfiguration configuration, Resource project, IVsEnvironmentHelper vshelper)
        {
            this.InitModel(configuration, project, vshelper);

            this.model.UpdateConfiguration(configuration, project, vshelper);
        }

        public PluginDescription GetPluginDescription(IVsEnvironmentHelper vsinter)
        {
            var isEnabled = vsinter.ReadOptionFromApplicationData(GlobalIds.PluginEnabledControlId, "DuplicationsPlugin");

            var desc = new PluginDescription()
            {
                Description = "Duplications Plugin",
                Enabled = true,
                Name = "DuplicationsPlugin",
                SupportedExtensions = "*",
                Version = this.GetVersion()
            };

            if (string.IsNullOrEmpty(isEnabled))
            {
                desc.Enabled = true;
            }
            else if (isEnabled.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                desc.Enabled = true;
            }
            else
            {
                desc.Enabled = false;
            }

            return desc;
        }

        /// <summary>
        /// The init model.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="vshelper">
        /// The vshelper.
        /// </param>
        private void InitModel(ISonarConfiguration configuration, Resource project, IVsEnvironmentHelper vshelper)
        {
            if (this.model == null)
            {
                this.model = new ProjectDuplicationTrackerModel(configuration, vshelper);
                this.model.Login();
                this.model.SelectMainResource(project);
            }
        }

        public string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string GetAssemblyPath()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }
}
