using API.Modelos;
using CarrinhoAPI.Models;
using DataBase.APPCarrinho.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Modelos.APPCarrinho.Modelos.User;

namespace CarrinhoAPI.Controllers
{
    //[Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")] // A rota para este controlador será /user
    public class UserController : Controller
    {
        private readonly UserManager<UsuarioLoginModels> _userManager;
        private readonly DAL<CongregacaoModel> _dalCongregacao;

        public UserController(UserManager<UsuarioLoginModels> userManager, DAL<CongregacaoModel> dalCongregacao)
        {
            _userManager = userManager;
            _dalCongregacao = dalCongregacao;
        }


        [HttpPost("criar")]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioModel model)
        {
            // Verifica se os dados do modelo são válidos
            if (!ModelState.IsValid) return BadRequest(ModelState);

            CongregacaoModel? congregacao = await _dalCongregacao.BuscarPorAsync(x => x.Id.Equals(model.CongregacaoId));
            if (congregacao is null) return BadRequest("Congregacao não existe no banco de dados!");


            
            // Criar um novo usuário com os dados fornecidos
            var user = new UsuarioLoginModels
            {
                UserName = model.Email,
                NomeCompleto = model.NomeCompleto,
                Email = model.Email,
                CongregacaoId = model.CongregacaoId,
                TipoUser = model.Tipo_User,
                DataCriacao = DateTime.Now
            };


            // Cria o usuário com a senha fornecida
            var result = await _userManager.CreateAsync(user, model.Password);

            // Se o usuário for criado com sucesso, retorna mensagem de sucesso
            if (result.Succeeded) return Ok(new { message = "Usuário criado com sucesso!" });

            return BadRequest(result.Errors);
        }
    }
}
