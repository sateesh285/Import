using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportCDO;

namespace ImportInterface
{
    public interface Itransaction
    {
        CommonReturnCDO InsertDate(CommonModel commonPassParameter);
        CommonReturnCDO SelectAll(CommonModel commonPassParameter);
        CommonReturnCDO UpdateData(CommonModel commonPassParameter);
        CommonReturnCDO DeleteData(CommonModel commonPassParameter); 
    }
}
