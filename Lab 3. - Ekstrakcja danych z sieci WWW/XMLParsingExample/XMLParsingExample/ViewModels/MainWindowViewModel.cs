using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMLParsingExample.MVVMUtilities;
using XMLParsingExample.Models;

namespace XMLParsingExample.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        // Properties
        private StudentsInformation studentInformation;
        public StudentsInformation StudentInformationObject
        {
            get { return studentInformation; }
            private set
            {
                studentInformation = value;
                NotifyPropertyChanged("StudentInformationObject");
            }
        }

        private void InitiateState()
        {
            studentInformation = new StudentsInformation();
        }

        // Commands
        public RelayCommand ClearResultCommand { get; private set; }
        private void ClearResult()
        {
            StudentInformationObject = new StudentsInformation();
        }

        public RelayCommand XMLDocumentLoadCommand { get; private set; }
        private void XMLDocumentLoad()
        {
            StudentInformationObject = XMLParsers.ParseByXMLDocument();
        }

        public RelayCommand XDocumentLoadCommand { get; private set; }
        private void XDocumentLoad()
        {
            StudentInformationObject = XMLParsers.ParseByXDocument();
        }

        private void WireCommands()
        {
            ClearResultCommand = new RelayCommand(ClearResult);
            ClearResultCommand.IsEnabled = true;

            XMLDocumentLoadCommand = new RelayCommand(XMLDocumentLoad);
            XMLDocumentLoadCommand.IsEnabled = true;

            XDocumentLoadCommand = new RelayCommand(XDocumentLoad);
            XDocumentLoadCommand.IsEnabled = true;
        }

        // Constructor
        public MainWindowViewModel()
        {
            InitiateState();
            WireCommands();
        }
    }
}
