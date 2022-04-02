using Keypass.Domain;
using Keypass.Repository.Interfaces;
using Keypass.State.Accounts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Keypass.Services.FileService
{
    public class FileExport : IFileExport
    {
        private string FILE_NAME = "keypass";

        private readonly IUnitOfWork unitOfWork;

        public FileExport(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void ExportToJson(string dir)
        {

            List<string> values = unitOfWork.Services
                                            .GetWithInclude()
                                            .Select(s => new Service 
                                            {
                                                Id = s.Id,
                                                Title = s.Title,
                                                Username = s.Username,
                                                Password = s.Password,
                                                Created = s.Created,
                                                Comment = s.Comment,
                                                Owner = s.Account.Username
                                            })
                                            .Select(s => JsonSerializer.Serialize<Service>(s))
                                            .ToList();

            WriteToFile(dir, "json", values);

        }

        public void ExportToTxt(string dir)
        {
            List<string> values = unitOfWork.Services
                                                .GetAll()
                                                .Select(s => FormatObjectToTxt(s))
                                                .ToList();

            WriteToFile(dir, "txt", values);


        }

        private void WriteToFile(string dir, string extension, List<string> values)
        {
            string filename = string.Join(".", FILE_NAME, extension);

            string fullpath = Path.Combine(dir, filename);

            FileStream fs = new FileStream(fullpath, FileMode.Create);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (string item in values)
                {
                    sw.WriteLine(item);
                }
            }

        }

        public void ExportToXml(string dir)
        {
            IEnumerable<Service> services = unitOfWork.Services.GetAll();

            XDocument xDocument = new XDocument();
            XElement xRoot = new XElement("Services");
            xDocument.Add(xRoot);

            foreach (Service item in services)
            {
                XElement xService = new XElement("Service");
                XAttribute idAttribute = new XAttribute("Id", item.Id);

                xService.Add(idAttribute);

                XElement xTitle = new XElement("Title", item.Title);
                XElement xUsername = new XElement("Username", item.Username);
                XElement xPassword = new XElement("Password", item.Password);
                XElement xCreated = new XElement("Created", item.Created);
                XElement xComment = new XElement("Comment", item.Comment);
                XElement xOwner = new XElement("Owner", item.Account.Username);

                xService.Add(xTitle);
                xService.Add(xUsername);
                xService.Add(xPassword);
                xService.Add(xCreated);
                xService.Add(xComment);
                xService.Add(xOwner);

                xRoot.Add(xService);

            }

            StringWriter sw = new StringWriter();
            xDocument.Save(sw);

            string filename = string.Join(".", FILE_NAME, "xml");

            string fullpath = Path.Combine(dir, filename);

            using (StreamWriter streamWriter = new StreamWriter(new FileStream(fullpath, FileMode.Create), Encoding.UTF8))
            {
                streamWriter.Write(sw);
            }

        }

        private string FormatObjectToTxt(Service service)
        {
            return string.Format("{0};" +
                                 "{1};" +
                                 "{2};" +
                                 "{3};" +
                                 "{4};" +
                                 "{5};" +
                                 "{6}",
                                 service.Id,
                                 service.Title,
                                 service.Username,
                                 service.Password,
                                 service.Created,
                                 service.Comment,
                                 service.Account.Username);
        }


    }
}
