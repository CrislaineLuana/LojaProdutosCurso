using LojaProdutosCurso.Enums;
using LojaProdutosCurso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LojaProdutosCurso.Filtros
{
    public class UsuarioLogadoAdm : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var sessao = context.HttpContext.Session.GetString("usuarioSessao");


            if (string.IsNullOrEmpty(sessao))
            {

                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Login" },
                    {"action", "Login" }
                });
            }
            else
            {
                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(sessao);






                if (usuarioModel.Cargo == CargoEnum.Cliente)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            {"controller", "Home" },
                            {"action", "Index" }
                        });
                }
            }

           



            base.OnActionExecuting(context);
        }


    }
}
