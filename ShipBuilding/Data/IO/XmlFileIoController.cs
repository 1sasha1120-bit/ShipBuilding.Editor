using Common.Data.IO;
using ShipBuilding.Data.Interfaces;
using ShipBuilding.Entities;
using System;
using System.Collections.Generic;
using ShipBuilding.Data.IO.Interfaces;
using System.Linq;
using System.Xml;
using ShipBuilding.Types;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Data.IO
{
    public class XmlFileIoController
        : BaseXmlFileIoController<IReadOnlyDataSet, IDataSet>, IFileIoController
    {
        public XmlFileIoController() : base(XmlFileTypeCaption, XmlFileExtension,
            XmlRootElementName)
        { }

        protected override void WriteData(IReadOnlyDataSet dataSet, XmlWriter writer)
        {
            WriteTypeOfShips(dataSet.Types, writer);
            WriteShipyards(dataSet.Shipyards, writer);
            WriteShipProjects(dataSet.ShipProjects, writer);
            WriteShips(dataSet.Ships, writer);
        }

        void WriteTypeOfShips(IEnumerable<TypeOfShip> collection, XmlWriter writer)
        {
            writer.WriteStartElement("TypeOfShipsData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("TypeOfShip");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                int parentId = obj.Parent == null ? 0 : obj.Parent.Id;
                writer.WriteElementString("ParentId", parentId.ToString());
                writer.WriteElementString("Description", obj.Description);
                writer.WriteElementString("TypeCode", obj.TypeCode);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        void WriteShipyards(IEnumerable<Shipyard> collection, XmlWriter writer)
        {
            writer.WriteStartElement("ShipyardData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("Shipyard");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("CountryName", obj.CountryName);
                writer.WriteElementString("YearOfFounded", obj.YearOfFounded.ToString());
                writer.WriteElementString("NumberOfDocks", obj.NumberOfDocks.ToString());
                writer.WriteElementString("CapacityPerYear", obj.CapacityPerYear.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        void WriteShipProjects(IEnumerable<ShipProject> collection, XmlWriter writer)
        {
            writer.WriteStartElement("ShipProjectsData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("ShipProject");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("ProjectCode", obj.ProjectCode);
                writer.WriteElementString("Description", obj.Description);
                writer.WriteElementString("Budget", obj.Budget.ToString());
                int shipyardId = obj.Shipyard == null ? 0 : obj.Shipyard.Id;
                writer.WriteElementString("ShipyardId", shipyardId.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        void WriteShips(IEnumerable<Ship> collection, XmlWriter writer)
        {
            writer.WriteStartElement("ShipsData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("Ship");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("RegistrationNumber", obj.RegistrationNumber);
                writer.WriteElementString("LaunchDate", obj.LaunchDate.ToString());
                writer.WriteElementString("Status", ((int)obj.Status).ToString());
                writer.WriteElementString("Height", obj.Height.ToString());
                writer.WriteElementString("Width", obj.Width.ToString());
                writer.WriteElementString("Length", obj.Length.ToString());
                int shipyardId = obj.Shipyard == null ? 0 : obj.Shipyard.Id;
                writer.WriteElementString("ShipyardId", shipyardId.ToString());
                int typeId = obj.TypeOfShip == null ? 0 : obj.TypeOfShip.Id;
                writer.WriteElementString("TypeOfShipId", typeId.ToString());
                int projectId = obj.Project == null ? 0 : obj.Project.Id;
                writer.WriteElementString("ProjectId", projectId.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        protected override void ReadData(IDataSet dataSet, XmlReader reader)
        {
            switch (reader.Name)
            {
                case "TypeOfShip":
                    ReadTypeOfShip(reader, dataSet.Types);
                    break;
                case "Shipyard":
                    ReadShipyard(reader, dataSet.Shipyards);
                    break;
                case "ShipProject":
                    ReadShipProject(reader, dataSet);
                    break;
                case "Ship":
                    ReadShip(reader, dataSet);
                    break;
            }
        }

        void ReadTypeOfShip(XmlReader reader, ICollection<TypeOfShip> collection)
        {
            reader.ReadStartElement("TypeOfShip");
            int id = reader.ReadElementContentAsInt();
            string name = reader.ReadElementContentAsString();
            int parentId = reader.ReadElementContentAsInt();
            TypeOfShip? parent = collection.FirstOrDefault(e => e.Id == parentId);
            var obj = new TypeOfShip(name, parent) { Id = id };
            obj.Description = reader.ReadElementContentAsString();
            obj.TypeCode = reader.ReadElementContentAsString();
            collection.Add(obj);
        }

        void ReadShipyard(XmlReader reader, ICollection<Shipyard> collection)
        {
            reader.ReadStartElement("Shipyard");
            int id = reader.ReadElementContentAsInt();
            string name = reader.ReadElementContentAsString();
            string countryName = reader.ReadElementContentAsString();
            var obj = new Shipyard() { Id = id, Name = name, CountryName = countryName };
            string yearOfFounded = reader.ReadElementContentAsString();
            obj.YearOfFounded = string.IsNullOrEmpty(yearOfFounded) ? (int?)null : int.Parse(yearOfFounded);
            string numberOfDocks = reader.ReadElementContentAsString();
            obj.NumberOfDocks = string.IsNullOrEmpty(numberOfDocks) ? (int?)null : int.Parse(numberOfDocks);
            string capacityPerYear = reader.ReadElementContentAsString();
            obj.CapacityPerYear = string.IsNullOrEmpty(capacityPerYear) ? (double?)null : double.Parse(capacityPerYear);
            collection.Add(obj);
        }

        void ReadShipProject(XmlReader reader, IDataSet dataSet)
        {
            reader.ReadStartElement("ShipProject");
            int id = reader.ReadElementContentAsInt();
            string name = reader.ReadElementContentAsString();
            string projectCode = reader.ReadElementContentAsString();
            string description = reader.ReadElementContentAsString();
            string budgetStr = reader.ReadElementContentAsString();
            // Порожній елемент означає «бюджету немає», а не 0
            decimal? budget = string.IsNullOrEmpty(budgetStr) ? null : decimal.Parse(budgetStr);
            int shipyardId = reader.ReadElementContentAsInt();
            Shipyard? shipyard = dataSet.Shipyards.FirstOrDefault(e => e.Id == shipyardId);
            if (shipyard == null) return; // ShipProject requires a Shipyard
            var obj = new ShipProject
            {
                Id = id,
                Name = name,
                ProjectCode = projectCode,
                Description = description,
                Budget = budget,
                Shipyard = shipyard
            };
            dataSet.ShipProjects.Add(obj);
        }

        void ReadShip(XmlReader reader, IDataSet dataSet)
        {
            reader.ReadStartElement("Ship");
            var obj = new Ship();
            obj.Id = reader.ReadElementContentAsInt();
            obj.Name = reader.ReadElementContentAsString();
            obj.RegistrationNumber = reader.ReadElementContentAsString();

            string launchDate = reader.ReadElementContentAsString();
            obj.LaunchDate = string.IsNullOrEmpty(launchDate) ? (DateTime?)null : DateTime.Parse(launchDate);

            string statusStr = reader.ReadElementContentAsString();
            obj.Status = string.IsNullOrEmpty(statusStr) ? ShipStatus.Unknown : (ShipStatus)int.Parse(statusStr);

            string height = reader.ReadElementContentAsString();
            obj.Height = string.IsNullOrEmpty(height) ? (double?)null : double.Parse(height);
            string width = reader.ReadElementContentAsString();
            obj.Width = string.IsNullOrEmpty(width) ? (double?)null : double.Parse(width);
            string length = reader.ReadElementContentAsString();
            obj.Length = string.IsNullOrEmpty(length) ? (double?)null : double.Parse(length);

            int shipyardId = reader.ReadElementContentAsInt();
            obj.Shipyard = dataSet.Shipyards.FirstOrDefault(e => e.Id == shipyardId);

            int typeOfShipId = reader.ReadElementContentAsInt();
            obj.TypeOfShip = dataSet.Types.FirstOrDefault(e => e.Id == typeOfShipId);

            int projectId = reader.ReadElementContentAsInt();
            obj.Project = dataSet.ShipProjects.FirstOrDefault(e => e.Id == projectId);

            dataSet.Ships.Add(obj);
        }
    }
}

