namespace ProjectDuplicationTracker
{
    using ExtensionTypes;

    /// <summary>
    /// The project file.
    /// </summary>
    public class ProjectFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFile"/> class.
        /// </summary>
        /// <param name="resource">
        /// The resource.
        /// </param>
        public ProjectFile(Resource resource)
        {
            this.Resource = resource;
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        public Resource Resource { get; private set; }

        /// <summary>
        /// Gets or sets the source code.
        /// </summary>
        public Source SourceCode { get; set; }
    }
}