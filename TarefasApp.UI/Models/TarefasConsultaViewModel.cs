using TarefasApp.Services.Model.Responses;

namespace TarefasApp.UI.Models
{
    public class TarefasConsultaViewModel
    {
        public List<TarefasConsultaResponseModel> Tarefas { get; set; }

        public TarefasConsultaViewModel()
        {
            Tarefas = new List<TarefasConsultaResponseModel>();

            for (int i = 0; i < 6; i++)
            {
                Tarefas.Add(new TarefasConsultaResponseModel
                {
                    Nome = "Tarefa Modelo - Agenda",
                    DataHoraInicio = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    DataHoraFim = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    Categoria = "Categoria teste",
                    Observacoes = "Observações teste"
                });
            }
        }
    }
}



