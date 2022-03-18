using App.Database;

namespace App.Models
{
    public class IdValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public IdValue(IBaseModel v)
        {
            Id = v.Id;
            Value = v.Name;
        }
    }
}
