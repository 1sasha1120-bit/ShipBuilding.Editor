namespace ShipBuilding.Web.Models
{
    public enum ActionButtonKind
    {
        Create,
        Save,
        Edit,
        Delete
    }

    public record ActionButtonModel(
        ActionButtonKind Kind,
        int? Id = null,
        bool Submit = false,
        string? CssClass = null,
        string? Controller = null)
    {
        public static ActionButtonModel CreateLink(string? cssClass = null, string? controller = null) =>
            new(ActionButtonKind.Create, CssClass: cssClass, Controller: controller);
        public static ActionButtonModel CreateSubmit() => new(ActionButtonKind.Create, Submit: true);
        public static ActionButtonModel SaveSubmit() => new(ActionButtonKind.Save, Submit: true);
        public static ActionButtonModel EditLink(int id, string? cssClass = null, string? controller = null) =>
            new(ActionButtonKind.Edit, id, CssClass: cssClass, Controller: controller);
        public static ActionButtonModel DeleteLink(int id) => new(ActionButtonKind.Delete, id);
        public static ActionButtonModel DeleteSubmit() => new(ActionButtonKind.Delete, Submit: true);
    }
}
