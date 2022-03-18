using System;

namespace App.Database
{    
    public interface IBaseModel
    {
        int Id { get; set; }
        public string Name { get; set; }
        int Status { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        int CreateUser { get; set; }
        int? UpdateUser { get; set; }
    }
}
