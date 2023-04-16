using System.ComponentModel.DataAnnotations.Schema;

namespace EA.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Tag")]
    public class Tag
    {
        /// <summary>
        /// Tag Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tag Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tag Description
        /// </summary>
        public string Description { get; set; }

    }
}