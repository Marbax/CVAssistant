using DotLiquid;
using System;

namespace Models
{
    public class Person : ILiquidizable
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _surname;

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private System.DateTime _bDay;

        public System.DateTime BirthDay
        {
            get { return _bDay; }
            set { _bDay = value; }
        }

        public int YearsOld
        {
            get { return System.DateTime.Now.Year - BirthDay.Year; }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _country;


        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _photo;


        public string Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }

        public Person(string name, string surname, string lastName, DateTime birthDay, string phone, string email, string photo) :
            this(name, surname, lastName, birthDay, phone, null, null, email, photo)
        {
        }

        public Person(string name, string surname, string lastName, DateTime birthDay, string phone, string city, string country, string email, string photo)
        {
            Name = name;
            Surname = surname;
            LastName = lastName;
            BirthDay = birthDay;
            Phone = phone;
            City = city;
            Country = country;
            Email = email;
            Photo = photo;
        }

        public object ToLiquid()
        {
            return new { Name, Surname, LastName, BirthDay = BirthDay.ToShortDateString(), YearsOld, Phone, City, Country, Email, Photo };
        }
    }
}
