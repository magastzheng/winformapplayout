using Model.Binding;
using Model.Constant;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class StockTemplate
    {
        public StockTemplate()
        { 
        }

        public StockTemplate(StockTemplate template)
        {
            this.TemplateId = template.TemplateId;
            this.TemplateName = template.TemplateName;
            this.FutureCopies = template.FutureCopies;
            this.MarketCapOpt = template.MarketCapOpt;
            this.Benchmark = template.Benchmark;
            this.EStatus = template.EStatus;
            this.EWeightType = template.EWeightType;
            this.EReplaceType = template.EReplaceType;
            this.DCreatedDate = template.DCreatedDate;
            this.DModifiedDate = template.DModifiedDate;
            this.CreatedUserId = template.CreatedUserId;
            this.CanEditUsers = template.CanEditUsers;
            this.CanViewUsers = template.CanViewUsers;
            this.Permissions = template.Permissions;
        }

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
            get { return DateFormat.Format(DCreatedDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("addedtime")]
        public string CreatedTime
        {
            get { return DateFormat.Format(DCreatedDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("modifieddate")]
        public string ModifiedDate 
        {
            get 
            {
                return DateFormat.Format(DModifiedDate, ConstVariable.DateFormat);
            }
        }

        [BindingAttribute("modifiedtime")]
        public string ModifiedTime
        {
            get
            {
                return DateFormat.Format(DModifiedDate, ConstVariable.TimeFormat);
            }
        }

        [BindingAttribute("editableuser")]
        public string EditableUser
        {
            get 
            {
                string str = string.Empty;
                if (CanEditUsers != null)
                {
                    var userNames = CanEditUsers.Select(p => p.Name).ToList();
                    str = string.Join(",", userNames);
                }

                return str;
            }
        }

        [BindingAttribute("viewuser")]
        public string ViewUser
        {
            get
            {
                string str = string.Empty;
                if (CanViewUsers != null)
                {
                    var userNames = CanViewUsers.Select(p => p.Name).ToList();
                    str = string.Join(",", userNames);
                }

                return str;
            }
        }

        public TemplateStatus EStatus { get; set; }

        public WeightType EWeightType { get; set; }

        public ReplaceType EReplaceType { get; set; }

        public DateTime DCreatedDate { get; set; }

        public DateTime DModifiedDate { get; set; }

        public int CreatedUserId { get; set; }

        public List<User> CanEditUsers { get; set; }

        public List<User> CanViewUsers { get; set; }

        public List<TokenResourcePermission> Permissions { get; set; }
    }
}
