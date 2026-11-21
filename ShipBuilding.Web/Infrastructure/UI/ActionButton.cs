namespace ShipBuilding.Web.Infrastructure.UI
{
    public enum ActionButtonKind { Create, Save, Edit, Delete }

    public record ActionButton(
        ActionButtonKind Kind,
        int? Id = null,
        bool Submit = false,
        string? CssClass = null,
        string? Controller = null)
    {
        public static ActionButton CreateLink(string? cssClass = null, string? controller = null)
        {
            return new(ActionButtonKind.Create, CssClass: cssClass, Controller: controller);
        }

        public static ActionButton CreateSubmit()
        {
            return new(ActionButtonKind.Create, Submit: true);
        }

        public static ActionButton SaveSubmit()
        {
            return new(ActionButtonKind.Save, Submit: true);
        }

        public static ActionButton EditLink(int id, string? cssClass = null, string? controller = null)
        {
            return new(ActionButtonKind.Edit, id, CssClass: cssClass, Controller: controller);
        }

        public static ActionButton DeleteLink(int id)
        {
            return new(ActionButtonKind.Delete, id);
        }

        public static ActionButton DeleteSubmit()
        {
            return new(ActionButtonKind.Delete, Submit: true);
        }
    }
}
