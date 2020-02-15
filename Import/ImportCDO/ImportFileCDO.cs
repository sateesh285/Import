using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCDO
{
    [Serializable]
    public class ImportFileCDO:CommonModel
    {
        public string StoreName { get; set; }
        public string SourcePath { get; set; }
        public List<ImportFileDataCDO> importfilecdo = new List<ImportFileDataCDO>();
    }

    [Serializable]
    public class ImportFileDataCDO 
    {
        public string Name { get; set; }
        public string CategoryGUID { get; set; }
        public string TwitterId { get; set; }
        public List<String> Categories=new List<string> ();

    }
}
