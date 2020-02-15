using ImportBL;

using ImportCDO;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;

using System;

using System.Collections.Generic;

using System.Configuration;

using System.IO;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Xml;

using YamlDotNet.Serialization;

 

namespace ConsoleApplication10

{

    public class Function

    {

        public List<ImportFileDataCDO> ConvertObject(dynamic jobject, List<ImportFileDataCDO> objcdo, bool isXml )

        {

            if (!isXml)

            {

                foreach (dynamic objdatacdo in jobject.Data)

                {

                    Console.WriteLine($"Importing Data {objdatacdo}");

                    ImportFileDataCDO objImportDataCDO = new ImportFileDataCDO();

                    objImportDataCDO.Name = objdatacdo.Name.ToString();

                    objImportDataCDO.TwitterId = objdatacdo.Twitter.ToString();

                    string CategoriesGUID = Guid.NewGuid().ToString();

                    objImportDataCDO.CategoryGUID = CategoriesGUID;

                    string[] array = objdatacdo.Categories.ToString().Split(',');

                    List<string> Categories = new List<string>(array);

                    objImportDataCDO.Categories = Categories;

                    objcdo.Add(objImportDataCDO);

                }

            }

            else

            {

                foreach (dynamic objdatacdo in jobject.Data.element)

                {

                    Console.WriteLine($"Importing Data {objdatacdo}");

                    ImportFileDataCDO objImportDataCDO = new ImportFileDataCDO();

                    objImportDataCDO.Name = objdatacdo.Name.ToString();

                    objImportDataCDO.TwitterId = objdatacdo.Twitter.ToString();

                    string CategoriesGUID = Guid.NewGuid().ToString();

                    objImportDataCDO.CategoryGUID = CategoriesGUID;

                    string[] array = objdatacdo.Categories.ToString().Split(',');

                    List<string> Categories = new List<string>(array);

                    objImportDataCDO.Categories = Categories;

                    objcdo.Add(objImportDataCDO);

                }

            }

            return objcdo;

        }

    }

 

    class Import

    {

        static void Main(string[] args)

        {

            dynamic jobject;

            ImportFileCDO objCDO = new ImportFileCDO();

            ImportDataBL objBl = new ImportDataBL();

            // Test if input arguments were supplied.

            if (args.Length < 2)

            {

                Console.WriteLine("Please enter import param in below format.");

                Console.WriteLine("Usage: import <store> <filePath>");

                return;

            }

            Console.WriteLine($"The input value is StoreName {args[0]} and  File Path - {args[1]}");

           

            string storePath = ConfigurationManager.AppSettings["LocalPath"].ToString();

            //string FullPath = @"C:\Store\Capteraa\Data.yaml";

            string FullPath = storePath + "/" + args[0].ToString() + "/" + args[1].ToString();

            objCDO.StoreName = args[0].ToString();

            objCDO.SourcePath = FullPath;

            objCDO.IsActive = true;

            objCDO.CreatedBy = "Import Tool";

            objCDO.CreatedDate = DateTime.Now;

            Function classobj = new Function();

            if (File.Exists(FullPath))

            {

                if (FullPath.ToUpper().Contains(".JSON"))

                {

                    string data = File.ReadAllText(FullPath);

                    jobject = JObject.Parse(data);

                    List<ImportFileDataCDO> importfilecdo = new List<ImportFileDataCDO>();

                    importfilecdo = classobj.ConvertObject(jobject, importfilecdo, false);                

                    objCDO.importfilecdo = importfilecdo;

                }

                else if (FullPath.ToUpper().Contains(".XML"))

                {

                    string data = File.ReadAllText(FullPath);

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(data);

                    string jsonText = JsonConvert.SerializeXmlNode(doc);

                    jobject = JObject.Parse(jsonText);

                    List<ImportFileDataCDO> importfilecdo = new List<ImportFileDataCDO>();

                    importfilecdo = classobj.ConvertObject(jobject, importfilecdo, true);

                    objCDO.importfilecdo = importfilecdo;

                }

                else if (FullPath.ToUpper().Contains(".YAML"))

                {

                    List<ImportFileDataCDO> importfilecdo = new List<ImportFileDataCDO>();

                    string data = File.ReadAllText(FullPath);

                    var r = new StringReader(data);

                    var deserializer = new Deserializer();

                    dynamic yamlObject = deserializer.Deserialize(r);                

                    var serializer = new Newtonsoft.Json.JsonSerializer();

                    serializer.Serialize(Console.Out, yamlObject);

                    string jsonText = JsonConvert.SerializeObject(yamlObject);

                    jobject = JObject.Parse(jsonText);

                    importfilecdo = classobj.ConvertObject(jobject, importfilecdo, false);

                    objCDO.importfilecdo = importfilecdo;

                }

                else if (FullPath.ToUpper().Contains(".CSV"))

                {

                  

                }

 

                    //Comment out if db is not active

                   // objBl.InsertData((CommonModel)objCDO); 

            }

            else

            {

                Console.WriteLine($"Please check the file if you have placed in the directory {FullPath}");

            }

 

        }

    }

}

 