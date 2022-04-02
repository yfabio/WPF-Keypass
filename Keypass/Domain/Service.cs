using MvvmFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Keypass.Domain
{
    public class Service : IEquatable<Service>
    {
        private int id;

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private DateTime created;

        public DateTime Created
        {
            get {                
                return created;
            }
            set { created = value; }
        }


        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
             
        private Account account;

        [JsonIgnore]
        public Account Account
        {
            get { return account; }
            set
            { 
                account = value;               
            }
        }

        /// <summary>
        /// This field is used in the FileImport class
        /// </summary>
        private string owner;

        [NotMapped]
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }


        public Service()
        {
            if (object.Equals(Created, null)) 
            {
                Created = DateTime.Now;
            }            
        }

        public override string ToString()
        {
            Type type = this.GetType();

            PropertyInfo[] properties = type.GetProperties();

            StringBuilder sb = new StringBuilder();

            foreach (var prop in properties)
            {
                sb.Append(prop.Name).Append(": ").Append(prop.GetValue(this)).Append(Environment.NewLine);
            }

            return sb.ToString();
        }



        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }
            Service service = obj as Service;
            if (service == null) {
                return false;
            }
            return Equals(service);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Service service)
        {
            return Id == service.Id;
        }
    }
}
