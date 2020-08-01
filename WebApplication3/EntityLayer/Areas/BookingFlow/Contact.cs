using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.EntityLayer.Areas.BookingFlow
{
    public class Contact : IPerson
    {
        private string _phone;
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { 
            get => this._phone; 
            set => _= ValidPositiveNum(value) ? this._phone = value : throw new Exception(); 
        }

        public bool ValidPositiveNum(string val)
        {
            try
            {
                var num = Convert.ToUInt32(val);

                return num >= 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
