using DotLiquid;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Experience : ILiquidizable
    {
        private string _company;
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private System.DateTime _startDate;
        public System.DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private System.DateTime _endDate;
        public System.DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private IEnumerable<string> _r14s;
        public IEnumerable<string> Responsibilities
        {
            get { return _r14s; }
            set { _r14s = value; }
        }

        public Experience(string company, string position, DateTime startDate, DateTime endDate, IEnumerable<string> responsibilities)
        {
            Company = company;
            Position = position;
            StartDate = startDate;
            EndDate = endDate;
            Responsibilities = responsibilities;
        }

        public object ToLiquid()
        {
            return new { Company, Position, StartDate = StartDate.ToShortDateString(), EndDate = EndDate.ToShortDateString(), Responsibilities };
        }
    }
}
