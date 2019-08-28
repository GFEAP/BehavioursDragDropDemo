using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Framework
{
    /// <summary>
    /// The ViewModelBase for MVVM
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewModelBase<T>: INotifyPropertyChanged where T : new() //  INotifyPropertyChanged
    {   
        public event PropertyChangedEventHandler PropertyChanged;

        protected T Model;

        protected ViewModelBase(T model)
        {
            Model = model;
        }
                
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Delievers an XmlString of the instance
        /// </summary>
        /// <returns></returns>
        protected string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                NewLineHandling = NewLineHandling.None,
                Encoding = new ASCIIEncoding()
            };

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    xmlSerializer.Serialize(textWriter, Model);
                }
                return textWriter.ToString();
            }
        }
    }
}
