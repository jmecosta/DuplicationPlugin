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
    using System.ComponentModel.Composition;
    using System.Windows.Controls;

    using ExtensionTypes;

    using VSSonarPlugins;

    /// <summary>
    /// The duplication tracker plugin.
    /// </summary>
    [Export(typeof(IMenuCommandPlugin))]
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
        public UserControl GetUserControl(ConnectionConfiguration configuration, Resource project, IVsEnvironmentHelper vshelper)
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
        public void UpdateConfiguration(ConnectionConfiguration configuration, Resource project, IVsEnvironmentHelper vshelper)
        {
            this.InitModel(configuration, project, vshelper);

            this.model.UpdateConfiguration(configuration, project, vshelper);
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
        private void InitModel(ConnectionConfiguration configuration, Resource project, IVsEnvironmentHelper vshelper)
        {
            if (this.model == null)
            {
                this.model = new ProjectDuplicationTrackerModel(configuration, vshelper);
                this.model.Login();
                this.model.SelectMainResource(project);
            }
        }
    }
}
