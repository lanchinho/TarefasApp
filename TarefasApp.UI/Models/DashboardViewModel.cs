namespace TarefasApp.UI.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            DashboardModelList = new List<DadosDashboardModel>
            {
                new DadosDashboardModel { Categoria = "Trabalho", Quantidade = 15 },
                new DadosDashboardModel { Categoria = "Estudo", Quantidade = 10 },
                new DadosDashboardModel { Categoria = "Família", Quantidade = 5 },
                new DadosDashboardModel { Categoria = "Outros", Quantidade = 5 }
            };
        }
        public List<DadosDashboardModel> DashboardModelList { get; set; }
    }

    public class DadosDashboardModel
    {
        public string Categoria { get; set; }
        public int? Quantidade { get; set; }
    }
}
