using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCDO
{
    [Serializable]
    public class CommonReturnCDO
    {
        public List<String> lstErrorMessage = new List<string>();
        public List<String> lstReturnString = new List<string>();

        public bool IsSuccess { get; set; }
        public CommonReturnCDO() { }
    }

    [Serializable]
    public class CommonModel
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
        public CommonModel() { }
    }
}
