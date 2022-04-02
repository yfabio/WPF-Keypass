using Keypass.Domain;
using Keypass.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Keypass.Services.FileService
{
    public class FileImport : IFileImport
    {
        private readonly IUnitOfWork unitOfWork;

        public FileImport(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
         
        
        public void ImportToJson(string file)
        {         

            List<Service> services = new List<Service>();

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);

            using (StreamReader reader = new StreamReader(fs))
            {
                string json = string.Empty;
                while ((json = reader.ReadLine()) != null) 
                {
                   Service service = (Service) JsonSerializer.Deserialize(json,typeof(Service));
                   services.Add(service);                    
                }                
            }

            unitOfWork.Services.AddRange(services);
            unitOfWork.Save();
        }

        public void ImportToTxt(string file)
        {

            List<Service> services = new List<Service>();

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);

            string line = string.Empty;

            using (StreamReader reader = new StreamReader(fs))
            {
                while ((line = reader.ReadLine()) !=null) 
                {
                    Service service = GetServiceFromText(line);
                    services.Add(service);
                }
            }

            services = ArrangeServices(services);
            unitOfWork.Services.AddRange(services);
            unitOfWork.Save();

        }

        private Service GetServiceFromText(string line)
        {
            string[] values = line.Split(";");
            return new Service
            {
                Id = int.Parse(values[0]),
                Title = values[1],
                Username = values[2],
                Password = values[3],
                Created = DateTime.Parse(values[4]),
                Comment = values[5],
                Owner = values[6]
            };
        }

        public void ImportToXml(string file)
        {
            List<Service> services = new List<Service>();
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(fs,Encoding.UTF8))
            {
                XDocument xml = XDocument.Load(reader);

                services = xml.Root.Elements("Service")
                                 .Select(e => new Service
                                 {
                                     Id = int.Parse(e.Attribute("Id").Value),
                                     Title = e.Element("Title").Value,
                                     Username = e.Element("Username").Value,
                                     Password = e.Element("Password").Value,
                                     Created = DateTime.Parse(e.Element("Created").Value),
                                     Comment = e.Element("Comment").Value,
                                     Owner = e.Element("Owner").Value
                                 }).ToList();
            }

            services = ArrangeServices(services);

            unitOfWork.Services.AddRange(services);
            unitOfWork.Save();
        }

        private List<Service> ArrangeServices(List<Service> services)
        {
            string owner = services.Select(e => e.Owner).FirstOrDefault();

            Account account = null;

            if (owner != null)
            {
                account = unitOfWork.Accounts.GetByUsername(owner);

                services = services.Select(s =>
                                    new Service
                                    {
                                        Id = s.Id,
                                        Title = s.Title,
                                        Username = s.Username,
                                        Password = s.Password,
                                        Created = s.Created,
                                        Comment = s.Comment,
                                        Account = account
                                    }).ToList();

               

            }

            return services;
        }



    }
}
