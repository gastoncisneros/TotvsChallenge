using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Domain
{
    public class BaseResponse
    {
        [JsonIgnore]
        public bool IsSuccess { get; set; }
        public ErrorDS[] Errors { get; set; }
    }

    public class ErrorDS
    {
        public int ID { get; set; }
        public string Descr { get; set; }

        [JsonIgnore]
        public string Key { get; set; }

        [JsonIgnore]
        public string ParentKey { get; set; }

    }
}
