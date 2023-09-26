using System;

namespace ProcsDLL.Models.Model
{
    public class Module : BaseEntity
    {
        public Int32 _MODULE_ID { get; set; }
        public String _MODULE_NM { get; set; }
        public String _MODULE_FOLDER { get; set; }
        public String _DATABASE_NAME { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}