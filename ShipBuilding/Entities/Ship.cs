using Common.Entities;
using ShipBuilding.Types;
using ShipBuilding.Types.Extensions;
using System;
using static ShipBuilding.Settings.UiSettings;


namespace ShipBuilding.Entities
{
    [Serializable]
    public class Ship : Entity
    {
        //public int Id { get; set; }

        public string? Name { get; set; }
        public string? RegistrationNumber { get; set; }

        public DateTime? LaunchDate { get; set; }
        public ShipStatus Status { get; set; }

        public double? Height {   get; set; }
        public double? Width {   get; set; }
        public double? Length { get; set; }

        public Shipyard? Shipyard { get; set; }
        public TypeOfShip? TypeOfShip {  get; set; }
        public ShipProject? Project { get; set; }


        public override string Key => $"{RegistrationNumber} - {Name}"; 

        public override string ToString() => // 
                $"\t{ShipCaption} {IdCaption}{Id} \n" +
                $"\t{NameCaption}: {Name}\n" +
                $"\t{RegistrationNumberCaption}: {RegistrationNumber}\n" +
                $"\t{LaunchDateCaption}: {LaunchDate}\n" + 
                $"\t{StatusCaption}: {Status.GetDisplayName()}\n" +
                $"\t{HeightCaption}: {Height}\n" +
                $"\t{WidthCaption}: {Width}\n" +
                $"\t{LengthCaption}: {Length}\n" +
                $"\t{TypeOfShipCaption}: {TypeOfShip?.Key}\n" +
                $"\t{ShipyardCaption}: {Shipyard?.Key}\n";

        public Ship(string? name, string? registrationNumber, DateTime? launchDate,
            ShipStatus? status, double? height, double? width, 
            double? length, Shipyard? shipyard, TypeOfShip? typeOfShip, ShipProject? project)
        {
            Name = name;
            RegistrationNumber = registrationNumber;

            LaunchDate = launchDate;
            Status = status ?? ShipStatus.Unknown;
            Height = height;
            Width = width;
            Length = length;
            Shipyard = shipyard;
            TypeOfShip = typeOfShip;
            Project = project;
        }

        public Ship() : this(null, null, null, null, null,  null, null, null, null, null) { }

        public Ship(string? name, string? registrationNumber) : this(name, registrationNumber, 
            null, null, null, null, null, null, null, null) { }
    }
}