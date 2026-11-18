using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using ShipBuilding.Types;
using ShipBuilding.Web.Infrastructure.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.Limitation;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.Ships
{
    public class ShipEditingModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }


        [Display(Name = NameCaption)]
        [Required(ErrorMessage =
             $"Потрібно заповнити поле \'{NameCaption}\'")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
             ErrorMessage = $"{NameCaption} {ShipsHeader}"
             + "повинна містити від {2} до {1} символів")]
        public string? Name { get; set; }


        [Display(Name = $"{RegistrationNumberCaption}")]
        [Required(ErrorMessage =
             $"{RegistrationNumberErrorMessage}")]
        [RegularExpression(RegistrationNumberLimitation, ErrorMessage =
            RegistrationNumberErrorMessage)]
        public string? RegistrationNumber { get; set; }

        [Display(Name = LaunchDateCaption)]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage =
            "Дата спуску на воду не може перевищувати сьогоднішню")]
        public DateTime? LaunchDate { get; set; }

        [Display(Name = StatusCaption)]
        [Required(ErrorMessage = $"Виберіть {StatusCaption}")]
        [Range(1, 4, ErrorMessage = $"Виберіть {StatusCaption}")]
        public ShipStatus Status { get; set; }

        [Display(Name = $"{HeightCaption}")]
        [Range(HeightOfShipMinLength, HeighthOfShipMaxLength, ErrorMessage = $"{HeightCaption}"
            + "повинно бути в межах від {1} до {2}")]
        public double? Height { get; set; }


        [Display(Name = $"{WidthCaption}")]
        [Range(WidthOfShipMinLength, WidthOfShipMaxLength, ErrorMessage = $"{WidthCaption}"
            + "повинно бути в межах від {1} до {2}")]
        public double? Width { get; set; }


        [Display(Name = $"{LengthCaption}")]
        [Range(LengthOfShipMinLength, LengthOfShipMaxLength, ErrorMessage = $"{LengthCaption}"
            + "повинно бути в межах від {1} до {2}")]
        public double? Length { get; set; }


        [Display(Name = $"{ShipyardCaption}")]
        [Required(ErrorMessage =
     $"Потрібно заповнити поле \'{ShipyardCaption}\'")]
        public string? ShipyardName { get; set; }


        [Display(Name = $"{TypeOfShipCaption}")]
        [Required(ErrorMessage =
    $"Потрібно заповнити поле \'{TypeOfShipCaption}\'")]
        public string? TypeOfShipName { get; set; }

        [Display(Name = $"{ProjectCaption}")]
        public string? ProjectName { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? ShipyardSelectList { get; set; }

        [ScaffoldColumn(false)]
        public SelectList? TypeOfShipSelectList { get; set; }
        [ScaffoldColumn(false)]
        public SelectList? ProjectSelectList { get; set; }


        public static explicit operator ShipEditingModel(Ship obj)
        {
            return new ShipEditingModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                RegistrationNumber = obj.RegistrationNumber,
                LaunchDate = obj.LaunchDate,
                Status = obj.Status,
                Height = obj.Height,
                Width = obj.Width,
                Length = obj.Length,
                ShipyardName = obj.Shipyard?.Name,
                TypeOfShipName = obj.TypeOfShip?.Name,
                ProjectName = obj.Project?.Name,
            };
        }
    }
}
