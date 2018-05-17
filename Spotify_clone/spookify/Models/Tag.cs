using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace spookify.Models
{
    public class Tag : BaseEntity
    {

        public int TagId {get;set;}
        public string TagName { get; set; }

        public string TagURL { get; set; }

        public int? TagReach { get; set; }

        public int? TagTaggings { get; set; }

        public int? Count { get; set; }

        public string RelatedTo { get; set; }

        public bool? Streamable { get; set; }

        public Album album { get; set; }

    }
}