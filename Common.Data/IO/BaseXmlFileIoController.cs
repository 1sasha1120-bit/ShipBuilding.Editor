using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Common.Data.IO {
    public abstract class BaseXmlFileIoController<TWrite, TRead> : FileTypeInformer
        where TRead : TWrite {

        string _rootElementName;

        protected BaseXmlFileIoController(string fileTypeCaption, string fileExtension, string rootElementName) 
            : base(fileTypeCaption, fileExtension) {
            _rootElementName = rootElementName;
        }

        public void Save(TWrite dataSet, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            XmlWriter? writer = null;
            try {
                writer = XmlWriter.Create(filePath, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement(_rootElementName);
                WriteData(dataSet, writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            catch (Exception) {
                throw;
            }
            finally {
                if (writer != null)
                    writer.Close();
            }
        }

        protected abstract void WriteData(TWrite dataSet, XmlWriter writer);

        public bool Load(TRead dataSet, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath)) return false;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(filePath, settings)) {
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Element) {
                        ReadData(dataSet, reader);
                    }
                }
            }
            return true;
        }

        protected abstract void ReadData(TRead dataSet, XmlReader reader);
    }
}
