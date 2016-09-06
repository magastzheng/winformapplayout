using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class StockTemplate
    {
        [BindingAttribute("templateid")]
        public int TemplateId { get; set; }

        [BindingAttribute("templatename")]
        public string TemplateName { get; set; }

        [BindingAttribute("status")]
        public string Status
        {
            get { return EnumTypeDisplayHelper.GetTemplateStatus(EStatus); }
        }

        [BindingAttribute("futurescopies")]
        public int FutureCopies { get; set; }

        [BindingAttribute("marketcapoption")]
        public double MarketCapOpt { get; set; }

        [BindingAttribute("benchmark")]
        public string Benchmark { get; set; }

        [BindingAttribute("weighttype")]
        public string WeightType
        {
            get { return EnumTypeDisplayHelper.GetWeightType(EWeightType); }
        }

        [BindingAttribute("replacetype")]
        public string ReplaceType 
        {
            get { return EnumTypeDisplayHelper.GetReplaceType(EReplaceType); } 
        }

        [BindingAttribute("addeddate")]
        public string CreatedDate 
        {
            get { return DCreatedDate.ToString("yyyy-MM-dd"); }
        }

        [BindingAttribute("addedtime")]
        public string CreatedTime
        {
            get { return DCreatedDate.ToString("hh:mm:ss"); }
        }

        [BindingAttribute("modifieddate")]
        public string ModifiedDate 
        {
            get { return DModifiedDate.ToString("yyyy-MM-dd"); }
        }

        [BindingAttribute("modifiedtime")]
        public string ModifiedTime
        {
            get { return DModifiedDate.ToString("hh:mm:ss"); }
        }

        public TemplateStatus EStatus { get; set; }

        public WeightType EWeightType { get; set; }

        public ReplaceType EReplaceType { get; set; }

        public DateTime DCreatedDate { get; set; }

        public DateTime DModifiedDate { get; set; }

        public int UserId { get; set; }
    }
}
