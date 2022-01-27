using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LALCXamarin.ViewModels.Categorias
{
   
    public class EditarCategoriaViewModel: LALC_UWP.Models.Categoria
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title { get; set; }

        public EditarCategoriaViewModel()
        {
            Title = "Editar categoría";
            lalcAPI = new LalcAPI();
        }

        public async Task<Categoria> OnAppearing(int id)
        {
            return await lalcAPI.GetCategoria(id);
        }

        public async Task<Boolean> EditarCategoria(int id,Categoria editCategoria)
        {
            return await lalcAPI.EditarCategoria(id, editCategoria);
        }

    }
}
