using System;
using Abp.Domain.Entities;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    public class MicroItem : Entity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string ItemNumber { get; set; }

        public string Factory { get; set; }

        public string AddressNumber { get; set; }

        public DateTime BorrowDate { get; set; }

        public string Borrower { get; set; }

        public string Complainer { get; set; }
        
        public string ComplainPhone { get; set; }

        public string MateriaNumber { get; set; }

        public string SubmitForm { get; set; }

        public string Protocol { get; set; }

        public string SerialNumber { get; set; }

        public string Comments { get; set; }
    }
}