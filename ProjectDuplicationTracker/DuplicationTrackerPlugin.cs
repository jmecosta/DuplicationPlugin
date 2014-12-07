// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DuplicationTrackerPlugin.cs" company="Copyright © 2014 jmecsoftware">
//     Copyright (C) 2014 [jmecsoftware, jmecsoftware2014@tekla.com]
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
// of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details. 
// You should have received a copy of the GNU Lesser General Public License along with this program; if not, write to the Free
// Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// --------------------------------------------------------------------------------------------------------------------

namespace ProjectDuplicationTracker
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Media;

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

        public PluginDescription GetPluginDescription(IConfigurationHelper vsinter)
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
        /// The update theme.
        /// </summary>
        /// <param name="backgroundColor">
        /// The background color.
        /// </param>
        /// <param name="foregroundColor">
        /// The foreground color.
        /// </param>
        public void UpdateTheme(Color backgroundColor, Color foregroundColor)
        {
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
