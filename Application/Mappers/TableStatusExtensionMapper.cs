using Domain.Table.Enum;

namespace Application.Mappers
{
    public static class TableStatusExtensionMapper
    {
        public static string StatusMap(this StatusTable status)
        {
            return status switch
            {
                StatusTable.Livre => "Livre",
                StatusTable.Ocupada => "Ocupada",
                StatusTable.Fechada => "Fechada",
                _ => "Indeterminado."
            };
        }
    }
}
