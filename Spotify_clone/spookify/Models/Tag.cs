using System;
using Newtonsoft.Json.Linq;

namespace spookify.Models
{
    public class Tag : BaseEntity
    {
        #region Properties

        public string Name { get; set; }
        
        public Uri Url { get; set; }

        public int? Count { get; set; }

        public string RelatedTo { get; set; }

        public bool? Streamable { get; set; }

        /// <summary>
        /// The number of users that have used this tag
        /// </summary>
        public int? Reach { get; set; }
        
        #endregion

        public Tag()
        {
        }

        public Tag(string name, string uri, int? count = null)
        {
            Name = name;
            Url = new Uri(uri, UriKind.RelativeOrAbsolute);
            Count = count;
        }

        internal static Tag ParseJToken(JToken token)
        {
            return ParseJToken(token, null);
        }

        internal static Tag ParseJToken(JToken token, string relatedTag)
        {
            var name = token.Value<string>("name");
            var url = token.Value<string>("url");

            int? count = null;
            var countToken = token.SelectToken("count") ?? token.SelectToken("taggings");
            if (countToken != null)
            {
                count = countToken.ToObject<int?>();
            }

            bool? streamable = null;
            var streamableToken = token.SelectToken("streamable");
            if (streamableToken != null)
            {
                streamable = Convert.ToBoolean(streamableToken.Value<int>());
            }

            int? reach = null;
            var reachToken = token.SelectToken("reach");
            if (reachToken != null)
            {
                reach = reachToken.ToObject<int?>();
            }

            return new Tag(name, url, count)
            {
                Streamable = streamable,
                RelatedTo = relatedTag,
                Reach = reach
            };
        }
    }
}