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

        public List<UserResourcePermission> Permissions { get; set; }
    }
}
