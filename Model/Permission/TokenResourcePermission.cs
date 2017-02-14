using System;

namespace Model.Permission
{
    public class TokenResourcePermission
    {
        public TokenResourcePermission()
        { 
        }

        public TokenResourcePermission(TokenResourcePermission item)
        {
            Id = item.Id;
            Token = item.Token;
            TokenType = item.TokenType;
            ResourceId = item.ResourceId;
            ResourceType = item.ResourceType;
            Permission = item.Permission;
            CreateDate = item.CreateDate;
            ModifieDate = item.ModifieDate;
        }

        public int Id { get; set; }

        public int Token { get; set; }

        public TokenType TokenType { get; set; }

        public int ResourceId { get; set; }

        public ResourceType ResourceType { get; set; }

        public int Permission { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifieDate { get; set; }
    }
}
