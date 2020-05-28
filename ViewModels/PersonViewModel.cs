using Commands;
using Microsoft.Win32;
using Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModels
{
    public class PersonViewModel : AViewModel
    {
        private Person _person;
        public Person Person { get => _person; set => SetProperty(ref _person, value); }

        private readonly RelayCommand _selectPhotoCommand;
        public ICommand SelectPhoto => _selectPhotoCommand;


        public string Name
        {
            get { return _person.Name; }
            set
            {
                if (_person.Name == null || !_person.Name.Equals(value))
                {
                    _person.Name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.Name)));
                }
            }
        }

        public string Surname
        {
            get { return _person.Surname; }
            set
            {
                if (_person.Surname == null || !_person.Surname.Equals(value))
                {
                    _person.Surname = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.Surname)));
                }
            }
        }

        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                if (_person.LastName == null || !_person.LastName.Equals(value))
                {
                    _person.LastName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.LastName)));
                }
            }
        }

        public DateTime BirthDay
        {
            get { return _person.BirthDay; }
            set
            {
                if (!_person.BirthDay.Equals(value))
                {
                    _person.BirthDay = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.BirthDay)));
                }
            }
        }

        public int YearsOld { get => _person.YearsOld; }

        public string Phone
        {
            get { return _person.Phone; }
            set
            {
                if (_person.Phone == null || !_person.Phone.Equals(value))
                {
                    _person.Phone = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.Phone)));
                }
            }
        }

        public string City
        {
            get { return _person.City; }
            set
            {
                if (_person.City == null || !_person.City.Equals(value))
                {
                    _person.City = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.City)));
                }
            }
        }

        public string Country
        {
            get { return _person.Country; }
            set
            {
                if (_person.Country == null || !_person.Country.Equals(value))
                {
                    _person.Country = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.Country)));
                }
            }
        }

        public string Email
        {
            get { return _person.Email; }
            set
            {
                if (_person.Email == null || !_person.Email.Equals(value))
                {
                    _person.Email = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.Email)));
                }
            }
        }

        public string Photo
        {
            get { return _person.Photo; }
            set
            {
                if (_person.Photo == null || !_person.Photo.Equals(value))
                {
                    _person.Photo = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(_person.Photo)));
                }
            }
        }


        public PersonViewModel(Person person)
        {
            _person = person;
            _selectPhotoCommand = new RelayCommand(OpenAndLoadPhoto);
        }

        private void OpenAndLoadPhoto(object args = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Photo = openFileDialog.FileName;
            }
        }

    }

}
