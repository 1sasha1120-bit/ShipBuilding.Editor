namespace ShipBuilding.Web.ViewModels
{
    public enum ActionButtonKind { Create, Save, Edit, Delete }

    public record ActionButtonViewModel(
        ActionButtonKind Kind,
        int? Id = null,
        bool Submit = false,
        string? CssClass = null,
        string? Controller = null)
    {
        public static ActionButtonViewModel CreateLink(string? cssClass = null, string? controller = null)
        {
            return new(ActionButtonKind.Create, CssClass: cssClass, Controller: controller);
        }

        public static ActionButtonViewModel CreateSubmit()
        {
            return new(ActionButtonKind.Create, Submit: true);
        }

        public static ActionButtonViewModel SaveSubmit()
        {
            return new(ActionButtonKind.Save, Submit: true);
        }

        public static ActionButtonViewModel EditLink(int id, string? cssClass = null, string? controller = null)
        {
            return new(ActionButtonKind.Edit, id, CssClass: cssClass, Controller: controller);
        }

        public static ActionButtonViewModel DeleteLink(int id)
        {
            return new(ActionButtonKind.Delete, id);
        }

        public static ActionButtonViewModel DeleteSubmit()
        {
            return new(ActionButtonKind.Delete, Submit: true);
        }
    }
}
