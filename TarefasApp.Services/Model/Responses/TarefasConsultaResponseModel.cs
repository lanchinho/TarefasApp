namespace TarefasApp.Services.Model.Responses
{
    public class TarefasConsultaResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? DataHoraInicio { get; set; }
        public string? DataHoraFim { get; set; }
        public string? Categoria { get; set; }
        public string? Observacoes { get; set; }
    }
}
