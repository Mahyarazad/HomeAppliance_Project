using System;

namespace DM.Application.Contracts
{
    public class EndUserSearchModel
    {
        public int ProductId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}