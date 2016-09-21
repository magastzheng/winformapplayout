using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database
{
    public class TemplateItem
    {
        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public int FutureCopies { get; set; }

        public double MarketCapOpt { get; set; }

        public string Benchmark { get; set; }

        public TemplateStatus EStatus { get; set; }

        public WeightType EWeightType { get; set; }

        public ReplaceType EReplaceType { get; set; }

        public DateTime DCreatedDate { get; set; }

        public DateTime DModifiedDate { get; set; }

        public int CreatedUserId { get; set; }
    }
}
