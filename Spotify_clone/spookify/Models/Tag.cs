using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace spookify.Models
{
    public class Tag : BaseEntity
    {
        #region Properties
        public int TagId {get;set;}
        public string TagName { get; set; }
        // [Key]
        // public int UriId {get;set;}
        // public Uri Url { get; set; }

        public int? Count { get; set; }

        public string RelatedTo { get; set; }

        public bool? Streamable { get; set; }

        public Album album { get; set; }

        /// <summary>
        /// The number of users that have used this tag
        /// </summary>
        public int? Reach { get; set; }
        
        #endregion

        public Tag(string name)
        {
        }

        public Tag(string name, string uri, int? count = null)
        {
            TagName = name;
            // Url = new Uri(uri, UriKind.RelativeOrAbsolute);
            Count = count;
        }
    }
}