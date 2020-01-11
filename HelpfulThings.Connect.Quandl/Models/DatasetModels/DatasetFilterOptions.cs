using System;
using HelpfulThings.Connect.Quandl.TypedEnums;

namespace HelpfulThings.Connect.Quandl.Models.DatasetModels
{
    public class DatasetFilterOptions
    {
        public int? Limit { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DatasetOrder Order { get; set; }
        public CollapseOption Collapse { get; set; }
        public TransformOption Transform { get; set; }

        public DatasetFilterOptions()
        {
            Limit = null;
            ColumnIndex = null;
            StartDate = null;
            EndDate = null;
            Order = null;
            Collapse = null;
            Transform = null;
        }
    }
}
