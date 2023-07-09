using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Services.Model.Requests;

namespace TarefasApp.UI.Models
{
    /// <summary>
    /// Classe para definir o modelo de dados da página de autenticação
    /// </summary>
    public class AutenticarViewModel
    {
        /// <summary>
        /// Propriedade para acessa o modelo de dados
        /// </summary>
        public AutenticarRequestModel AutenticarRequestModel { get; set; }

        public AutenticarViewModel()
        {
            AutenticarRequestModel = new AutenticarRequestModel();
        }
    }
}
