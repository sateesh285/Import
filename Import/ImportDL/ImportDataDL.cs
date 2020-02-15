using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportInterface;
using ImportCDO;
using System.Data;
using System.Data.SqlClient;
namespace ImportDL
{
    public class ImportDataDL:BaseData,ImportInterfaceFile
    {
        public ImportCDO.CommonReturnCDO InsertDate(ImportCDO.CommonModel commonPassParameter)
        {
            CommonReturnCDO objReturnCDO = new CommonReturnCDO();
            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            ImportFileCDO objcdo = null;
            try
            {

                objcdo = (ImportFileCDO)commonPassParameter;
                cmd = GetCommand();
                cmd.CommandText = "USPInsertImport";
                cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("StoreName",objcdo.StoreName);
                    cmd.Parameters.AddWithValue("SourcePath", objcdo.SourcePath);
                    cmd.Parameters.AddWithValue("CreatedBy", objcdo.CreatedBy);
                    cmd.Parameters.AddWithValue("CreatedOn", objcdo.CreatedDate);
                    cmd.Parameters.AddWithValue("IsActive", objcdo.IsActive);
                    cmd.ExecuteNonQuery();
                    foreach (var importfiledata in objcdo.importfilecdo)
                    {
                        cmd.CommandText = "USPInsertImportData";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("Name", importfiledata.Name);
                        cmd.Parameters.AddWithValue("TwitterId", importfiledata.TwitterId);
                        cmd.Parameters.AddWithValue("CategoryGUID", importfiledata.CategoryGUID);
                        cmd.ExecuteNonQuery();

                        foreach (var category in importfiledata.Categories)
                        {
                            cmd.CommandText = "USPInsertImportDataCategory";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("Name", category);
                        cmd.Parameters.AddWithValue("@CategoryGUID", importfiledata.CategoryGUID);
                
                        cmd.ExecuteNonQuery();

                            
                        }
                    }


                    cmd.Transaction.Commit();
                    objReturnCDO.IsSuccess = true;
                    return objReturnCDO;

            }
            catch (Exception ex)
            {

                return objReturnCDO;
            }
            finally { cmd.Connection.Close();
            }
            
        }

        public ImportCDO.CommonReturnCDO SelectAll(ImportCDO.CommonModel commonPassParameter)
        {
            throw new NotImplementedException();
        }

        public ImportCDO.CommonReturnCDO UpdateData(ImportCDO.CommonModel commonPassParameter)
        {
            throw new NotImplementedException();
        }

        public ImportCDO.CommonReturnCDO DeleteData(ImportCDO.CommonModel commonPassParameter)
        {
            throw new NotImplementedException();
        }
    }
}
