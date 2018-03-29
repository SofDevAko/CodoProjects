using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace spookify.Models
{
    public class Stats : BaseEntity
    {
        #region Properties

        public int Listeners { get; set; }
        public int Plays { get; set; }

        #endregion

        internal static Stats ParseJToken(JToken token)
        {
            var stats = new Stats
            {
                Listeners = token.Value<int>("listeners"),
                Plays = token.Value<int>("plays")
            };

            return stats;
        }
    }
}